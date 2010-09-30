﻿using System;
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
using QCV.Toolbox.Sources;
using NDesk.Options;

namespace QCV.ConsoleExample {
  [Serializable]
  class Program {

    static void Main(string[] args) {

      Base.Addins.AddinStore.Discover();

      string example_name = "";
      bool help = false;
      bool list_examples = false;

      OptionSet opts = new OptionSet() {
        { "l|list=", "list all examples.", v => list_examples = v != null},
        { "e|example=", "{EXAMPLE} class to execute", v => example_name = v},
        { "h|help", "print this help message", v => help = v != null }
      };

      try {
        List<string> extra = opts.Parse(args);

        if (help) {
          opts.WriteOptionDescriptions(Console.Out);
        }

        IEnumerable<Base.Addins.AddinInfo> examples = Base.Addins.AddinStore.FindAddins(
          typeof(IExample),
          (ai) => { return ai.DefaultConstructible; }
        );

        if (list_examples) {
          foreach (Base.Addins.AddinInfo ai in examples) {
            System.Console.WriteLine(ai.FullName);
          }
        }

        Base.Addins.AddinInfo info = examples.FirstOrDefault(
          (ai) => { return ai.FullName == example_name;  }
        );

        if (info != null) {
          IExample example = Base.Addins.AddinStore.CreateInstance(info) as IExample;
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
