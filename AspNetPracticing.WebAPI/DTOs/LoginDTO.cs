using System.ComponentModel.DataAnnotations;

namespace AspNetPracticing.WebAPI.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "{0} can't be blank")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} can't be blank")]
        public string Password { get; set; } = string.Empty;
    }
}
