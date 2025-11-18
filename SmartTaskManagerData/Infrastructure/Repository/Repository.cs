using Microsoft.EntityFrameworkCore;
using SmartTaskManagerCore.Core.Interfaces.IRepository;
using SmartTaskManagerData.Infrastructure.Data;

namespace SmartTaskManagerData.Infrastructure.Repositories
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
        public void Delete(Guid id)
        {
            _dbSet.Remove(_dbSet.Find(id));
        }
    }
}