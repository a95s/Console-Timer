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
            Console.SetWindowSize(25, 8);
            Console.SetWindowPosition(0, 0);

            // Character for instruction and parsing
            char ch;

            // Initializing timer with 1 second timeout and load data
            GeneralTimer t = new GeneralTimer(1000);
            LoadAndSave.Load(t);

            // Hook up method to the event
            t.Elapsed += (sender, e) => t.t_Elapsed(sender, e);

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
                        t.StartOrStopTimer(0);
                        break;
                    case '2':
                        t.StartOrStopTimer(1);
                        break;
                    case '3':
                        t.StartOrStopTimer(2);
                        break;
                    case '/':
                        t.StartOrStopTimer(3);
                        break;

                    case '7':
                        t.ResetTimer(0);
                        break;
                    case '8':
                        t.ResetTimer(1);
                        break;
                    case '9':
                        t.ResetTimer(2);
                        break;
                    case '-':
                        t.ResetTimer(3);
                        break;

                    case '4':
                        t.AddShiftToTimer(0);
                        break;
                    case '5':
                        t.AddShiftToTimer(1);
                        break;
                    case '6':
                        t.AddShiftToTimer(2);
                        break;
                    case '*':
                        t.AddShiftToTimer(3);
                        break;

                    //Retrieve snapshot
                    case '0':
                        t.Stop();
                        Console.Clear();
                        Console.WriteLine("Rewind to snapshot?(y/n)");
                        string s = Console.ReadLine();
                        if (s == "y")
                        {
                            for (int i = 0; i < t.customTimers.Length; i++)
                            {
                                t.customTimers[i].timer.Reset();
                                t.customTimers[i].enabled = ' ';
                                t.customTimers[i].shift += t.customTimers[i].snapshot;
                            }
                        }
                        t.Start();
                        break;
                }// case
            }// while
            #endregion
        }// Main
        
    }// MyTimerConsole
}// namespace
