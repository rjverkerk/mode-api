using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mode_platonic_api.data.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {
        Task<IEnumerable<T>> GetByExternalIds(IEnumerable<Guid> externalIds);
        Task<IEnumerable<T>> GetAll();
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task SaveAsync();
    }
}
