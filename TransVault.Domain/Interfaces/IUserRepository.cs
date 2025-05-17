
using TransVault.Domain.Entities;

namespace TransVault.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> GetUserByEmail(string email);
    }
}
