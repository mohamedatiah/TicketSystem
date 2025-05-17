using System.Linq.Expressions;

namespace TransfloRepository.Data
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity?> FindAsync(object id)
            => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> FindAllAsync()
            => await _dbSet.ToListAsync();

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
          => await _dbSet.FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
            => await _dbSet.Where(predicate).ToListAsync();

        public async Task<IEnumerable<TEntity>> GetAllAsync()
    => await _dbSet.ToListAsync();

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public void Update(TEntity entity)
            => _dbSet.Update(entity);

        public void Delete(TEntity entity)
            => _dbSet.Remove(entity);
    }
}
