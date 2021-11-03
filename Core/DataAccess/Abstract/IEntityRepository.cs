using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.Abstract
{
    public interface IEntityRepository<T1, T2>
    {
        Task<IEnumerable<T1>> GetAll(String connectionString);
        Task<T1> Get(int id, String connectionString);
        Task<int> Add(T1 entity1, T2 entity2, String connectionString);
        Task<int> Update(T1 entity1, T2 entity2, String connectionString);
        Task<int> Delete(string fileName, String connectionString);
    }
}