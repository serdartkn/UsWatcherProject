using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FileWatcherService
{
    class FileWatcher
    {
        Timer _timer = new Timer();

        public void Start()
        {
            WriteToFile("Service starts at " + DateTime.Now);
            _timer.Elapsed += new ElapsedEventHandler(onElapsedTime);
            _timer.Enabled = true;
            _timer.Interval = 1000;
        }

        private void onElapsedTime(object sender, ElapsedEventArgs e)
        {
            WriteToFile("Service recalls at " + DateTime.Now);
        }

        public void Stop()
        {
            WriteToFile("Service stops at " + DateTime.Now);
        }

        private void WriteToFile(string message)
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "ServiceLogPath";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = folderPath + @"\" + "logfile" + ".txt";

            if (!File.Exists(filePath))
            {
                using (StreamWriter streamWriter = File.CreateText(filePath))
                {
                    streamWriter.WriteLine(message);
                }
            }
            else
            {
                using (StreamWriter streamWriter = File.AppendText(filePath))
                {
                    streamWriter.WriteLine(message);
                }
            }
        }

    }
}