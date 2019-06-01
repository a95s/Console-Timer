# Console stopwatch
It has three timers.

7 8 9 - clear (shift are cleared too)  
4 5 6 - add a shift (shifts are stackable)  
1 2 3 - start/stop the corresponding timer  

Timers are implemented with System.Diagnostics.Stopwatch, so if the timer is started, it will continue counting even when computer is in sleep mode.

Main things I've done in this project:
1. Deepened my understanding of time tracking in C#. It turns out usual ticking mechanisms are not precise, so I had to use the _Stopwatch_ class.
2. Understood how to work with several threads, as I used it to separate the display and the timers.
