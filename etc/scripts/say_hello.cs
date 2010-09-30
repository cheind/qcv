using System;

namespace Scripts {

  [QCV.Base.Addins.Addin]
  public class SayHello : QCV.Base.IFilter {
    public void Execute(QCV.Base.Bundle b, System.ComponentModel.CancelEventArgs e) {
      System.Console.WriteLine("Hello From SayHello filter.");
      e.Cancel = true;
    }
  }
}