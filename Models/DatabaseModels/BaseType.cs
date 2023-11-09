using System.ComponentModel.DataAnnotations;

namespace Models.DatabaseModels
{
    public class BaseType : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
