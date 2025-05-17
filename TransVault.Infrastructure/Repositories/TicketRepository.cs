
using TransfloRepository.Repository;
using TransfloTicket.Models;
using TransfloUser.Data;
using TransfloUser.Repository.IRepostiory;

namespace TransfloUser.Repository
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
