
namespace TransfloRepository.Repository
{
    public class AttachmentRepository : IGenericRepository<Attachment>
    {
        Task<List<Order>> GetOrdersWithItemsAsync();
    }
}
