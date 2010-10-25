using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using QCV.Base.Compilation;

namespace QCV.Base.Tests {

  [TestFixture]
  class AggregateCompilerTest {

    [Test]
    public void CompileMultiple() {
      AggregateCompiler a = new AggregateCompiler(new CompilerSettings());
      a.AddCompiler(typeof(CSharpCompiler));
      a.AddCompiler(typeof(VBCompiler));

      Assert.True(a.CanCompileFile(@"..\..\etc\testdata\compiler\compile_ok.cs"));
      Assert.True(a.CanCompileFile(@"..\..\etc\testdata\compiler\compile_ok.vb"));

      ICompilerResults cr = a.CompileFiles(
        new string[] 
          { @"..\..\etc\testdata\compiler\compile_ok.cs", 
            @"..\..\etc\testdata\compiler\compile_ok.vb"
          }
      );

      Assert.True(cr.Success);
      Assert.AreEqual(2, cr.GetCompiledAssemblies().Count());

    }

    [Test]
    public void CannotCompileMultiple() {
      AggregateCompiler a = new AggregateCompiler(new CompilerSettings());
      a.AddCompiler(typeof(CSharpCompiler));
      a.AddCompiler(typeof(VBCompiler));

      Assert.True(a.CanCompileFile(@"..\..\etc\testdata\compiler\compile_ok.cs"));
      Assert.True(a.CanCompileFile(@"..\..\etc\testdata\compiler\compile_ok.vb"));
      Assert.False(a.CanCompileFile(@"..\..\etc\testdata\compiler\compile_ok.rb"));

      Assert.Throws<ArgumentException>(new TestDelegate(() => {
        ICompilerResults cr = a.CompileFiles(
          new string[] 
            { @"..\..\etc\testdata\compiler\compile_ok.cs", 
              @"..\..\etc\testdata\compiler\compile_ok.vb",
              @"..\..\etc\testdata\compiler\compile_ok.rb"
            }
        );
      }));
    }
  }
}
