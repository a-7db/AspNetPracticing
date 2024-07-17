using System.ComponentModel.DataAnnotations;

namespace AspNetPracticing.WebAPI.Models
{
    public class City
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "{0} can't be blank")]
        public string Name { get; set; } = string.Empty;
    }
}
