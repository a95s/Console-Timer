using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace TimerConsole
{
    public class CustomTimer : System.Timers.Timer
    {
        public Stopwatch timer;
        public TimeSpan timeSpan;
        public TimeSpan snapshot;
        public TimeSpan shift;
        public char enabled;
        public string name;

        public CustomTimer(string assignedName)
        {
            name = assignedName;
            timer = new Stopwatch();
            enabled = ' ';
        }
    }
}
