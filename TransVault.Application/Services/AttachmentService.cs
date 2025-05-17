

using AutoMapper;

namespace FutureWorkshopTicketSystem.Application.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AttachmentDTO> AddAttachment(AttachmentDTO dto)
        {
            var model = _mapper.Map<Attachment>(dto);
            var addedAttachment=await _unitOfWork.Repository<Attachment>().AddAsync(model);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<AttachmentDTO>(addedAttachment);
        }

        public async Task<bool> DeleteAttachment(int id)
        {
            var model = await _unitOfWork.Repository<Attachment>().FindAsync(id);
            if (model == null) return false;

            _unitOfWork.Repository<Attachment>().Delete(model);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<AttachmentDTO> GetAttachment(int id)
        {
            var attachment = await _unitOfWork.Repository<Attachment>().FindAsync(id);
            return _mapper.Map<AttachmentDTO>(attachment);
        }

        public async Task<IEnumerable<AttachmentDTO>> GetAttachmentsByTicketId(int ticketId)
        {
            var attachments = await _unitOfWork.Repository<Attachment>().GetAllAsync(a => a.TicketId == ticketId);
            return _mapper.Map<IEnumerable<AttachmentDTO>>(attachments);
        }
    }
}
