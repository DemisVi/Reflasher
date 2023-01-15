using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Management;
using System.Diagnostics;

namespace Reflasher
{
    internal class Writer
    {
        private readonly StatusReporter _reporter;
        private readonly string? _cuName;
        private readonly static object _lock = new();

        private readonly Process flasherProc;

        public Writer(StatusReporter reporter, string? cuName)
        {
            _reporter = reporter;
            _cuName = cuName;
            flasherProc = new()
            {
                StartInfo = new()
                {
                    FileName = "./test.exe",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                },
            };
        }

        public void Run() => Task.Factory.StartNew(Write);

        private void Write()
        {
            var counter = 0;
            ManagementEventWatcher watcher = new();
            while (true)
            {
                counter++;
                lock (_lock)
                {
                    // watcher.Query = WqlQueries.creationTsQuery;
                    // watcher.WaitForNextEvent();
                    // watcher.Stop();
                    flasherProc.Start();
                    _reporter.Report(_cuName + " going report " + (flasherProc.StandardOutput.ReadLine().Contains("0") ? "SUCESS" : "FAIL"));
                    _reporter.Color(Brushes.Wheat);

                    Thread.Sleep(new TimeSpan(0, 0, new Random().Next(10, 20)));

                    _reporter.Color(Brushes.Aqua);
                    // some process launch work

                    // watcher.Query = WqlQueries.deletionTsQuery;
                    // watcher.WaitForNextEvent();
                    // watcher.Stop();
                }
                _reporter.Report(_cuName + " reporting " + (flasherProc.StandardOutput.ReadToEnd().Contains("0") ? "SUCESS" : "FAIL"));
                flasherProc.WaitForExit();
                if (flasherProc.ExitCode != 0) _reporter.Color(Brushes.LightPink);
                else _reporter.Color(Brushes.LightGreen);
                Thread.Sleep(2222);
                _reporter.Color(Brushes.White);
                // if (counter % 2 == 0 && counter > 0) return;
                Thread.Sleep(111);
            }
        }
    }
}
