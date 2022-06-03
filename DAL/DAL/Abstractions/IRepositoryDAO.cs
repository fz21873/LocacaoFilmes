

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;

namespace DAL.Abstractions
{
    public interface IRepositoryDAO<T> 
    {
        Task<IList<T>> FindAll();
        Task<T> FindById(params object[] key);
        Task<bool> Create(T model,bool save);

        Task<bool> Update(T model, bool save);

        Task<bool> Delete(T model, bool save);
    }
}
