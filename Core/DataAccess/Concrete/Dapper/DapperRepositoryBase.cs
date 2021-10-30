using Core.DataAccess.Abstract;
using Core.Entity.Abstract;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Concrete.Dapper
{
    public abstract class DapperRepositoryBase<TEntity>// IEntityRepository<TEntity> where TEntity : class
    {
        //private readonly IConfiguration _configuration;
        //private readonly SqlConnection sqlConnection;
        //private bool _disposed = false;
        //public DapperRepositoryBase(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //    sqlConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        //}

        //public DapperRepositoryBase() 
        //{
        //    Dispose();
        //}

        //public void Dispose()
        //{
        //    if (Equals(!_disposed))
        //    {
        //        sqlConnection.Close();
        //        sqlConnection.Dispose();
        //        _disposed = true;
        //    }
        //    GC.SuppressFinalize(this);
        //}        

        //public void Add(TEntity entity)
        //{
        //    sqlConnection.Insert(entity);
        //}

        //public void Delete(TEntity entity)
        //{
        //    sqlConnection.Delete(entity);
        //}

        //public void Update(TEntity entity)
        //{
        //    sqlConnection.Update(entity);
        //}

        //public virtual List<TEntity> GetAll()
        //{
        //    throw new NotImplementedException();
        //}

        //public TEntity Get(int id)
        //{
        //    throw new NotImplementedException();
        //}
        
    }
}