using Microsoft.EntityFrameworkCore;
using TransVault.Domain.Interfaces;
using TransVault.Infrastructure.Data;

namespace TransVault.Extensions
{
    public static class DatabaseConfigurationExtensions
    {
        public static IServiceCollection AddDatabaseConfiguration(this IServiceCollection services)
        {
            services.AddDbContext<TransVaultDbContext>((provider, options) =>
            {
                var tenantContext = provider.GetRequiredService<ITenantContext>();
                var configuration = provider.GetRequiredService<IConfiguration>();

                var connectionString = configuration.GetConnectionString("TransVault");
                if (!string.IsNullOrEmpty(connectionString))
                {
                    connectionString = connectionString.Replace("{DatabaseName}", tenantContext.RepositoryName);
                }
                else
                {
                    throw new InvalidOperationException("Connection string 'TransVault' not found.");
                }
                var optionsBuilder = options.UseSqlServer(connectionString);
                options.AddInterceptors(new AuditSaveChangesInterceptor());
            });
            return services;
        }
    }
}

