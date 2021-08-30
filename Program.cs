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
            // Set size and position of the window
            Console.SetWindowSize(25, 7);
            Console.SetWindowPosition(0, 0);

            // Character for instruction and parsing
            char ch;

            // Initialize timer with a 200ms timeout and load data
            GeneralTimer t = new GeneralTimer(200);
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
                    case '1':
                        t.StartOrStopTimer(0);
                        break;
                    case '4':
                        t.StartOrStopTimer(1);
                        break;
                    case '7':
                        t.StartOrStopTimer(2);
                        break;
                    case '0':
                        t.StartOrStopTimer(3);
                        break;

                    case '3':
                        t.ResetTimer(0);
                        break;
                    case '6':
                        t.ResetTimer(1);
                        break;
                    case '9':
                        t.ResetTimer(2);
                        break;
                    case '=':
                        t.ResetTimer(3);
                        break;

                    case '2':
                        t.AddShiftToTimer(0);
                        break;
                    case '5':
                        t.AddShiftToTimer(1);
                        break;
                    case '8':
                        t.AddShiftToTimer(2);
                        break;
                    case '-':
                        t.AddShiftToTimer(3);
                        break;

                    //Retrieve snapshot
                    case '`':
                        t.RetrieveSnapshot();
                        break;
                }// case
            }// while
            #endregion
        }// Main
        
    }// MyTimerConsole
}// namespace
