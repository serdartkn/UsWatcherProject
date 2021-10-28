﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FileWatcherService
{
    public class FileWatcher
    {
        Timer _timer;
        FileSystemWatcher _fileSystemWatcher;

        public void Start()  
        {
            _timer = new Timer();
            WriteRunLogToFile("Service starts at " + DateTime.Now);
            _timer.Elapsed += new ElapsedEventHandler(onElapsedTime);
            _timer.Enabled = true;
            _timer.Interval = 1000;

            _fileSystemWatcher = new FileSystemWatcher("C:\\deneme")
            {
                EnableRaisingEvents = true,
                IncludeSubdirectories = true,
            };
            _fileSystemWatcher.Created += DirectoryChanged;
            _fileSystemWatcher.Deleted += DirectoryChanged;
            _fileSystemWatcher.Renamed += DirectoryRenamed;
            _fileSystemWatcher.Changed += DirectoryChanged;
        }

        private void DirectoryRenamed(object sender, RenamedEventArgs e)
        {
            WriteFileLogToFile($"{e.ChangeType} > " + "OLD NAME: " + $"{e.OldName} " + "NEW NAME: " + $"{e.Name} / " + DateTime.Now + $"{ System.Environment.NewLine}" + "************************************");
        }

        private void DirectoryChanged(object sender, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    WriteFileLogToFile($"{e.ChangeType} > " + $"{e.Name}" + " - " + DateTime.Now + $"{System.Environment.NewLine}" + "*********************************");
                    break;
                case WatcherChangeTypes.Changed:
                    WriteFileLogToFile($"{e.ChangeType} > " + $"{e.Name}" + " - " + DateTime.Now + $"{System.Environment.NewLine}" + "*********************************");
                    break;
                case WatcherChangeTypes.Deleted:
                    WriteFileLogToFile($"{e.ChangeType} > " + $"{e.Name}" + " - " + DateTime.Now + $"{System.Environment.NewLine}" + "*********************************");
                    break;
                default:
                    break;
            }
        }

        private void onElapsedTime(object sender, ElapsedEventArgs e)
        {
            WriteRunLogToFile("Service recalls at " + DateTime.Now);
        }

        public void Stop()
        {
            WriteRunLogToFile("Service stops at " + DateTime.Now);
        }

        private void WriteRunLogToFile(string message)
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

        private void WriteFileLogToFile(string message)
        {
            string folderPath = AppDomain.CurrentDomain.BaseDirectory + "ServiceFilePath";
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