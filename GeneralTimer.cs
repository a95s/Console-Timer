using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerConsole
{
    public class GeneralTimer : System.Timers.Timer
    {
        public static CustomTimer[] customTimers;

        public GeneralTimer(double interval)
            : base(interval)
        {
            customTimers = new CustomTimer[4] { new CustomTimer("147Box"), 
                                                new CustomTimer("258Rest"), 
                                                new CustomTimer("369Other1"),
                                                new CustomTimer("/*-Other2") };
        }

        public static void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.Clear();

            for (int i = 0; i < customTimers.Length; i++)
            {
                customTimers[i].timeSpan = customTimers[i].timer.Elapsed;
            }

            //Output
            for (int i = 0; i < customTimers.Length; i++)
            {
                Console.WriteLine("{0,11} {1}:{2:d2} {3}",
                    customTimers[i].name,
                    customTimers[i].timeSpan.Days * 1440 + customTimers[i].timeSpan.Hours * 60 + customTimers[i].timeSpan.Minutes + (customTimers[i].shift.Days * 1440 + customTimers[i].shift.Hours * 60 + customTimers[i].shift.Minutes),
                    customTimers[i].timeSpan.Seconds + customTimers[i].shift.Seconds,
                    customTimers[i].enabled);
            }
        }
    }
}
