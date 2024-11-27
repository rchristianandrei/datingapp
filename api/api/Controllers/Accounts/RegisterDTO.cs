using System.ComponentModel.DataAnnotations;

namespace api.Controllers.Accounts
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(45, MinimumLength = 6)]
        public string? Username { get; set; }

        [Required]
        [StringLength(45, MinimumLength = 6)]
        public string? Password { get; set; }
    }
}
