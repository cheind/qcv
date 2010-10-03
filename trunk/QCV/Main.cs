using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using NDesk.Options;
using System.CodeDom.Compiler;
using log4net.Config;
using log4net;

namespace QCV {
  public partial class Main : Form, QCV.Base.IInteraction {
    private static readonly ILog _logger = LogManager.GetLogger("qcv");
    private Dictionary<string, ShowImageForm> _show_forms = new Dictionary<string, ShowImageForm>();
    private Base.FilterList _filters = new QCV.Base.FilterList();
    private Base.Runtime _runtime = null;
    private PropertyForm _props = new PropertyForm();

    public Main() {
      InitializeComponent();
      _runtime = new QCV.Base.Runtime(this);

      XmlConfigurator.Configure(new System.IO.FileInfo("QCV.log4net"));
      
      this.AddOwnedForm(_props);

      Base.Addins.AddinHost.DiscoverInDomain();
      Base.Addins.AddinHost.DiscoverInDirectory(Environment.CurrentDirectory);
      Base.Addins.AddinHost.DiscoverInDirectory(Path.Combine(Environment.CurrentDirectory, "plugins"));

      _props.FormClosing += new FormClosingEventHandler(AnyFormClosing);

      // Parse command line
      CommandLine cl = new CommandLine();
      CommandLine.CLIArgs args = cl.Args;

      if (args.script_paths.Count > 0) {
        Base.Compiler s = new QCV.Base.Compiler();
        
        bool success = s.CompileFromFile(
          args.script_paths,
          new string[] { 
            "mscorlib.dll", "System.dll", "System.Drawing.dll", "System.Xml.dll",
            "QCV.Base.dll", "QCV.Toolbox.dll", "Emgu.CV.dll", "Emgu.Util.dll"}
        );

        if (success) {
          _logger.Debug(s.FormatErrors(s.CompilerResults));
          Base.Addins.AddinHost.DiscoverInAssembly(s.CompiledAssemblies);
        } else {
          _logger.Error(s.FormatErrors(s.CompilerResults));
        }
      }

      _filters.AddRange(LoadAndCombineFilterLists(args.load_paths));
      _filters.AddRange(CreateFilterListFromNames(args.filter_names));

      _lb_status.Text = String.Format("Created {0} filters", _filters.Count);
      PreprocessFilter(_filters);
      _props.Filters = _filters;

      if (args.immediate_execute) {
        RunOrStopRuntime();
      }
    }

    private Base.FilterList LoadAndCombineFilterLists(List<string> list) {
      Base.FilterList fl = new QCV.Base.FilterList();
      foreach (string path in list) {
        fl.AddRange(Base.FilterList.Load(path));
      }
      return fl;
    }

    private void PreprocessFilter(QCV.Base.FilterList filters) {
      Toolbox.ShowFPS fps = filters.FirstOrDefault(
        (f) => { return f is Toolbox.ShowFPS; }
      ) as Toolbox.ShowFPS;

      if (fps != null) {
        fps.FPSUpdateEvent += new Toolbox.ShowFPS.FPSUpdateEventHandler(FPSUpdateEvent);
      }
    }

    void FPSUpdateEvent(object sender, double fps) {
      if (_lb_status.InvokeRequired) {
        _btn_run.Invoke(new MethodInvoker(delegate {
          _lb_status.Text = String.Format("FPS: {0}", (int)fps);
        }));
      } else {
        _lb_status.Text = String.Format("FPS: {0}", (int)fps);
      }
    }

    void AnyFormClosing(object sender, FormClosingEventArgs e) {
      if (e.CloseReason != CloseReason.FormOwnerClosing) {
        e.Cancel = true;
        (sender as Form).Hide();
      }
    }

    Base.FilterList CreateFilterListFromNames(IEnumerable<string> filter_names) {
      Base.FilterList fl = new QCV.Base.FilterList();
      foreach (string filter_name in filter_names) {
        IEnumerable<Base.Addins.AddinInfo> e = Base.Addins.AddinHost.FindAddins(
          typeof(Base.IFilter),
          (ai) => { return ai.FullName == filter_name; }
        );
        if (e.Count() > 0) {
          Base.IFilter f = Base.Addins.AddinHost.CreateInstance(e.First()) as Base.IFilter;
          fl.Add(f);
        }
      }
      return fl;
    }

    private void _btn_props_Click(object sender, EventArgs e) {
      _props.Show();
    }

    private void _btn_play_Click(object sender, EventArgs e) {
      RunOrStopRuntime();
    }

    private void RunOrStopRuntime() {
      if (_runtime.Running) {
        _runtime.Stop(false);
      } else {
        _btn_run.Text = "Stop";
        _lb_status.BackColor = Color.LightGreen;

        _runtime.Run(_filters, 0);
      }
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e) {
      if (_runtime.Running) {
        _runtime.Stop(false);
        e.Cancel = true;
      }
    }

    private void _mnu_help_arguments_Click(object sender, EventArgs e) {
      CommandLine cl = new CommandLine();
      MessageBox.Show(cl.GetHelp(), "qcv.exe", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void _mnu_save_filter_list_Click(object sender, EventArgs e) {
      if (this.saveFileDialog1.ShowDialog() == DialogResult.OK) {
        Base.FilterList.Save(this.saveFileDialog1.FileName, _filters);
      }
    }

    #region IInteraction Members

    public void ShowImage(string id, Image<Bgr, byte> image) {
      Image<Bgr, byte> copy = image.Copy();
      this.Invoke(new MethodInvoker(delegate {
        ShowImageForm f = null;
        if (!_show_forms.ContainsKey(id)) {
          f = new ShowImageForm();
          f.Text = id;
          this.AddOwnedForm(f);
          f.FormClosing += new FormClosingEventHandler(AnyFormClosing);
          f.Show();
          _show_forms.Add(id, f);
        } else {
          f = _show_forms[id];
        }
        Rectangle r = f.ClientRectangle;
        f.Image = copy.Resize(r.Width, r.Height, Emgu.CV.CvEnum.INTER.CV_INTER_NN, true);
      }));
    }

    public void RuntimeStarted() {
    }

    public void RuntimeStopped() {
      if (_btn_run.InvokeRequired) {
        _btn_run.Invoke(new MethodInvoker(delegate {
          _btn_run.Text = "Run";
          _lb_status.BackColor = Control.DefaultBackColor;
        }));
      } else {
        _btn_run.Text = "Run";
        _lb_status.BackColor = Control.DefaultBackColor;
      }
    }

    #endregion
  }
}
