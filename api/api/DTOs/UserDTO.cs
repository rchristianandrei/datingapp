using System.ComponentModel.DataAnnotations;

namespace api.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; } = "";

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Gender { get; set; } = "";

        [Required]
        public int Age { get; set; } = 0;

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Introduction { get; set; } = "";

        [Required]
        public string LookingFor { get; set; } = "";

        [Required]
        public string Interests { get; set; } = "";

        [Required]
        public string City { get; set; } = "";

        [Required]
        public string Country { get; set; } = "";

        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }
    }
}
