using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab_4.Domain.Abstraction
{
    public interface IRepository<T>
    {
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> SetAsync(T entity);
    }
}