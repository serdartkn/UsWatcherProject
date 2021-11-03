using Business.Abstract;
using Core.Utilities.Hashing;
using Dapper;
using DataAccess.Abstract;
using DataAccess.Concrete.Dapper;
using Entity.Concrete;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
            try
            {
                FileInfo fileInfo = new FileInfo(e.FullPath);
                delete(fileInfo.Name);
                WriteFileLogToFile($"{e.ChangeType} > " + $"{e.Name}" + " - " + DateTime.Now + $"{System.Environment.NewLine}" + "*********************************");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void DirectoryRenamed(object sender, RenamedEventArgs e)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(e.FullPath);
                byte[] fileContentByte = File.ReadAllBytes(e.FullPath);
                var fileContentBase64 = Convert.ToBase64String(fileContentByte);

                byte[] fileContentHash, fileContentSalt;
                HashingHelper.CreateContentHash(fileContentByte, out fileContentHash, out fileContentSalt);

                update(e.ChangeType.ToString(), fileContentHash, fileContentSalt, fileInfo.Name, fileContentBase64);
                WriteFileLogToFile($"{e.ChangeType} > " + "OLD NAME: " + $"{e.OldName} " + "NEW NAME: " + $"{e.Name} / " + DateTime.Now + $"{ System.Environment.NewLine}" + "************************************");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void DirectoryChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(e.FullPath);
                byte[] fileContentByte = File.ReadAllBytes(e.FullPath);
                var fileContentBase64 = Convert.ToBase64String(fileContentByte);

                byte[] fileContentHash, fileContentSalt;
                HashingHelper.CreateContentHash(fileContentByte, out fileContentHash, out fileContentSalt);

                switch (e.ChangeType)
                {
                    case WatcherChangeTypes.Created:
                        add(e.ChangeType.ToString(), fileContentHash, fileContentSalt, fileInfo.Name, fileContentBase64);
                        WriteFileLogToFile($"{e.ChangeType} > " + $"{e.Name}" + " - " + DateTime.Now + $"{System.Environment.NewLine}" + "*********************************");
                        break;
                    case WatcherChangeTypes.Changed:
                        update(e.ChangeType.ToString(), fileContentHash, fileContentSalt, fileInfo.Name, fileContentBase64);
                        WriteFileLogToFile($"{e.ChangeType} > " + $"{e.Name}" + " - " + DateTime.Now + $"{System.Environment.NewLine}" + "*********************************");
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void update(string changeType, byte[] fileContentHash, byte[] fileContentSalt, string fileName, string fileContentBase64)
        {
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("changeType", changeType),
                        new KeyValuePair<string, string>("fileContentHash", Convert.ToBase64String(fileContentHash)),
                        new KeyValuePair<string, string>("fileContentSalt", Convert.ToBase64String(fileContentSalt)),
                        new KeyValuePair<string, string>("fileName", fileName),
                        new KeyValuePair<string, string>("fileContentBase64", fileContentBase64)
                    });
            var result = _httpClient.PostAsync("https://localhost:44376/api/Files/update", content).Result;
        }

        public void add(string changeType, byte[] fileContentHash, byte[] fileContentSalt, string fileName, string fileContentBase64)
        {            
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("changeType", changeType),
                        new KeyValuePair<string, string>("fileContentHash", Convert.ToBase64String(fileContentHash)),
                        new KeyValuePair<string, string>("fileContentSalt", Convert.ToBase64String(fileContentSalt)),
                        new KeyValuePair<string, string>("fileName", fileName),
                        new KeyValuePair<string, string>("fileContentBase64", fileContentBase64)
                    });
            var result = _httpClient.PostAsync("https://localhost:44376/api/Files/add", content).Result;




            /*
            FileDbModel fileDbModel = new FileDbModel
            {
                ChangeType = changeType,
                FileContentHash = fileContentHash,
                FileContentSalt = fileContentSalt,
                FileName =  fileName
            };
            var myContent = JsonConvert.SerializeObject(fileDbModel);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var resultDb = _httpClient.PostAsync("https://localhost:44376/api/Files/addDatabase", byteContent).Result;
        */
            }

        /*
        public void saveFolder(string fileContentBase64, string fileName)
        {
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("fileContent", fileContentBase64),
                        new KeyValuePair<string, string>("fileName", fileName)
                    });
            var result = _httpClient.PostAsync("https://localhost:44376/api/Files/addFolder", content).Result;
        }
        */

        public void delete(string fileName)
        {
            var content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>>
                    {
                        new KeyValuePair<string, string>("fileName", fileName)
                    });
            var result = _httpClient.PostAsync("https://localhost:44376/api/Files/delete", content).Result;
        }

        public void Stop()
        {
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