using System;
using System.Configuration;
using CopyBot;

namespace TimerTeste
{
    internal class Program
    {
        private static System.Timers.Timer aTimer;


        static void Main(string[] args)
        {
            aTimer = new System.Timers.Timer();
            aTimer.Interval = double.Parse(ConfigurationManager.AppSettings["timer"]);
            aTimer.Elapsed += CopyFile.OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;

            string dateTeste = DateTime.Now.ToString("yyyyMMdd");
            Console.WriteLine("Press the Enter key to exit the program at any time... " + dateTeste);
            Console.ReadLine();
        }
    }

}