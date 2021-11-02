using Business.Abstract;
using Dapper;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly string connectionString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=FileWatcher;Integrated Security=True";
        IFileModelService fileModelService;
        public FilesController(IFileModelService fileModelService)
        {
            this.fileModelService = fileModelService;
        }

        private readonly string storageLocation = @"C:\Users\Nilvera\source\repos\NilveraFileWatcherApp\WebAPI\yedek\";
        [HttpPost("addFolder")]
        public void SaveFolder([FromForm] string fileContent, [FromForm] string fileName)
        {
            try
            {
                var filePath = this.storageLocation + fileName;
                System.IO.File.WriteAllBytes(filePath, string.IsNullOrEmpty(fileContent) ? new byte[0] : Convert.FromBase64String(fileContent));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        [HttpPost("addDatabase")]
        public async Task<int> SaveDbAsync([FromForm] string changeType, [FromForm] string fileContentHash, [FromForm] string fileContentSalt, [FromForm] string fileName)
        {
            byte[] fileContentHashByte = Encoding.ASCII.GetBytes(fileContentHash);
            byte[] fileContentSaltByte = Encoding.ASCII.GetBytes(fileContentSalt);

            FileModel fileModel = new FileModel
            {
                ChangeType = changeType,
                FileContentHash = fileContentHashByte,
                FileContentSalt = fileContentSaltByte,
                FileName = fileName,
                Date = DateTime.Now
            };

            try
            {
                var sql = "INSERT INTO FileModel (ChangeType, FileContentHash, FileContentSalt, FileName, Date) Values (@ChangeType ,@FileContentHash, @FileContentSalt, @FileName, @Date);";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, fileModel);
                    return affectedRows;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("delete")]
        public async Task<int> DeleteAsync([FromForm] string fileName)
        {
            try
            {
                var filePath = this.storageLocation + fileName;
                System.IO.File.Delete(filePath);

                var sql = "DELETE FROM FileModel WHERE FileName ="+"'"+fileName+"'";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql);
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("update")]
        public async Task<int> UpdateAsync([FromForm] string fileContentHash, [FromForm] string fileContentSalt, [FromForm] string fileName)
        {
            try
            {
                var sql = "UPDATE FileModel SET FileContentHash = " + "'"+fileContentHash+"' , FileContentSalt = " + "'"+fileContentSalt+"' WHERE FileName = " + "'"+fileName+"'";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql);
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}