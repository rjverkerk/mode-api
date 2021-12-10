using Microsoft.EntityFrameworkCore;
using mode_api.Domain;
using mode_api.Domain.DomainModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mode_api.data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AggregateRoot
    {
        protected readonly ApplicationContext _context;
        private readonly DbSet<T> _items;
        public BaseRepository(ApplicationContext context) {
            _context = context;
            _items = _context.Set<T>();
        }

        public void Add(T entity) {
            _items.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities) {
            _items.AddRange(entities);
        }

        public async Task<IEnumerable<T>> GetAll() {
            return await _items.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByExternalIds(IEnumerable<Guid> externalIds) {
            return await _items.Where(x => externalIds.Contains(x.ExternalId))
                               .ToListAsync();
        }

        public void Remove(T entity) {
            _items.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities) {
            _items.RemoveRange(entities);
        }

        public async Task SaveAsync() {
            await _context.SaveChangesAsync();
        }

        public void Dispose() {
            _context.Dispose();
        }
    }
}
