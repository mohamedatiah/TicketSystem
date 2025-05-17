
namespace FutureWorkshopTicketSystem.Application.Interfaces
{
    public interface IAttachmentService
    {
        Task<IEnumerable<AttachmentDTO>> GetAttachmentsByTicketId(int ticketId);
        Task<AttachmentDTO> GetAttachment(int id);
        Task<AttachmentDTO> AddAttachment(AttachmentDTO dto);
        Task<bool> DeleteAttachment(int id);
    }
}
