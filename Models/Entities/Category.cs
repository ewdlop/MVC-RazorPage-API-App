using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [StringLength(500)]
        public string? IconUrl { get; set; }
        
        [StringLength(50)]
        public string? Color { get; set; } // For UI theming
        
        public bool IsActive { get; set; } = true;
        public int SortOrder { get; set; } = 0;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation Properties
        public ICollection<Course> Courses { get; set; } = new List<Course>();
    }
} 