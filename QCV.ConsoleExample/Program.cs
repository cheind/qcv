using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using QCV.Base;
using QCV.Toolbox;
using QCV.Base.Extensions;
using System.Drawing;
using System.Diagnostics;
using NDesk.Options;
using log4net.Config;

namespace QCV.ConsoleExample {
  [Serializable]
  class Program {

    static void Main(string[] args) {

      XmlConfigurator.Configure(new System.IO.FileInfo("QCV.ConsoleExample.log4net"));

      Base.AddinHost h = new QCV.Base.AddinHost();
      h.DiscoverInDomain();

      string example_name = "";
      bool help = false;
      bool list_examples = false;

      OptionSet opts = new OptionSet() {
        { "l|list", "list all examples.", v => list_examples = v != null},
        { "e|example=", "{EXAMPLE} class to execute", v => example_name = v},
        { "h|help", "print this help message", v => help = v != null }
      };

      try {
        List<string> extra = opts.Parse(args);

        if (help) {
          opts.WriteOptionDescriptions(Console.Out);
        }

        IEnumerable<Type> examples = h.FindAddins(
          typeof(IExample),
          (t) => { return t.IsDefaultConstructible(); }
        );

        if (list_examples) {
          foreach (Type addin_type in examples) {
            System.Console.WriteLine(addin_type.FullName);
          }
        }

        Type addin = examples.FirstOrDefault(
          (t) => { return t.FullName == example_name;  }
        );

        if (addin != null) {
          IExample example = h.CreateInstance(addin) as IExample;
          example.Run(extra.ToArray());
        }
      } catch (Exception e) {
        Console.WriteLine(e.Message);
        Console.WriteLine();
        opts.WriteOptionDescriptions(Console.Out);
      }
    }
  }
}
