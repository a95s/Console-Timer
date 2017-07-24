using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerConsole
{
    public class GeneralTimer : System.Timers.Timer
    {
        public CustomTimer[] customTimers;
        public int counter;

        public GeneralTimer(double interval)
            : base(interval)
        {
            customTimers = new CustomTimer[4] { new CustomTimer("147Deals"), 
                                                new CustomTimer("258YouCanBeatIt"), 
                                                new CustomTimer("369DidntEat"),
                                                new CustomTimer("/*-Awake") };
            counter = 0;
        }

        public void ResetInnerTimers()
        {
            for(int i = 0; i < customTimers.Length; ++i)
            {
                customTimers[i].timer.Reset();
                customTimers[i].timeSpan = new TimeSpan();
                customTimers[i].shift = new TimeSpan();
                customTimers[i].snapshot = new TimeSpan();
                customTimers[i].enabled = ' ';
            }
        }

        public void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.Clear();
            counter++;
            if (counter > 60)
            {
                LoadAndSave.Save(this);
                counter = 0;
            }

            for (int i = 0; i < customTimers.Length; i++)
            {
                customTimers[i].timeSpan = customTimers[i].timer.Elapsed;
            }

            //Output
            for (int i = 0; i < customTimers.Length; i++)
            {
                Console.WriteLine("{0,15} {1}:{2:d2} {3}",
                    customTimers[i].name,
                    (int)(customTimers[i].timeSpan.TotalMinutes + customTimers[i].shift.TotalMinutes),
                    customTimers[i].timeSpan.Seconds + customTimers[i].shift.Seconds,
                    customTimers[i].enabled);
            }
        }

        public void ResetTimer(int timerIndex)
        {
            customTimers[timerIndex].timer.Reset();
            customTimers[timerIndex].enabled = ' ';
            customTimers[timerIndex].shift = new TimeSpan();
            LoadAndSave.Save(this);
        }

        public void StartOrStopTimer(int timerIndex)
        {
            if (customTimers[timerIndex].timer.IsRunning)
            {
                customTimers[timerIndex].timer.Stop();
                customTimers[timerIndex].enabled = ' ';
                customTimers[timerIndex].snapshot = customTimers[timerIndex].timer.Elapsed;
            }
            else
            {
                customTimers[timerIndex].timer.Start();
                customTimers[timerIndex].enabled = '*';
                customTimers[timerIndex].snapshot = customTimers[timerIndex].timer.Elapsed;
            }
        }

        public void RetrieveSnapshot()
        {
            this.Stop();
            Console.Clear();
            Console.WriteLine("Rewind to snapshot?(y/n)");
            string s = Console.ReadLine();
            if (s == "y")
            {
                for (int i = 0; i < customTimers.Length; i++)
                {
                    customTimers[i].timer.Reset();
                    customTimers[i].enabled = ' ';
                    customTimers[i].shift += customTimers[i].snapshot;
                }
            }
            LoadAndSave.Save(this);
            this.Start();
        }

        public void AddShiftToTimer(int timerIndex)
        {
            int temp;
            this.Stop();
            Console.Clear();
            Console.WriteLine("Shift " + customTimers[timerIndex].name.Substring(3));
            Int32.TryParse(Console.ReadLine(), out temp);
            if (temp >= 0)
                customTimers[timerIndex].shift = customTimers[timerIndex].shift.Add(new TimeSpan(0, temp, 0));
            else
                customTimers[timerIndex].shift = customTimers[timerIndex].shift.Subtract(new TimeSpan(0, (-1) * temp, 0));
            LoadAndSave.Save(this);
            this.Start();
        }
    }
}
