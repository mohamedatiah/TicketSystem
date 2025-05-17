using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TransVault.Domain.Common;

namespace TransVault.Domain.Entities
{
    public class Ticket : BaseAuditableEntity
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public TicketStatus Status { get; set; } = TicketStatus.Open;

        [Required]
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;

        public int? AssignedToUserId { get; set; }

        public ICollection<TicketNote> Notes { get; set; }
        public ICollection<Attachment> Attachments { get; set; }
    }

}
