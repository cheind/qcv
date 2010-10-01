using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.CodeDom.Compiler;
using log4net;
using System.Reflection;
using System.Security.Policy;

namespace QCV.ConsoleExample {

  [Base.Addins.Addin]
  public class HelloScriptingExchange : IExample {
    public void Run(string[] args) {
      /*

      //Create evidence for the new appdomain from evidence of the current application domain
      Evidence adevidence = AppDomain.CurrentDomain.Evidence;

      AppDomain d = AppDomain.CreateDomain("test", adevidence, AppDomain.CurrentDomain.SetupInformation);
      Console.WriteLine(d.BaseDirectory);

      d.DoCallBack(new CrossAppDomainDelegate(delegate()
	        {
            QCV.Base.Scripting s = new QCV.Base.Scripting();
            bool success = s.Compile(
              new string[] { @"..\..\etc\scripts\say_hello.cs" },
              new string[] { "System.dll" });
	        }));
       * 
       */
    }
  }
}

