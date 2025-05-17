using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FutureWorkshopTicketSystem.Domain.Common;

namespace FutureWorkshopTicketSystem.Domain.Entities
{
    public class Attachment : BaseAuditableEntity
    {
        public int TicketId { get; set; }

        [Required]
        public string FileName { get; set; }

        public string FilePath { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }
    }
}
