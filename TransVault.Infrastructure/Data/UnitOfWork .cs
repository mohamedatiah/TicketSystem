using TransfloRepository.Data;

namespace TransVault.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TransVaultDbContext _context;
        private readonly Dictionary<Type, object> _repositories = new();

        public UnitOfWork(TransVaultDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);

            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new GenericRepository<TEntity>(_context);
                _repositories[type] = repoInstance;
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => await _context.SaveChangesAsync(cancellationToken);

        public void Dispose()
            => _context.Dispose();
    }
}