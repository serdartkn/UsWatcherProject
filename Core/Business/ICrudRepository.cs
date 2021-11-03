using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business
{
    public interface ICrudRepository<T1, T2> where T1 : class, IEntity, new() where T2 : class, IEntity, new()
    {
        IEnumerable<T1> GetAll(string connectionString);
        T1 GetById(int id, string connectionString);
        void Add(T1 entity1, T2 entity2, string connectionString);
        void Update(T1 entity1, T2 entity2, string connectionString);
        void Delete(string fileName, string connectionString);
    }
}
