using Core.DataAccess.Concrete.Dapper;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Dapper
{
    public class FileDal : DapperRepository<File, FileDbContext>, IFileDal
    {
    }
}
