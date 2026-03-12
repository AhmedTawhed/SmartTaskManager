using Microsoft.EntityFrameworkCore;
using SmartTaskManager.Core.Helpers.CustomRequests;
using SmartTaskManager.Core.Helpers.CustomResults;
using SmartTaskManager.Core.Interfaces.IRepository;
using SmartTaskManager.Infrastructure.Data;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace SmartTaskManager.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly TaskDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(TaskDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAll() => await _dbSet.ToListAsync();
        public async Task<T?> GetById(Guid id) => await _dbSet.FindAsync(id);
        public async Task Add(T entity) => await _dbSet.AddAsync(entity);
        public void Update(T entity) => _dbSet.Update(entity);
        public void Delete(Guid id) { var entity = _dbSet.Find(id); if (entity != null) _dbSet.Remove(entity); }

        public async Task<PagedList<T>> GetPage(GridRequest request, Expression<Func<T, bool>>? filter = null)
        {
            var query = _dbSet.AsQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                var text = request.SearchText.Trim().ToLower();

                var stringProperties = typeof(T).GetProperties()
                    .Where(p => p.PropertyType == typeof(string));

                if (stringProperties.Any())
                {
                    var searchQuery = string.Join(" OR ",
                        stringProperties.Select(p => $"({p.Name} != null && {p.Name}.ToLower().Contains(@0))"));
                    query = query.Where(searchQuery, text);
                }
            }

            var validColumns = typeof(T).GetProperties().Select(p => p.Name).ToList();

            if (!string.IsNullOrEmpty(request.SortColumn) &&
                validColumns.Contains(request.SortColumn))
            {
                var direction = request.SortDirection?.ToLower() == "desc" ? "desc" : "asc";
                query = query.OrderBy($"{request.SortColumn} {direction}");
            }

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync();

            return new PagedList<T>
            {
                Items = items,
                TotalCount = totalCount,
                NumberOfPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }
    }
}