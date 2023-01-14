using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Reflasher
{
    internal class Writer
    {
        private readonly StatusReporter _reporter;
        private readonly string? _cuLabel;
        private readonly object _lock;

        public Writer(StatusReporter reporter, string? cuLabel, object lockObj)
        {
            _reporter = reporter;
            _cuLabel = cuLabel;
            _lock = lockObj;
        }

        public void Run() => Task.Factory.StartNew(() =>
        {
            var counter = 1;
            while (true)
            {
                counter++;
                lock (_lock)
                {
                    _reporter.Report(Thread.CurrentThread.ManagedThreadId.ToString() + " reporting " + _cuLabel);
                    _reporter.Color(Brushes.Wheat);
                    Thread.Sleep(555);
                }
                _reporter.Color(Brushes.White);
                if (counter % 5 == 0) _reporter.LogClear();
                Thread.Sleep(111);
            }
        });
    }
}
