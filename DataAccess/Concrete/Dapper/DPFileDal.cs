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
    public class DPFileDal : IFileDal
    {
        IConfiguration configuration;
        SqlConnection sqlConnection;
        public DPFileDal(IConfiguration configuration) 
        {
           this.configuration = configuration;
           this.sqlConnection = new SqlConnection(configuration.GetConnectionString(@"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FileWatcher;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
        }

        public void Add(FileModel entity)
        {

                sqlConnection.Open();
                sqlConnection.Query<FileModel>(@"INSERT INTO File(Id, ChangeType, Sha512, FileName) VALUES(@Id, @ChangeType, @Sha512, @FileName)",entity);
                sqlConnection.Close();


            

            //_sqlConnection.Open();
            //string sqlquery = @"INSERT INTO FileWatcher(Id, ChangeType, FileName, Sha512) VALUES(@Id, @ChangeType, @FileName, @Sha512)";


            // var result = _sqlConnection.Execute(sqlquery, new
            // {
            //     entity.Id,
            //     entity.ChangeType,
            //     entity.FileName,
            //     entity.Sha512
            // });

            //_sqlConnection.Insert(entity);
        }

        public void Delete(FileModel entity)
        {
            sqlConnection.Open();
            sqlConnection.Query<FileModel>(@"DELETE FROM File WHERE Id= 1");
            sqlConnection.Close();
        }

        public FileModel Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<FileModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Update(FileModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
