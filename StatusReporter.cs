using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Reflasher
{
    class StatusReporter : IProgress<string>
    {
        public StatusReporter(Action<string> status, Action<Brush> color, Action clear)
        {
            OnProgress = status;
            OnBrush = color;
            OnClear = clear;
        }

        private event Action<string>? OnProgress;
        private event Action<Brush>? OnBrush;
        private event Action OnClear;

        public void Report(string value) => OnProgress?.Invoke(value);
        public void Color(Brush color) => OnBrush?.Invoke(color);
        public void LogClear() => OnClear?.Invoke();
    }
}
