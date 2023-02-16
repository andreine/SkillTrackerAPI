using System.ComponentModel.DataAnnotations;

namespace SkillTracker.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
