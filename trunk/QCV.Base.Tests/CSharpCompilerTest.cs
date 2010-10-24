using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QCV.Base.Compilation;
using System.Reflection;

namespace QCV.Base.Tests {
  
  [TestFixture]
  class CSharpCompilerTest {

    [Test]
    public void CanCompile() {
      CompilerSettings s = new CompilerSettings();
      CSharpCompiler c = new CSharpCompiler(s);
      Assert.True(c.CanCompileFile(@"..\..\etc\testdata\csharp_test.cs"));
    }

    [Test]
    public void Compile() {
      CompilerSettings s = new CompilerSettings();
      CSharpCompiler c = new CSharpCompiler(s);

      ICompilerResults cr = c.CompileFiles(new string[] { @"..\..\etc\testdata\csharp_test.cs" });
      Assert.True(cr.Success);

      Assembly a = cr.GetCompiledAssemblies().First();
      Type t = a.GetType("QCV.Test.TestCompilation");
      Assert.NotNull(t);
      object o = Activator.CreateInstance(t);
      Assert.NotNull(o);
      object result = t.GetMethod("GetString").Invoke(o, null);
      Assert.NotNull(result);
      Assert.AreEqual("Compilation Succeeded", result as string);
    }

  }
}
