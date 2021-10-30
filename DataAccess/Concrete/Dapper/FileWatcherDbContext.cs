using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper
{
    public class FileWatcherDbContext
    {

        
        IDbConnection dbConnection;

        public FileWatcherDbContext(IConfiguration configuration) 
        {
            dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }


    }
}
