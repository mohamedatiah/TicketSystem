namespace TransVault.Application.DTOs
{
    public class TicketNoteDTO
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Note { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }
}
