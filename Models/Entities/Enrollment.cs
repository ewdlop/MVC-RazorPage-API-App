using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models.Entities
{
    public enum EnrollmentStatus
    {
        Active,
        Completed,
        Suspended,
        Cancelled,
        Expired
    }
    
    public enum ProgressStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Skipped
    }
    
    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal Progress { get; set; } = 0; // Percentage (0-100)
        
        [Column(TypeName = "decimal(10,2)")]
        public decimal PricePaid { get; set; } = 0;
        
        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public DateTime? LastAccessedAt { get; set; }
        public DateTime? CertificateIssuedAt { get; set; }
        
        [StringLength(500)]
        public string? CertificateUrl { get; set; }
        
        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;
        public int CourseId { get; set; }
        
        // Navigation Properties
        public IdentityUser User { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public ICollection<LearningProgress> LearningProgress { get; set; } = new List<LearningProgress>();
    }
    
    public class LearningProgress
    {
        public int LearningProgressId { get; set; }
        
        public ProgressStatus Status { get; set; } = ProgressStatus.NotStarted;
        
        public int TimeSpentMinutes { get; set; } = 0;
        
        [Column(TypeName = "decimal(5,2)")]
        public decimal Score { get; set; } = 0; // For assessments
        
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime LastAccessedAt { get; set; } = DateTime.UtcNow;
        
        public string? Notes { get; set; } // Student notes
        public bool IsBookmarked { get; set; } = false;
        
        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;
        public int LessonId { get; set; }
        public int? EnrollmentId { get; set; }
        
        // Navigation Properties
        public IdentityUser User { get; set; } = null!;
        public Lesson Lesson { get; set; } = null!;
        public Enrollment? Enrollment { get; set; }
    }
    
    public class CourseReview
    {
        public int CourseReviewId { get; set; }
        
        [Range(1, 5)]
        public int Rating { get; set; }
        
        [StringLength(1000)]
        public string? Comment { get; set; }
        
        public bool IsApproved { get; set; } = false;
        public bool IsVisible { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ApprovedAt { get; set; }
        
        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;
        public int CourseId { get; set; }
        
        // Navigation Properties
        public IdentityUser User { get; set; } = null!;
        public Course Course { get; set; } = null!;
    }
    
    public class Certificate
    {
        public int CertificateId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string CertificateNumber { get; set; } = string.Empty;
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [StringLength(500)]
        public string? CertificateUrl { get; set; }
        
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ExpiresAt { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public int? EnrollmentId { get; set; }
        
        // Navigation Properties
        public IdentityUser User { get; set; } = null!;
        public Course Course { get; set; } = null!;
        public Enrollment? Enrollment { get; set; }
    }
} 