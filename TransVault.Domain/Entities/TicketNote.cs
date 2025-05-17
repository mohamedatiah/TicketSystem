using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TransVault.Domain.Common;

namespace TransVault.Domain.Entities
{
    public class TicketNote : BaseAuditableEntity
    {

        [Required]
        public int TicketId { get; set; }

        [Required]
        public string Note { get; set; }

        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }
    }
}