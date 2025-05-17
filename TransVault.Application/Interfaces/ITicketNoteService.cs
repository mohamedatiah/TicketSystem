
namespace FutureWorkshopTicketSystem.Application.Interfaces
{
    public interface ITicketNoteService
    {
        Task<IEnumerable<TicketNoteDTO>> GetNotesByTicketId(int ticketId);
        Task<TicketNoteDTO> GetNote(int id);
        Task<TicketNoteDTO> AddNote(TicketNoteDTO dto);
        Task UpdateNote(TicketNoteDTO dto);
        Task<bool> DeleteNote(int id);
    }
}
