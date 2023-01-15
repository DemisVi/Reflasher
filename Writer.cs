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

namespace Reflasher
{
    internal class Writer
    {
        private readonly StatusReporter _reporter;
        private readonly string? _cuName;
        private readonly static object _lock = new();

        public Writer(StatusReporter reporter, string? cuName)
        {
            _reporter = reporter;
            _cuName = cuName;
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
                    watcher.Query = WqlQueries.creationTsQuery;
                    watcher.WaitForNextEvent();
                    watcher.Stop();

                    _reporter.Report(Thread.CurrentThread.ManagedThreadId.ToString() + " reporting " + _cuName);
                    _reporter.Color(Brushes.Wheat);

                    // some process launch work

                    watcher.Query = WqlQueries.deletionTsQuery;
                    watcher.WaitForNextEvent();
                    watcher.Stop();
                }
                _reporter.Color(Brushes.White);
                if (counter % 2 == 0 && counter > 0) return;
                Thread.Sleep(111);
            }
        }
    }
}
