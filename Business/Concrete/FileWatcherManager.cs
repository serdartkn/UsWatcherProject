using Business.Abstract;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FileWatcherManager : IFileWatcherService
    {
        IFileDal _fileDal;
        public FileWatcherManager(IFileDal fileDal) 
        {
            _fileDal = fileDal;
        }
        public void Add(FileModel fileModel)
        {
            this._fileDal.Add(fileModel);
        }

        public void Delete(FileModel fileModel)
        {
            this._fileDal.Delete(fileModel);
        }
    }
}
