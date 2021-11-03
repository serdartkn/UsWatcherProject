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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly string connectionString = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog=FileWatcher;Integrated Security=True";
        IFileModelService fileModelService;
        public FilesController(IFileModelService fileModelService)
        {
            this.fileModelService = fileModelService;
        }

        
        [HttpPost]
        public void add([FromForm] string changeType, [FromForm] string fileContentHash, [FromForm] string fileContentSalt, [FromForm] string fileName, [FromForm] string fileContentBase64)
        {
            byte[] fileContentHashByte = Encoding.ASCII.GetBytes(fileContentHash);
            byte[] fileContentSaltByte = Encoding.ASCII.GetBytes(fileContentSalt);

            FileDbModel fileDbModel = new FileDbModel
            {
                ChangeType = changeType,
                FileContentHash = fileContentHashByte,
                FileContentSalt = fileContentSaltByte,
                FileName = fileName,
                Date = DateTime.Now
            };
            FileFolderModel fileFolderModel = new FileFolderModel 
            {
                FileContent = fileContentBase64,
                FileName = fileName
            };
            fileModelService.Add(fileDbModel, fileFolderModel, connectionString);
        }

        [HttpPost]
        public void delete([FromForm] string fileName)
        {
            fileModelService.Delete(fileName, connectionString);
        }

        [HttpPost]
        public void Update([FromForm] string changeType, [FromForm] string fileContentHash, [FromForm] string fileContentSalt, [FromForm] string fileName, [FromForm] string fileContentBase64)
        {
            byte[] fileContentHashByte = Encoding.ASCII.GetBytes(fileContentHash);
            byte[] fileContentSaltByte = Encoding.ASCII.GetBytes(fileContentSalt);

            FileDbModel fileDbModel = new FileDbModel
            {
                ChangeType = changeType,
                FileContentHash = fileContentHashByte,
                FileContentSalt = fileContentSaltByte,
                FileName = fileName,
                Date = DateTime.Now
            };

            FileFolderModel fileFolderModel = new FileFolderModel
            {
                FileContent = fileContentBase64,
                FileName = fileName
            };
            fileModelService.Update(fileDbModel, fileFolderModel, connectionString);
        }
    }
}