using TransfloTicket.Models;
using TransfloUser.Data;
using TransfloUser.Repository.IRepostiory;

namespace TransfloRepository.Repository;

public class TicketNoteRepository : GenericRepository<TicketNote>, ITicketNoteRepository
{
    public TicketNoteRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}
