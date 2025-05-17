namespace FutureWorkshopTicketSystem.Application.DTOs
{
    public class AttachmentDTO
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
    }
}
