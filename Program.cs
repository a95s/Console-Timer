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
    
    class MyTimerConsole
    {
        static void Main(string[] args)
        {
            #region Set up
            // Setting size and position of the window
            Console.SetWindowSize(24, 8);
            Console.SetWindowPosition(0, 0);

            // Character for instruction and parsing
            char ch;
            int temp;

            // Initializing timer with 1 second timeout
            GeneralTimer t = new GeneralTimer(1000);

            // Hook up method to the event
            t.Elapsed += (sender, e) => GeneralTimer.t_Elapsed(sender, e);

            // Alive all the time
            GC.KeepAlive(t);

            // On
            t.Enabled = true;
            #endregion
            #region In progress
            while (true)
            {
                ch = Console.ReadKey().KeyChar;
                switch (ch)
                {
                    // 1-3 and "/" Start/stop timers
                    // 7-9 and "-" Reset specified timer
                    // 4-6 and "*" Set additional shift to timers (they're stacking)

                    case '1':
                        StartOrStopTimer(0);
                        break;
                    case '2':
                        StartOrStopTimer(1);
                        break;
                    case '3':
                        StartOrStopTimer(2);
                        break;
                    case '/':
                        StartOrStopTimer(3);
                        break;

                    case '7':
                        ResetTimer(0);
                        break;
                    case '8':
                        ResetTimer(1);
                        break;
                    case '9':
                        ResetTimer(2);
                        break;
                    case '-':
                        ResetTimer(3);
                        break;

                    case '4':
                        AddShiftToTimer(t, 0);
                        break;
                    case '5':
                        AddShiftToTimer(t, 1);
                        break;
                    case '6':
                        AddShiftToTimer(t, 2);
                        break;
                    case '*':
                        AddShiftToTimer(t, 3);
                        break;

                    //Retrieve snapshot
                    case '0':
                        t.Stop();
                        Console.Clear();
                        Console.WriteLine("Rewind to snapshot?(y/n)");
                        string s = Console.ReadLine();
                        if (s == "y")
                        {
                            for (int i = 0; i < GeneralTimer.customTimers.Length; i++)
                            {
                                GeneralTimer.customTimers[i].timer.Reset();
                                GeneralTimer.customTimers[i].enabled = ' ';
                                GeneralTimer.customTimers[i].shift += GeneralTimer.customTimers[i].snapshot;
                            }
                        }
                        t.Start();
                        break;
                }// case
            }// while
            #endregion
        }// Main
        //TODO try decrease these long names
        static void StartOrStopTimer(int timerIndex)
        {
            if (GeneralTimer.customTimers[timerIndex].timer.IsRunning)
            {
                GeneralTimer.customTimers[timerIndex].timer.Stop();
                GeneralTimer.customTimers[timerIndex].enabled = ' ';
                GeneralTimer.customTimers[timerIndex].snapshot = GeneralTimer.customTimers[timerIndex].timer.Elapsed;
            }
            else
            {
                GeneralTimer.customTimers[timerIndex].timer.Start();
                GeneralTimer.customTimers[timerIndex].enabled = '*';
                GeneralTimer.customTimers[timerIndex].snapshot = GeneralTimer.customTimers[timerIndex].timer.Elapsed;
            }
        }
        static void ResetTimer(int timerIndex)
        {
            GeneralTimer.customTimers[timerIndex].timer.Reset();
            GeneralTimer.customTimers[timerIndex].enabled = ' ';
            GeneralTimer.customTimers[timerIndex].shift = new TimeSpan();
        }
        static void AddShiftToTimer(GeneralTimer t, int timerIndex)
        {
            int temp;
            t.Stop();
            Console.Clear();
            Console.WriteLine("Shift " + GeneralTimer.customTimers[timerIndex].name.Substring(3));
            Int32.TryParse(Console.ReadLine(), out temp);
            if (temp >= 0)
                GeneralTimer.customTimers[timerIndex].shift = GeneralTimer.customTimers[timerIndex].shift.Add(new TimeSpan(0, temp, 0));
            else
                GeneralTimer.customTimers[timerIndex].shift = GeneralTimer.customTimers[timerIndex].shift.Subtract(new TimeSpan(0, (-1) * temp, 0));
            t.Start();
        }
        static void RetrieveSnapshot(GeneralTimer t)
        {
            t.Stop();
            Console.Clear();
            Console.WriteLine("Rewind to snapshot?(y/n)");
            string s = Console.ReadLine();
            if (s == "y")
            {
                for (int i = 0; i < GeneralTimer.customTimers.Length; i++)
                {
                    GeneralTimer.customTimers[i].timer.Reset();
                    GeneralTimer.customTimers[i].enabled = ' ';
                    GeneralTimer.customTimers[i].shift += GeneralTimer.customTimers[i].snapshot;
                }
            }
            t.Start();
        }
    }// MyTimerConsole
}// namespace
