using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.DTOs
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? LongDescription { get; set; }
        public decimal Price { get; set; }
        public int DurationHours { get; set; }
        public string Level { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? PreviewVideoUrl { get; set; }
        public bool IsPublished { get; set; }
        public bool IsFeatured { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? PublishedAt { get; set; }
        
        // Related Data
        public string InstructorName { get; set; } = string.Empty;
        public string CategoryName { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        
        // Statistics
        public double AverageRating { get; set; }
        public int TotalEnrollments { get; set; }
        public int TotalLessons { get; set; }
        public int TotalReviews { get; set; }
        public decimal CompletionRate { get; set; }
        
        // Tags
        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
    
    public class CourseCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Description is required")]
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(5000, ErrorMessage = "Long description cannot exceed 5000 characters")]
        public string? LongDescription { get; set; }
        
        [Required(ErrorMessage = "CategoryId is required")]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Level is required")]
        [RegularExpression("^(Beginner|Intermediate|Advanced)$", ErrorMessage = "Level must be Beginner, Intermediate, or Advanced")]
        public string Level { get; set; } = "Beginner";
        
        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 500, ErrorMessage = "Duration must be between 1 and 500 hours")]
        public int DurationHours { get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 9999.99, ErrorMessage = "Price must be between 0 and 9999.99")]
        public decimal Price { get; set; }
        
        [Url(ErrorMessage = "Image URL must be a valid URL")]
        public string? ImageUrl { get; set; }
        
        [Url(ErrorMessage = "Preview Video URL must be a valid URL")]
        public string? PreviewVideoUrl { get; set; }
        
        public bool IsFeatured { get; set; } = false;
        
        public IEnumerable<string> Tags { get; set; } = new List<string>();
    }
    
    public class CourseUpdateDto : CourseCreateDto
    {
        public bool IsPublished { get; set; }
        public bool IsActive { get; set; } = true;
    }
    
    public class LessonDto
    {
        public int LessonId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? VideoUrl { get; set; }
        public int DurationMinutes { get; set; }
        public int OrderIndex { get; set; }
        public bool IsPreview { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        
        public IEnumerable<LessonResourceDto> Resources { get; set; } = new List<LessonResourceDto>();
        public IEnumerable<QuizDto> Quizzes { get; set; } = new List<QuizDto>();
        
        // User Progress (if authenticated)
        public ProgressStatus? UserProgressStatus { get; set; }
        public int? UserTimeSpent { get; set; }
        public bool? IsBookmarked { get; set; }
    }
    
    public class LessonCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(2000, ErrorMessage = "Description cannot exceed 2000 characters")]
        public string? Description { get; set; }
        
        public string? Content { get; set; }
        
        [Url(ErrorMessage = "Video URL must be a valid URL")]
        public string? VideoUrl { get; set; }
        
        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes")]
        public int DurationMinutes { get; set; }
        
        [Required(ErrorMessage = "Order index is required")]
        public int OrderIndex { get; set; }
        
        public bool IsPreview { get; set; } = false;
        
        [Required(ErrorMessage = "Course ID is required")]
        public int CourseId { get; set; }
    }
    
    public class LessonResourceDto
    {
        public int LessonResourceId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    
    public class QuizDto
    {
        public int QuizId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string Type { get; set; } = string.Empty;
        public int PassingScore { get; set; }
        public int TimeLimit { get; set; }
        public int MaxAttempts { get; set; }
        public bool AllowRetakes { get; set; }
        
        public int CourseId { get; set; }
        public int? LessonId { get; set; }
        
        public IEnumerable<QuizQuestionDto> Questions { get; set; } = new List<QuizQuestionDto>();
        
        // User attempt data (if authenticated)
        public int? UserAttempts { get; set; }
        public int? BestScore { get; set; }
        public bool? HasPassed { get; set; }
    }
    
    public class QuizQuestionDto
    {
        public int QuizQuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string QuestionType { get; set; } = string.Empty;
        public int Points { get; set; }
        public int OrderIndex { get; set; }
        
        public IEnumerable<QuizOptionDto> Options { get; set; } = new List<QuizOptionDto>();
    }
    
    public class QuizOptionDto
    {
        public int QuizOptionId { get; set; }
        public string OptionText { get; set; } = string.Empty;
        public int OrderIndex { get; set; }
        // Note: IsCorrect is not included for security reasons
    }
    
    public class EnrollmentDto
    {
        public int EnrollmentId { get; set; }
        public EnrollmentStatus Status { get; set; }
        public decimal Progress { get; set; }
        public decimal PricePaid { get; set; }
        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime? LastAccessedAt { get; set; }
        public DateTime? CertificateIssuedAt { get; set; }
        public string? CertificateUrl { get; set; }
        
        public CourseDto Course { get; set; } = null!;
    }
    
    public class LearningProgressDto
    {
        public int LearningProgressId { get; set; }
        public ProgressStatus Status { get; set; }
        public int TimeSpentMinutes { get; set; }
        public decimal Score { get; set; }
        public DateTime? StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public DateTime LastAccessedAt { get; set; }
        public string? Notes { get; set; }
        public bool IsBookmarked { get; set; }
        
        public LessonDto Lesson { get; set; } = null!;
    }
    
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? IconUrl { get; set; }
        public string? Color { get; set; }
        public int SortOrder { get; set; }
        
        public int CourseCount { get; set; }
    }
    
    public class UserProfileDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public string? JobTitle { get; set; }
        public string? Company { get; set; }
        public string? Location { get; set; }
        public bool IsInstructor { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // Computed Properties
        public string FullName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        
        // Statistics
        public int CoursesCompleted { get; set; }
        public int TotalLearningHours { get; set; }
        public int AchievementsEarned { get; set; }
        
        public IEnumerable<UserAchievementDto> RecentAchievements { get; set; } = new List<UserAchievementDto>();
        public IEnumerable<UserSkillDto> Skills { get; set; } = new List<UserSkillDto>();
    }
    
    public class UserAchievementDto
    {
        public int UserAchievementId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? BadgeUrl { get; set; }
        public string Type { get; set; } = string.Empty;
        public int Points { get; set; }
        public DateTime EarnedAt { get; set; }
        
        public string? CourseName { get; set; }
    }
    
    public class UserSkillDto
    {
        public int UserSkillId { get; set; }
        public string SkillName { get; set; } = string.Empty;
        public int Level { get; set; }
        public bool IsVerified { get; set; }
        public DateTime AcquiredAt { get; set; }
        public DateTime? VerifiedAt { get; set; }
        
        public string? CourseName { get; set; }
    }
    
    public class CourseReviewDto
    {
        public int CourseReviewId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string UserName { get; set; } = string.Empty;
        public string? UserAvatarUrl { get; set; }
        public bool IsVerifiedPurchase { get; set; }
    }
    
    public class CertificateDto
    {
        public int CertificateId { get; set; }
        public string CertificateNumber { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? CertificateUrl { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        
        public string CourseName { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
    }
} 