using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AspNetPracticing.WebAPI.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "{0} can't be blank")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} can't be blank")]
        [EmailAddress(ErrorMessage = "{0} Should be valid format")]
        [Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "{0 is already used}")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} can't be blank")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "{0} can't be blank")]
        [Display(Name = "Confirm Password")]
        [Compare(nameof(Password), ErrorMessage = "{0} and {1} do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
