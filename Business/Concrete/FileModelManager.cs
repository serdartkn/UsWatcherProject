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

        public void Add(FileModel entity, string connectionString)
        {
            this.fileModelDal.Add(entity, connectionString);
        }

        public Task<int> Delete(FileModel entity, string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<FileModel>> GetAll(string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<FileModel> GetById(int id, string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(FileModel entity, string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
