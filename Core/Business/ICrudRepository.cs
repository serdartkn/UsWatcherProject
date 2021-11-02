using Core.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Business
{
    public interface ICrudRepository<T> where T : class, IEntity, new()
    {
        Task<IEnumerable<T>> GetAll(string connectionString);
        Task<T> GetById(int id, string connectionString);
        void Add(T entity, string connectionString);
        Task<int> Update(T entity, string connectionString);
        Task<int> Delete(T entity, string connectionString);
    }
}
