using Core.DataAccess.Abstract;
using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper
{
    public class DPFileModelDal : IFileModelDal
    {
        private readonly string storageLocation = @"C:\Users\Nilvera\source\repos\UsWatcherProject\WebAPI\wwwroot\";
        public async Task<int> Add(FileDbModel fileDbModel, FileFolderModel fileFolderModel, string connectionString)
        {
            try
            {
                var filePath = this.storageLocation + fileFolderModel.FileName;
                File.WriteAllBytes(filePath, string.IsNullOrEmpty(fileFolderModel.FileContent) ? new byte[0] : Convert.FromBase64String(fileFolderModel.FileContent));

                var sql = "INSERT INTO FileModel (ChangeType, FileContentHash, FileContentSalt, FileName, Date) Values (@ChangeType ,@FileContentHash, @FileContentSalt, @FileName, @Date);";
                using (var connection = new SqlConnection(connectionString)) 
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, fileDbModel);
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> Delete(string fileName, string connectionString)
        {
            try
            {
                var filePath = storageLocation + fileName;
                File.Delete(filePath);

                var sql = "DELETE FROM FileModel WHERE FileName = @FileName";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, new { FileName = fileName });
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            /*
            try
            {
                string storageLocation = @"C:\Users\Nilvera\source\repos\NilveraFileWatcherApp\WebAPI\yedek\";
                var filePath = storageLocation + entity.FileName;
                System.IO.File.Delete(filePath);

                var sql = "DELETE FROM FileModel WHERE FileName = @FileName";
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    var affectedRows = await connection.ExecuteAsync(sql, new { FileName = entity.FileName});
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            */
        }

        public Task<FileDbModel> Get(int id, string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FileDbModel>> GetAll(string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(FileDbModel entity, FileFolderModel fileFolderModel, string connectionString)
        {
            if (entity.ChangeType == "Changed")
            {
                try
                {
                    var filePath = storageLocation + entity.FileName;
                    File.WriteAllBytes(filePath, string.IsNullOrEmpty(fileFolderModel.FileContent) ? new byte[0] : Convert.FromBase64String(fileFolderModel.FileContent));


                    var sql = "UPDATE FileModel SET ChangeType = @ChangeType, FileContentHash = @FileContentHash, FileContentSalt = @FileContentSalt, Date = @Date WHERE FileName = @FileName";
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        var affectedRows = await connection.ExecuteAsync(sql, entity);
                        return affectedRows;
                    }

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                try
                {
                    var filePath = storageLocation + entity.FileName;
                    File.WriteAllBytes(filePath, string.IsNullOrEmpty(fileFolderModel.FileContent) ? new byte[0] : Convert.FromBase64String(fileFolderModel.FileContent));
                    var sql = "UPDATE FileModel SET ChangeType = @ChangeType, FileName = @FileName WHERE FileContentHash = @FileContentHash";
                    using (var connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        var affectedRows = await connection.ExecuteAsync(sql, entity);
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
}
