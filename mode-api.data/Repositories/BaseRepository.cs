using Microsoft.EntityFrameworkCore;
using mode_api.Domain;
using mode_api.Domain.DomainModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace mode_api.data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AggregateRoot
    {
        protected readonly ApplicationContext _context;
        public BaseRepository(ApplicationContext context) {
            _context = context;
        }
        public void Add(T entity) {
            _context.Set<T>().Add(entity);
        }
        public void AddRange(IEnumerable<T> entities) {
            _context.Set<T>().AddRange(entities);
        }
        public IQueryable<T> Find(Expression<Func<T, bool>> expression) {
            return _context.Set<T>().Where(expression);
        }
        public IQueryable<T> GetAll() {
            return _context.Set<T>();
        }
        public IQueryable<T> GetByExternalId(Guid externalId) {
            return _context.Set<T>()
                .Where(x => x.ExternalId == externalId);
        }
        public void Remove(T entity) {
            _context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities) {
            _context.Set<T>().RemoveRange(entities);
        }
        public async Task SaveAsync() {
            await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}
