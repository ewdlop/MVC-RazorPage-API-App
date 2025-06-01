using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entities
{
    public class Lesson
    {
        public int LessonId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(2000)]
        public string? Description { get; set; }
        
        public string? Content { get; set; } // Rich text content
        
        [StringLength(500)]
        public string? VideoUrl { get; set; }
        
        public int DurationMinutes { get; set; }
        
        public int OrderIndex { get; set; } // Order within the course
        
        public bool IsPreview { get; set; } = false; // Can be viewed without enrollment
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Key
        public int CourseId { get; set; }
        
        // Navigation Properties
        public Course Course { get; set; } = null!;
        public ICollection<LessonResource> Resources { get; set; } = new List<LessonResource>();
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
        public ICollection<LearningProgress> LearningProgress { get; set; } = new List<LearningProgress>();
    }
    
    public class LessonResource
    {
        public int LessonResourceId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(500)]
        public string Url { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // PDF, Video, Link, etc.
        
        public long FileSize { get; set; } = 0; // in bytes
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Key
        public int LessonId { get; set; }
        
        // Navigation Property
        public Lesson Lesson { get; set; } = null!;
    }
} 