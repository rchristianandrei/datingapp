using System.ComponentModel.DataAnnotations;

namespace api.Controllers.Accounts
{
    public class LoginDTO
    {
        [Required]
        [StringLength(45, MinimumLength = 1)]
        public string Username { get; set; } = "";

        [Required]
        [StringLength(45, MinimumLength = 1)]
        public string Password { get; set; } = "";
    }
}
