using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [StringLength(45, MinimumLength = 1)]
        public string Username { get; set; } = "";

        [Required]
        [StringLength(45, MinimumLength = 1)]
        public string Password { get; set; } = "";
    }
}
