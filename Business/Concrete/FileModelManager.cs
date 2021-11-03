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
    public class FileModelManager : IFileModelService
    {
        IFileModelDal fileModelDal;
        public FileModelManager(IFileModelDal fileModelDal)
        {
            this.fileModelDal = fileModelDal;
        }

        public void Add(FileDbModel entity1, FileFolderModel entity2, string connectionString)
        {
            fileModelDal.Add(entity1, entity2, connectionString);
        }

        public void Delete(string fileName, string connectionString)
        {
            fileModelDal.Delete(fileName, connectionString);
        }

        public IEnumerable<FileDbModel> GetAll(string connectionString)
        {
            throw new NotImplementedException();
        }

        public FileDbModel GetById(int id, string connectionString)
        {
            throw new NotImplementedException();
        }

        public void Update(FileDbModel entity, FileFolderModel entity2, string connectionString)
        {
            fileModelDal.Update(entity, entity2, connectionString);
        }
    }
}
