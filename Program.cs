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
/*Project of a timer.
 * Timer has 3 timers inside, 2 of them (second and third) can shift their time.
 * Hotkeys: 1-3: start/stop, 4-6: shift , 7-9: reset.
 * Shifts are stacking.
*/
namespace TimerConsole
{
    class MyTimer : System.Timers.Timer
    {
        public MyTimer(double interval)
            : base(interval)
        {
            timer1 = new Stopwatch();
            timer2 = new Stopwatch();
            timer3 = new Stopwatch();

            enabled1 = ' ';
            enabled2 = ' ';
            enabled3 = ' ';

            shift1 = 0;
            shift2 = 0;
            shift3 = 0;
        }

        public static Stopwatch timer1;//147Basic
        public static Stopwatch timer2;//285Good
        public static Stopwatch timer3;//396Happiness

        public static TimeSpan ts1;
        public static TimeSpan ts2;
        public static TimeSpan ts3;

        public static char enabled1;
        public static char enabled2;
        public static char enabled3;

        public static int shift1;
        public static int shift2;
        public static int shift3;

        public static void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Console.Clear();

            ts1 = timer1.Elapsed;
            ts2 = timer2.Elapsed;
            ts3 = timer3.Elapsed;

            //Output
            Console.WriteLine("147Basic      {0}:{1:d2} {2}", ts1.Days * 1440 + ts1.Hours * 60 + ts1.Minutes + shift1, ts1.Seconds, enabled1);
            Console.WriteLine("285Good       {0}:{1:d2} {2}", ts2.Days * 1440 + ts2.Hours * 60 + ts2.Minutes + shift2, ts2.Seconds, enabled2);
            Console.WriteLine("396Happiness  {0}:{1:d2} {2}", ts3.Days * 1440 + ts3.Hours * 60 + ts3.Minutes + shift3, ts3.Seconds, enabled3);

        }//t_Elapsed
    }//MyTimer
    class MyTimerConsole
    {

        static void Main(string[] args)
        {
            //Setting size and position of the window
            Console.SetWindowSize(24, 8);
            Console.SetWindowPosition(0, 0);

            //Character for instruction and parsing
            char ch;
            int temp;

            //Initializing timer with 1 second timeout
            MyTimer t = new MyTimer(1000);

            //Hook up method to the event
            t.Elapsed += (sender, e) => MyTimer.t_Elapsed(sender, e);

            //Alive all the time
            GC.KeepAlive(t);

            //On
            t.Enabled = true;

            while (true)
            {
                ch = Console.ReadKey().KeyChar;
                switch (ch)
                {
                    //1-3 Start/stop timers
                    //7-9 Reset specified timer
                    //4-6 Set additional shift to timers (they're stacking)

                    //Start/stop 147Basic timer
                    case '1':
                        if (MyTimer.timer1.IsRunning)
                        {
                            MyTimer.timer1.Stop();
                            MyTimer.enabled1 = ' ';
                        }
                        else
                        {
                            MyTimer.timer1.Start();
                            MyTimer.enabled1 = '*';
                        }
                        break;
                    //Start/stop 285Good timer
                    case '2':
                        if (MyTimer.timer2.IsRunning)
                        {
                            MyTimer.timer2.Stop();
                            MyTimer.enabled2 = ' ';
                        }
                        else
                        {
                            MyTimer.timer2.Start();
                            MyTimer.enabled2 = '*';
                        }
                        break;
                    //Start/stop 396Happiness timer
                    case '3':
                        if (MyTimer.timer3.IsRunning)
                        {
                            MyTimer.timer3.Stop();
                            MyTimer.enabled3 = ' ';
                        }
                        else
                        {
                            MyTimer.timer3.Start();
                            MyTimer.enabled3 = '*';
                        }
                        break;

                    //Resetting timers
                    case '7':
                        MyTimer.timer1.Reset();
                        MyTimer.enabled1 = ' ';
                        MyTimer.shift1 = 0;
                        break;
                    case '8':
                        MyTimer.timer2.Reset();
                        MyTimer.enabled2 = ' ';
                        MyTimer.shift2 = 0;
                        break;
                    case '9':
                        MyTimer.timer3.Reset();
                        MyTimer.enabled3 = ' ';
                        MyTimer.shift3 = 0;
                        break;

                    //Adding shift for 147Basic timer
                    case '4':
                        t.Stop();
                        Console.Clear();
                        Console.WriteLine("Shift Basic");
                        Int32.TryParse(Console.ReadLine(), out temp);
                        MyTimer.shift1 += temp;
                        t.Start();
                        break;
                    //Adding shift for 285Good timer
                    case '5':
                        t.Stop();
                        Console.Clear();
                        Console.WriteLine("Shift Good");
                        Int32.TryParse(Console.ReadLine(), out temp);
                        MyTimer.shift2 += temp;
                        t.Start();
                        break;
                    //Adding shift for 396Happiness timer
                    case '6':
                        t.Stop();
                        Console.Clear();
                        Console.WriteLine("Shift Happi");
                        Int32.TryParse(Console.ReadLine(), out temp);
                        MyTimer.shift3 += temp;
                        t.Start();
                        break;
                }//case
            }//while
        }//Main
    }//MyTimerConsole
}//namespace
