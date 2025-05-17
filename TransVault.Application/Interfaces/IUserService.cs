
namespace TransVault.Application.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> AddUser(UserDTO createDTO);
        public Task UpdateUser(UserDTO updateDTO);
        public Task<bool> DeleteUser(int id);
        public Task<UserDTO> GetUser(int id);
        public Task<UserDTO> GetUserByEmail(string email);
        public Task<UserDTO> ValidateUserAsync(string email, string password);

        public Task<bool> IsEmailExist(UserDTO createDTO);
        public Task<IEnumerable<UserDTO>> GetAll();


    }
}
