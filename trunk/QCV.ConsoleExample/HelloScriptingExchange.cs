﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using log4net;
using Emgu.CV;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloScriptingExchange : IExample {
    public void Run(string[] args) {
      Console.WriteLine("Press any key to quit");

      QCV.Base.Scripting s = new QCV.Base.Scripting();
      CompileScripts(s);

      QCV.Base.Addins.AddinHost h = new QCV.Base.Addins.AddinHost();
      h.DiscoverInAssembly(s.CompiledAssemblies);

      QCV.Base.FilterList fl = BuildFilterList(h);

      QCV.Base.Runtime runtime = new QCV.Base.Runtime();

      Dictionary<string, object> env = new Dictionary<string, object>() {
        {"interaction", new QCV.Base.ConsoleInteraction(runtime)}
      };

      runtime.FPS = 30.0;
      runtime.Run(fl, env, 0);

      System.IO.FileSystemWatcher watch = new System.IO.FileSystemWatcher();
      watch.Path = @"..\..\etc\scripts\";
      watch.Filter = "draw_rectangle.cs";
      watch.NotifyFilter = System.IO.NotifyFilters.LastWrite;
      watch.IncludeSubdirectories = false;

      DateTime dt = DateTime.Now;

      watch.Changed += (sender, ev) => {
        if ((DateTime.Now - dt).TotalSeconds > 1) {
          runtime.Stop(true);
          if (CompileScripts(s)) {

            QCV.Base.Addins.AddinHost tmp = new QCV.Base.Addins.AddinHost();
            tmp.DiscoverInAssembly(s.CompiledAssemblies);

            h.Merge(tmp);

            QCV.Base.Reconfiguration r = new QCV.Base.Reconfiguration();
            QCV.Base.FilterList fl_new;
            if (r.Update(fl, h, out fl_new)) {
              r.CopyPropertyValues(fl, fl_new);
              fl = fl_new;
              runtime.Run(fl_new, env, 0);
            }
          } else {
            runtime.Run(fl, env, 0);
          }
          
          dt = DateTime.Now;
        }

      };
      watch.EnableRaisingEvents = true;

      Console.ReadKey();

      runtime.Stop(true);
      runtime.Shutdown();
    }

    private bool CompileScripts(QCV.Base.Scripting s) {

      string[] scripts = new string[] { 
        @"..\..\etc\scripts\draw_rectangle.cs" 
      };

      return s.Compile(scripts, new string[] { 
        "QCV.Base.dll", 
        "Emgu.CV.dll", 
        "Emgu.Util.dll", 
        "System.dll", 
        "System.Drawing.dll",
        "System.Xml.dll"});
    }

    private QCV.Base.FilterList BuildFilterList(QCV.Base.Addins.AddinHost h) {
      QCV.Toolbox.Sources.Camera c = new QCV.Toolbox.Sources.Camera();
      c.DeviceIndex = 0;
      c.FrameWidth = 320;
      c.FrameHeight = 200;
      c.Name = "source";

      QCV.Toolbox.ShowImage si = new QCV.Toolbox.ShowImage();
      si.BagName = "source";

      object script = h.FindAndCreateInstance(typeof(QCV.Base.IFilter), "Scripts.DrawRectangle");

      QCV.Base.FilterList f = new QCV.Base.FilterList();
      f.Add(c);
      f.Add(script as QCV.Base.IFilter);
      f.Add(si);
      return f;
    }
  }
}

