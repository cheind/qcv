This page contains notes for developers of QCV.



# Visual Studio Express Notes #

## Enabling Platform and Configuration Selection Toolbar ##

To enable the toolbar you need to switch to the [expert mode](http://blogs.msdn.com/b/nicgrave/archive/2010/06/19/platform-and-configuration-selection-in-visual-studio-2010-express-for-windows-phone.aspx).

# NUnit Notes #

## Compatibility with .NET 4.0 ##

In order to successfully run unit tests created with NUnit 2.5.7 on .NET 4.0, you need to update the application runners configuration file to contain

```
<?xml version="1.0" encoding="utf-8"?>
<configuration>

  <startup>
    <requiredRuntime version="v4.0" />
  </startup>

  <runtime>
    <loadFromRemoteSources enabled="true" />
  </runtime>

</configuration>
```

<tt>QCV.TestsRunner</tt> already contains those changes.