using Core.DataAccess.Abstract;
using Dapper;
using Dapper.Contrib.Extensions;
using DataAccess.Abstract;
using Entity.Concrete;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper
{
    public class DPFileModelDal : IFileModelDal
    {
        public async Task<int> Add(FileModel entity, string connectionString)
        {
            var sql = "INSERT INTO FileModel (ChangeType, FileContentHash, FileContentSalt, FileName, Date) Values (@ChangeType ,@FileContentHash, @FileContentSalt, @FileName, @Date);";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public async Task<int> Delete(FileModel entity, string connectionString)
        {
            var sql = "DELETE FROM FileModel WHERE Name (@Name);";
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var affectedRows = await connection.ExecuteAsync(sql, entity);
                return affectedRows;
            }
        }

        public Task<FileModel> Get(int id, string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FileModel>> GetAll(string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(FileModel entity, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
