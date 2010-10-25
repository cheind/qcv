using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QCV.Base.Compilation;
using System.Reflection;
using System.Text.RegularExpressions;

namespace QCV.Base.Tests {

  [TestFixture]
  class VBCompilerTest {

    [Test]
    public void CanCompile() {
      CompilerSettings s = new CompilerSettings();
      VBCompiler c = new VBCompiler(s);
      Assert.True(c.CanCompileFile(@"..\..\etc\testdata\compiler\compile_ok.vb"));
    }

    [Test]
    public void Compile() {
      CompilerSettings s = new CompilerSettings();
      VBCompiler c = new VBCompiler(s);

      ICompilerResults cr = c.CompileFiles(new string[] { @"..\..\etc\testdata\compiler\compile_ok.vb" });
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

    [Test]
    public void CannotCompile() {
      CompilerSettings s = new CompilerSettings();
      VBCompiler c = new VBCompiler(s);

      ICompilerResults cr = c.CompileFiles(new string[] { @"..\..\etc\testdata\compiler\compile_fail.vb" });
      Assert.False(cr.Success);
      string formatted_erros = cr.GetFormattedErrors();
      Assert.True(formatted_erros.Contains("compile_fail.vb(3,0)"));
      
    }

  }
}
