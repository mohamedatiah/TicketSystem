using AutoMapper;
using FutureWorkshopTicketSystem.Domain.Common;

namespace FutureWorkshopTicketSystem.Application.Services
{
    public class UserService : IUserService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<UserDTO> AddUser(UserDTO createDTO)
        {
            User User = _mapper.Map<User>(createDTO);
            var addedUser=await _unitOfWork.Repository<User>().AddAsync(User);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserDTO>(addedUser);
        }
        public async Task UpdateUser(UserDTO updateDTO)
        {
            var user = await _unitOfWork.Repository<User>().FindAsync(updateDTO.Id);
            if (user == null) throw new BaseFutureWorkshopTicketSystemException($"User with id=${updateDTO.Id} not found");

            _mapper.Map(updateDTO, user);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task<bool> DeleteUser(int id)
        {
            var User = await _unitOfWork.Repository<User>().FindAsync(id);
            if (User == null) throw new BaseFutureWorkshopTicketSystemException($"User with id=$ {id} not found");
            _unitOfWork.Repository<User>().Delete(User);
            await _unitOfWork.SaveChangesAsync();

            return true;//deleted
        }
        public async Task<UserDTO> GetUser(int id)
        {
            var User = await _unitOfWork.Repository<User>().FindAsync(id);
            if (User == null) return null;
            var res = _mapper.Map<UserDTO>(User);
            return res;
        }
        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var User = await _unitOfWork.Repository<User>().GetAsync(a => a.Email == email);
            if (User == null) return null;
            var res = _mapper.Map<UserDTO>(User);
            return res;
        }
        public async Task<bool> IsEmailExist(UserDTO createDTO)
        {
            var res = await _unitOfWork.Repository<User>().GetAsync(u => u.Email.ToLower() == createDTO.Email.ToLower()) != null;
            return res;
        }

        public async Task<UserDTO> ValidateUserAsync(string email, string password)
        {
            var user = await _unitOfWork.Repository<User>().GetAsync(u => u.Email.ToLower() == email.ToLower() && u.Password == password);
           var userDto= _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            var Users = await _unitOfWork.Repository<User>().GetAllAsync();
            var res = _mapper.Map<IEnumerable<UserDTO>>(Users);
            return res;
        }
    }
}
