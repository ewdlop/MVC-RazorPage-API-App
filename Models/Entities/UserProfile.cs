using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models.Entities
{
    public class UserProfile
    {
        public int UserProfileId { get; set; }
        
        [StringLength(100)]
        public string? FirstName { get; set; }
        
        [StringLength(100)]
        public string? LastName { get; set; }
        
        [StringLength(500)]
        public string? Bio { get; set; }
        
        [StringLength(500)]
        public string? AvatarUrl { get; set; }
        
        [StringLength(100)]
        public string? JobTitle { get; set; }
        
        [StringLength(100)]
        public string? Company { get; set; }
        
        [StringLength(100)]
        public string? Location { get; set; }
        
        [StringLength(500)]
        public string? WebsiteUrl { get; set; }
        
        [StringLength(100)]
        public string? LinkedInUrl { get; set; }
        
        [StringLength(100)]
        public string? GitHubUrl { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        [StringLength(10)]
        public string? PreferredLanguage { get; set; } = "en";
        
        [StringLength(50)]
        public string? TimeZone { get; set; }
        
        public bool IsInstructor { get; set; } = false;
        public bool IsEmailNotificationsEnabled { get; set; } = true;
        public bool IsPushNotificationsEnabled { get; set; } = true;
        public bool IsProfilePublic { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Key
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        // Navigation Properties
        public IdentityUser User { get; set; } = null!;
        public ICollection<UserAchievement> Achievements { get; set; } = new List<UserAchievement>();
        public ICollection<UserSkill> Skills { get; set; } = new List<UserSkill>();
        
        // Computed Properties
        [NotMapped]
        public string FullName => $"{FirstName} {LastName}".Trim();
        
        [NotMapped]
        public string DisplayName => !string.IsNullOrEmpty(FullName) ? FullName : User?.UserName ?? "Unknown User";
    }
    
    public class UserAchievement
    {
        public int UserAchievementId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [StringLength(500)]
        public string? BadgeUrl { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = string.Empty; // CourseCompletion, Streak, Milestone, etc.
        
        public int Points { get; set; } = 0;
        
        public DateTime EarnedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;
        public int? CourseId { get; set; }
        
        // Navigation Properties
        public IdentityUser User { get; set; } = null!;
        public Course? Course { get; set; }
    }
    
    public class UserSkill
    {
        public int UserSkillId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string SkillName { get; set; } = string.Empty;
        
        [Range(1, 5)]
        public int Level { get; set; } = 1; // 1-5 proficiency level
        
        public bool IsVerified { get; set; } = false;
        
        public DateTime AcquiredAt { get; set; } = DateTime.UtcNow;
        public DateTime? VerifiedAt { get; set; }
        
        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;
        public int? CourseId { get; set; } // Course that verified this skill
        
        // Navigation Properties
        public IdentityUser User { get; set; } = null!;
        public Course? Course { get; set; }
    }
    
    public class Tag
    {
        public int TagId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(200)]
        public string? Description { get; set; }
        
        [StringLength(50)]
        public string? Color { get; set; }
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Navigation Properties
        public ICollection<CourseTag> CourseTags { get; set; } = new List<CourseTag>();
    }
    
    public class CourseTag
    {
        public int CourseTagId { get; set; }
        
        // Foreign Keys
        public int CourseId { get; set; }
        public int TagId { get; set; }
        
        // Navigation Properties
        public Course Course { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
    
    public class LearningAnalytics
    {
        public int LearningAnalyticsId { get; set; }
        
        [Required]
        [StringLength(50)]
        public string ActionType { get; set; } = string.Empty; // View, Complete, Quiz, Download, etc.
        
        [StringLength(100)]
        public string? ContentType { get; set; } // Lesson, Quiz, Course, etc.
        
        public int? ContentId { get; set; }
        
        public int DurationMinutes { get; set; } = 0;
        
        [StringLength(200)]
        public string? DeviceInfo { get; set; }
        
        [StringLength(50)]
        public string? IPAddress { get; set; }
        
        [StringLength(500)]
        public string? UserAgent { get; set; }
        
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        
        // Foreign Keys
        [Required]
        public string UserId { get; set; } = string.Empty;
        public int? CourseId { get; set; }
        public int? LessonId { get; set; }
        
        // Navigation Properties
        public IdentityUser User { get; set; } = null!;
        public Course? Course { get; set; }
        public Lesson? Lesson { get; set; }
    }
} 