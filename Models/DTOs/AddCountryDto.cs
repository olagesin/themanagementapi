using System.ComponentModel.DataAnnotations;

namespace Models.DTOs
{
    public class AddCountryDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
