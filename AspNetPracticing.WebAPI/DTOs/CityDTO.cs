using System.ComponentModel.DataAnnotations;

namespace AspNetPracticing.WebAPI.DTOs
{
    public class CityDTO
    {
        [Required(ErrorMessage = "{0} can't be blank")]
        public string Name { get; set; } = string.Empty;
    }
}
