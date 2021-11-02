using Business.Abstract;
using Core.Utilities.Hashing;
using Dapper;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using Entity.Concrete;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FileWatcherService
{
    public class FileWatcher
    {
        private Timer timer;
        private FileSystemWatcher fileSystemWatcher;
        static HttpClient _httpClient = new HttpClient();

        public void Start()
        {
            this.timer = new Timer();
            WriteRunLogToFile("Service starts at " + DateTime.Now);
            this.timer.Elapsed += new ElapsedEventHandler(onElapsedTime);
            this.timer.Enabled = true;
            this.timer.Interval = 1000;

            this.fileSystemWatcher = new FileSystemWatcher("C:\\deneme")
            {
                EnableRaisingEvents = true,
                IncludeSubdirectories = true,
            };
            this.fileSystemWatcher.Created += DirectoryChanged;
            this.fileSystemWatcher.Deleted += DirectoryDeleted;
            this.fileSystemWatcher.Renamed += DirectoryRenamed;
            this.fileSystemWatcher.Changed += DirectoryChanged;
        }

        private void DirectoryDeleted(object sender, FileSystemEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(e.FullPath);
            delete(fileInfo.Name);
            WriteFileLogToFile($"{e.ChangeType} > " + $"{e.Name}" + " - " + DateTime.Now + $"{System.Environment.NewLine}" + "*********************************");
        }

        private void DirectoryRenamed(object sender, RenamedEventArgs e)
        {
            WriteFileLogToFile($"{e.ChangeType} > " + "OLD NAME: " + $"{e.OldName} " + "NEW NAME: " + $"{e.Name} / " + DateTime.Now + $"{ System.Environment.NewLine}" + "************************************");
        }

        private void DirectoryChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(e.FullPath);
                byte[] fileContent = File.ReadAllBytes(e.FullPath);

                byte[] fileContentHash, fileContentSalt;
                HashingHelper.CreateContentHash(fileContent, out fileContentHash, out fileContentSalt);

                if (e.ChangeType == WatcherChangeTypes.Created)
                {
                    saveFolder(fileContent, fileInfo.Name);
                    saveDb(e.ChangeType.ToString(), fileContentHash, fileContentSalt, fileInfo.Name);
                    WriteFileLogToFile($"{e.ChangeType} > " + $"{e.Name}" + " - " + DateTime.Now + $"{System.Environment.NewLine}" + "*********************************");
                }
                else if (e.ChangeType.ToString() == "Changed")
                {
                    update(fileContentHash, fileContentSalt, fileInfo.Name);
                    WriteFileLogToFile($"{e.ChangeType} > " + $"{e.Name}" + " - " + DateTime.Now + $"{System.Environment.NewLine}" + "*********************************");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            

        }

        public void update(byte[] fileContentHash, byte[] fileContentSalt, string fileName)
        {
            var AddDbContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("fileContentHash", Convert.ToBase64String(fileContentHash)),
                        new KeyValuePair<string, string>("fileContentSalt", Convert.ToBase64String(fileContentSalt)),
                        new KeyValuePair<string, string>("fileName", fileName)
                    });
            var resultDb = _httpClient.PostAsync("https://localhost:44338/api/Files/update", AddDbContent).Result;
        }

        public void saveDb(string changeType, byte[] fileContentHash, byte[] fileContentSalt, string fileName)
        {
            var AddDbContent = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("changeType", changeType),
                        new KeyValuePair<string, string>("fileContentHash", Convert.ToBase64String(fileContentHash)),
                        new KeyValuePair<string, string>("fileContentSalt", Convert.ToBase64String(fileContentSalt)),
                        new KeyValuePair<string, string>("fileName", fileName)
                    });
            var resultDb = _httpClient.PostAsync("https://localhost:44376/api/Files/addDatabase", AddDbContent).Result;
        }

        public void saveFolder(byte[] fileContent, string fileName)
        {
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("fileContent", Convert.ToBase64String(fileContent)),
                        new KeyValuePair<string, string>("fileName", fileName)
                    });
            var result = _httpClient.PostAsync("https://localhost:44376/api/Files/addFolder", content).Result;
        }

        public void delete(string fileName)
        {
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("fileName", fileName)
                    });
            var result = _httpClient.PostAsync("https://localhost:44338/api/Files/delete", content).Result;
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