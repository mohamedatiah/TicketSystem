using System.ComponentModel.DataAnnotations;
using TransVault.Domain.Common;

namespace TransVault.Domain.Entities
{
    public class User : BaseAuditableEntity
    {

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        [MaxLength(256)]
        public string? Password { get; set; }

        public ICollection<Ticket> CreatedTickets { get; set; }
        public ICollection<Ticket> AssignedTickets { get; set; }

    }
}
