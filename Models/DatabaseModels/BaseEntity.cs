using System.ComponentModel.DataAnnotations;

namespace Models.DatabaseModels
{
    /// <summary>
    /// Base wrapper for entities with related Properties
    /// </summary>
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string UUID { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime DateCreated  { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedDate { get; set; }
    }
}
