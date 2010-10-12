using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace QCV {
  public class HookableTextWriter : TextWriter {

    public delegate void StringAppendedEventHandler(object sender, string text);
    public event StringAppendedEventHandler StringAppendedEvent;

    public HookableTextWriter() 
    {}

    public override System.Text.Encoding Encoding { 
      get { return System.Text.Encoding.Default; } 
    }

    public override void Write(string str)
    {
      if (StringAppendedEvent != null) {
        StringAppendedEvent(this, str);
      }
    }

    public override void WriteLine(string value)
    {
      Write(value + Environment.NewLine);
    }

    public override void Write(bool value) { this.Write(value.ToString()); }
    public override void Write(char value) { this.Write(value.ToString()); }
    public override void Write(char[] buffer) { this.Write(new string(buffer)); }
    public override void Write(char[] buffer, int index, int count) { this.Write(new string(buffer, index, count)); }
    public override void Write(decimal value) { this.Write(value.ToString()); }
    public override void Write(double value) { this.Write(value.ToString()); }
    public override void Write(float value) { this.Write(value.ToString()); }
    public override void Write(int value) { this.Write(value.ToString()); }
    public override void Write(long value) { this.Write(value.ToString()); }
    public override void Write(string format, object arg0) { this.WriteLine(string.Format(format, arg0)); }
    public override void Write(string format, object arg0, object arg1) { this.WriteLine(string.Format(format, arg0, arg1)); }
    public override void Write(string format, object arg0, object arg1, object arg2) { this.WriteLine(string.Format(format, arg0, arg1, arg2)); }
    public override void Write(string format, params object[] arg) { this.WriteLine(string.Format(format, arg)); }
    public override void Write(uint value) { this.WriteLine(value.ToString()); }
    public override void Write(ulong value) { this.WriteLine(value.ToString()); }
    public override void Write(object value) { this.WriteLine(value.ToString()); }
    public override void WriteLine() { this.Write(Environment.NewLine); }
    public override void WriteLine(bool value) { this.WriteLine(value.ToString()); }
    public override void WriteLine(char value) { this.WriteLine(value.ToString()); }
    public override void WriteLine(char[] buffer) { this.WriteLine(new string(buffer)); }
    public override void WriteLine(char[] buffer, int index, int count) { this.WriteLine(new string(buffer, index, count)); }
    public override void WriteLine(decimal value) { this.WriteLine(value.ToString()); }
    public override void WriteLine(double value) { this.WriteLine(value.ToString()); }
    public override void WriteLine(float value) { this.WriteLine(value.ToString()); }
    public override void WriteLine(int value) { this.WriteLine(value.ToString()); }
    public override void WriteLine(long value) { this.WriteLine(value.ToString()); }
    public override void WriteLine(string format, object arg0) { this.WriteLine(string.Format(format, arg0)); }
    public override void WriteLine(string format, object arg0, object arg1) { this.WriteLine(string.Format(format, arg0, arg1)); }
    public override void WriteLine(string format, object arg0, object arg1, object arg2) { this.WriteLine(string.Format(format, arg0, arg1, arg2)); }
    public override void WriteLine(string format, params object[] arg) { this.WriteLine(string.Format(format, arg)); }
    public override void WriteLine(uint value) { this.WriteLine(value.ToString()); }
    public override void WriteLine(ulong value) { this.WriteLine(value.ToString()); }
    public override void WriteLine(object value) { this.WriteLine(value.ToString()); }
  }
}
