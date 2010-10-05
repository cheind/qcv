using System;
using System.Collections.Generic;

namespace Scripts {

  [QCV.Base.Addins.Addin]
  public class SayHello : QCV.Base.IFilter {
    public void Execute(Dictionary<string, object> b) {
      System.Console.WriteLine("Hello From SayHello filter.");
      b["cancel"] = true;
    }
  }
}