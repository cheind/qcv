To compile QCV from source make sure your system meets the following prerequisites

  * Microsoft Visual Studio 2010 Express ([download link](http://www.microsoft.com/express/Downloads/#2010-All)) or any other 2010 version
  * Microsoft Visual C++ 2008 SP1 Redistributable Package x86 ([download link](http://www.microsoft.com/downloads/en/details.aspx?familyid=a5c84275-3b97-4ab7-a40d-3802b2af5fc2&displaylang=en))

In case you intend to contribute to QCV you want to install
  * StyleCop v4.4 ([download link](http://stylecop.codeplex.com/)) to verify compliance with QCVs coding and documentation standards
  * WiX v3.5 ([download link](http://wix.sourceforge.net/releases/3.5.2215.0/)) or higher to build the installers.

QCV uses NUnit for testing purposes. A compatible Version for .NET 4.0 is committed to the repository. Therefore, there is no need to install NUnit separately.

The source code can grabbed from the projects [subversion repository](http://code.google.com/p/qcv/source/checkout). The main development happens on the trunk.

Once you have your local checkout of the source, build QCV by
  * opening <tt>qcv.sln</tt>,
  * selecting <tt>x86</tt> platform, and
  * build either in <tt>Release</tt> or <tt>Debug</tt> mode.

Optionally you can invoke the build via <tt>msbuild</tt> scripts that can be found in the <tt>etc/msbuild</tt> directory. Note that these scripts will build the entire solution including the installer projects.