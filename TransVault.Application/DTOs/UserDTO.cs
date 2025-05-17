using System.ComponentModel.DataAnnotations;

namespace TransVault.Application.DTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]

        public string Email { get; set; }
        [Required]
        [MaxLength(30)]
        public string PhoneNumber { get; set; }
        [MaxLength(30)]
        public string? Password { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string? Createdby { get; set; }
    }
}
