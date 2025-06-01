using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models.Entities
{
    public class Course
    {
        public int CourseId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(5000)]
        public string? LongDescription { get; set; }
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        
        public int DurationHours { get; set; }
        
        [Required]
        [StringLength(20)]
        public string Level { get; set; } = "Beginner"; // Beginner, Intermediate, Advanced
        
        [StringLength(500)]
        public string? ImageUrl { get; set; }
        
        [StringLength(500)]
        public string? PreviewVideoUrl { get; set; }
        
        public bool IsPublished { get; set; } = false;
        public bool IsFeatured { get; set; } = false;
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? PublishedAt { get; set; }
        
        // Foreign Keys
        [Required]
        public string InstructorId { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        
        // Navigation Properties
        public IdentityUser Instructor { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<CourseReview> Reviews { get; set; } = new List<CourseReview>();
        public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
        
        // Computed Properties
        [NotMapped]
        public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
        
        [NotMapped]
        public int TotalEnrollments => Enrollments.Count(e => e.Status == EnrollmentStatus.Active);
        
        [NotMapped]
        public int TotalLessons => Lessons.Count;
        
        [NotMapped]
        public bool IsFree => Price == 0;
    }
} 