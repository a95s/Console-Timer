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
        public int saveCounter;
        public int timesRecorded;
        public DateTime lastEdited = new DateTime(2000,1,1);
        public string outputString;

        public GeneralTimer(double interval)
            : base(interval)
        {
            customTimers = new CustomTimer[4] { new CustomTimer("123"), 
                                                new CustomTimer("456"), 
                                                new CustomTimer("789"),
                                                new CustomTimer("0-=") };
            saveCounter = 0;
            timesRecorded = 0;
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
            outputString = "";
            saveCounter++;
            if (saveCounter > 300)
            {
                LoadAndSave.Save(this);
                saveCounter = 0;
                timesRecorded++;
            }
            for (int i = 0; i < customTimers.Length; i++)
            {
                customTimers[i].timeSpan = customTimers[i].timer.Elapsed;
            }

            //Output
            for (int i = 0; i < customTimers.Length; i++)
            {
                outputString += String.Format("{0,10} {1}:{2:d2} {3}\r\n",
                    customTimers[i].name,
                    (int)(customTimers[i].timeSpan.TotalMinutes + customTimers[i].shift.TotalMinutes),
                    customTimers[i].timeSpan.Seconds,
                    customTimers[i].enabled);
            }
            outputString += String.Format("Records: {0}\r\n", timesRecorded);
            Console.WriteLine(outputString);
        }

        public void ResetTimer(int timerIndex)
        {
            TakeSnapshotIfNeededAndChangeTimeOfLastChange();
            customTimers[timerIndex].timer.Reset();
            customTimers[timerIndex].enabled = ' ';
            customTimers[timerIndex].shift = new TimeSpan();
        }

        public void StartOrStopTimer(int timerIndex)
        {
            if (customTimers[timerIndex].timer.IsRunning)
            {
                customTimers[timerIndex].timer.Stop();
                customTimers[timerIndex].enabled = ' ';
            }
            else
            {
                customTimers[timerIndex].timer.Start();
                customTimers[timerIndex].enabled = '*';
            }
            TakeSnapshotIfNeededAndChangeTimeOfLastChange();
        }

        public void TakeSnapshotIfNeededAndChangeTimeOfLastChange()
        {
            if (DateTime.Now.Subtract(lastEdited).TotalMinutes > 1)
                TakeSnapshot();

            lastEdited = DateTime.Now;
        }

        public void TakeSnapshot()
        {
            for (int i = 0; i < customTimers.Length; ++i)
            {
                customTimers[i].snapshot = customTimers[i].timer.Elapsed + customTimers[i].shift;
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
                    customTimers[i].shift = customTimers[i].snapshot;
                }
            }
            TakeSnapshotIfNeededAndChangeTimeOfLastChange();
            this.Start();
        }

        public void AddShiftToTimer(int timerIndex)
        {
            TakeSnapshotIfNeededAndChangeTimeOfLastChange();
            int temp;
            this.Stop();
            Console.Clear();
            Console.WriteLine("Shift " + customTimers[timerIndex].name.Substring(3));
            Int32.TryParse(Console.ReadLine(), out temp);
            if (temp >= 0)
                customTimers[timerIndex].shift = customTimers[timerIndex].shift.Add(new TimeSpan(0, temp, 0));
            else
                customTimers[timerIndex].shift = customTimers[timerIndex].shift.Subtract(new TimeSpan(0, (-1) * temp, 0));
            this.Start();
        }
    }
}
