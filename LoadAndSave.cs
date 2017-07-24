using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TimerConsole
{
    public static class LoadAndSave
    {
        static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TimerConsole";
        static string fileName = "data.txt";
        
        public static void Load(GeneralTimer loadHere)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            if (!File.Exists(path + "\\" + fileName))
                return;
            StreamReader sr = new StreamReader(path + "\\" + fileName);
            string[] lines = sr.ReadToEnd().Split(new string[1]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            loadHere.ResetInnerTimers();
            if(lines.Length != loadHere.customTimers.Length)
            {
                throw new Exception("Error while loading. Lines count and timers count are different.");
            }
            else
            {
                for(int i = 0; i < lines.Length; ++i)
                {
                    loadHere.customTimers[i].shift = new TimeSpan(Int32.Parse(lines[i]) / 60, Int32.Parse(lines[i]) % 60, 0);
                }
            }
            sr.Close();
        }

        public static void Save(GeneralTimer saveThis)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            StreamWriter sw = new StreamWriter(path + "\\" + fileName);
            for(int i = 0; i < saveThis.customTimers.Length; ++i)
            {
                sw.Write((int)(saveThis.customTimers[i].timeSpan.TotalMinutes + saveThis.customTimers[i].shift.TotalMinutes) + "\r\n");
            }
            sw.Close();
        }
    }
}
