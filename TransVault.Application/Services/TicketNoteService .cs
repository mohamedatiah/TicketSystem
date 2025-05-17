using AutoMapper;
using TransVault.Domain.Common;

namespace TransVault.Application.Services
{
    public class TicketNoteService : ITicketNoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TicketNoteService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TicketNoteDTO> AddNote(TicketNoteDTO dto)
        {
            var note = _mapper.Map<TicketNote>(dto);
            var addedTicketNote = await _unitOfWork.Repository<TicketNote>().AddAsync(note);
            await _unitOfWork.SaveChangesAsync();
           return   _mapper.Map<TicketNoteDTO>(addedTicketNote);
        }

        public async Task UpdateNote(TicketNoteDTO dto)
        {
            var note = await _unitOfWork.Repository<TicketNote>().FindAsync(dto.Id);
            if (note == null) throw new BaseTransVaultException($"Ticket Note with id=${dto.Id} not found");

            _mapper.Map(dto, note);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> DeleteNote(int id)
        {
            var note = await _unitOfWork.Repository<TicketNote>().FindAsync(id);
            if (note == null) throw new BaseTransVaultException($"Ticket Note with id=${id} not found");
            _unitOfWork.Repository<TicketNote>().Delete(note);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<TicketNoteDTO> GetNote(int id)
        {
            var note = await _unitOfWork.Repository<TicketNote>().FindAsync(id);
            if(note==null) throw new BaseTransVaultException($"Ticket Note with id=${id} not found");
            return _mapper.Map<TicketNoteDTO>(note);
        }

        public async Task<IEnumerable<TicketNoteDTO>> GetNotesByTicketId(int ticketId)
        {
            var notes = await _unitOfWork.Repository<TicketNote>().GetAllAsync(n => n.TicketId == ticketId);
            return _mapper.Map<IEnumerable<TicketNoteDTO>>(notes);
        }
    }
}
