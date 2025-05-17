using AutoMapper;
using TransVault.Domain.Common;

namespace TransVault.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TicketDTO> AddTicket(TicketDTO dto)
        {
            var model = _mapper.Map<Ticket>(dto);
            var addedTicket=await _unitOfWork.Repository<Ticket>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<TicketDTO>(addedTicket);
        }

        public async Task UpdateTicket(TicketDTO dto)
        {
            var existing = await _unitOfWork.Repository<Ticket>().FindAsync(dto.Id);
            if (existing == null) throw new BaseTransVaultException($"Ticket with id=${dto.Id} not found");

            _mapper.Map(dto, existing);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteTicket(int id)
        {
            var existing = await _unitOfWork.Repository<Ticket>().FindAsync(id);
            if (existing == null) throw new BaseTransVaultException($"Ticket with id=${id} not found"); ;

            _unitOfWork.Repository<Ticket>().Delete(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<TicketDTO> GetTicket(int id)
        {
            var ticket = await _unitOfWork.Repository<Ticket>().FindAsync(id);
            if(ticket==null) throw new BaseTransVaultException($"Ticket with id=${id} not found");
            return _mapper.Map<TicketDTO>(ticket);
        }

        public async Task<IEnumerable<TicketDTO>> GetAll(int createdByUserId)
        {
            var tickets = await _unitOfWork.Repository<Ticket>().GetAllAsync();
            return _mapper.Map<IEnumerable<TicketDTO>>(tickets);
        }
    }
}
