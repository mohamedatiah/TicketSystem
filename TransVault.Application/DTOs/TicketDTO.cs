namespace FutureWorkshopTicketSystem.Application.DTOs
{
    public class TicketDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public TicketStatus Status { get; set; }

        public TicketPriority Priority { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

        public int? AssignedToUserId { get; set; }
    }
}
