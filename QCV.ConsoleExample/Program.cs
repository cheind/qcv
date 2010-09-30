using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Emgu.CV;
using Emgu.CV.Structure;
using QCV.Base;
using QCV.Sources;
using QCV.Base.Extensions;
using System.Drawing;
using System.Diagnostics;

namespace QCV.ConsoleExample {
  [Serializable]
  class Program {

    static void Main(string[] args) {
      Camera c = new Camera();
      c.Name = "input 1";
      c.DeviceIndex = 0;
      c.FrameWidth = 320;

      Video v = new Video();
      v.Name = "input 2";
      v.VideoPath = "Wildlife.wmv";
      v.Loop = true;
      c.FrameWidth = 320;
      c.FrameHeight = 200;

      ImageList img = new ImageList();
      img.Name = "input 3";
      img.DirectoryPath = ".";
      img.FilePattern = "*.png";
      img.Loop = true;

      Stopwatch watch = new Stopwatch();
      watch.Start();

      FilterList f = new FilterList();
      //f.Add(c.Frame);
      f.Add(v);
      //f.Add(img.Frame);
      /*f.Add(
        (b) => {
          System.Console.WriteLine("Frame received");
          return true;
        }
      );*/
      /*f.Add(
        (b, ev) => {
          Image<Bgr, byte> i = b.FetchImage("input 1");
          Runtime r = b.FetchRuntime();
          r.Show("a", i, true);
        }
      );*/
      f.Add(
        new AnonymousFilter(
          (b, ev) => {
            Image<Bgr, byte> i = b.FetchImage("input 2");
            Runtime r = b.FetchRuntime();
            r.Show("b", i, false);
          }
        )
      );
      /*
      f.Add(
        (b) => {
          watch.Stop();
          Console.Write("FPS {0} \r", 1.0 / watch.Elapsed.TotalSeconds);
          watch.Reset();
          watch.Start();
          return true;
        }
      );
      /*
      f.Add(
        (b) => {
          Image<Bgr, byte> i = b.FetchImage("input 3");

          IRuntime r = b.FetchRuntime();
          r.Show("c", i, true);
          return true;
        }
      );*/


      Runtime runtime = new Runtime();
      runtime.FPS = 25.0;
      runtime.Run(f, 10);

    }
  }
}
