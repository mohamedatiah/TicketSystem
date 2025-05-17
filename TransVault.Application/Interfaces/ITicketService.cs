
namespace TransVault.Application.Interfaces
{

    public interface ITicketService
    {
        public Task<TicketDTO> AddTicket(TicketDTO createDTO);
        public Task UpdateTicket(TicketDTO updateDTO);
        public Task<bool> DeleteTicket(int id);
        public Task<TicketDTO> GetTicket(int id);
        public Task<IEnumerable<TicketDTO>> GetAll(int Createdby);

    }
}
