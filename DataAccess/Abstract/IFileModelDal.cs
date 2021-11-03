using Core.DataAccess.Abstract;
using Entity;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IFileModelDal : IEntityRepository<FileDbModel, FileFolderModel>
    {    
    }
}