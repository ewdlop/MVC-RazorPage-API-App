using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Requests
{
    // Course Search and Filtering
    public class CourseSearchRequest
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public string? Level { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinRating { get; set; }
        public bool FreeOnly { get; set; } = false;
        public bool FeaturedOnly { get; set; } = false;
        public IEnumerable<string>? Tags { get; set; }
        
        public string SortBy { get; set; } = "Title";
        public string SortDirection { get; set; } = "asc";
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 12;
    }
    
    // Learning Progress and Completion
    public class LessonCompletionRequest
    {
        [Required]
        public int LessonId { get; set; }
        
        public int TimeSpentMinutes { get; set; } = 0;
        public string? Notes { get; set; }
        public bool IsBookmarked { get; set; } = false;
    }
    
    public class QuizSubmissionRequest
    {
        [Required]
        public int QuizId { get; set; }
        
        [Required]
        public int QuizAttemptId { get; set; }
        
        [Required]
        public IEnumerable<QuizAnswerSubmission> Answers { get; set; } = new List<QuizAnswerSubmission>();
    }
    
    public class QuizAnswerSubmission
    {
        [Required]
        public int QuizQuestionId { get; set; }
        
        public int? SelectedOptionId { get; set; } // For multiple choice
        public string? AnswerText { get; set; } // For text answers
    }
    
    public class QuizAttemptStartRequest
    {
        [Required]
        public int QuizId { get; set; }
    }
    
    // Enrollment and Course Access
    public class CourseEnrollmentRequest
    {
        [Required]
        public int CourseId { get; set; }
        
        public string? CouponCode { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentToken { get; set; }
    }
    
    // User Profile Management
    public class UpdateProfileRequest
    {
        [StringLength(100)]
        public string? FirstName { get; set; }
        
        [StringLength(100)]
        public string? LastName { get; set; }
        
        [StringLength(500)]
        public string? Bio { get; set; }
        
        [StringLength(100)]
        public string? JobTitle { get; set; }
        
        [StringLength(100)]
        public string? Company { get; set; }
        
        [StringLength(100)]
        public string? Location { get; set; }
        
        [Url]
        public string? WebsiteUrl { get; set; }
        
        [Url]
        public string? LinkedInUrl { get; set; }
        
        [Url]
        public string? GitHubUrl { get; set; }
        
        public DateTime? DateOfBirth { get; set; }
        
        [StringLength(10)]
        public string? PreferredLanguage { get; set; }
        
        [StringLength(50)]
        public string? TimeZone { get; set; }
        
        public bool IsEmailNotificationsEnabled { get; set; } = true;
        public bool IsPushNotificationsEnabled { get; set; } = true;
        public bool IsProfilePublic { get; set; } = true;
    }
    
    public class ChangePasswordRequest
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        
        [Required]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
    
    // Authentication
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        
        public bool RememberMe { get; set; } = false;
    }
    
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        
        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? FirstName { get; set; }
        
        [StringLength(100)]
        public string? LastName { get; set; }
        
        public bool AgreeToTerms { get; set; } = false;
        public bool SubscribeToNewsletter { get; set; } = false;
    }
    
    public class RefreshTokenRequest
    {
        [Required]
        public string AccessToken { get; set; } = string.Empty;
        
        [Required]
        public string RefreshToken { get; set; } = string.Empty;
    }
    
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
    
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string Token { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100, MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; } = string.Empty;
        
        [Required]
        [Compare("NewPassword")]
        [DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
    
    // Course Reviews and Ratings
    public class CourseReviewRequest
    {
        [Required]
        public int CourseId { get; set; }
        
        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }
        
        [StringLength(1000)]
        public string? Comment { get; set; }
    }
    
    // File Upload
    public class FileUploadRequest
    {
        [Required]
        public IFormFile File { get; set; } = null!;
        
        [Required]
        public string Type { get; set; } = string.Empty; // "avatar", "course-image", "lesson-video", etc.
        
        public int? CourseId { get; set; }
        public int? LessonId { get; set; }
    }
    
    // Analytics and Tracking
    public class LearningAnalyticsRequest
    {
        [Required]
        public string ActionType { get; set; } = string.Empty;
        
        public string? ContentType { get; set; }
        public int? ContentId { get; set; }
        public int DurationMinutes { get; set; } = 0;
        public int? CourseId { get; set; }
        public int? LessonId { get; set; }
    }
    
    // Communication
    public class ContactRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string Subject { get; set; } = string.Empty;
        
        [Required]
        [StringLength(2000)]
        public string Message { get; set; } = string.Empty;
        
        public int? CourseId { get; set; } // If inquiry is about a specific course
    }
    
    public class NewsletterSubscriptionRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [StringLength(100)]
        public string? FirstName { get; set; }
        
        public IEnumerable<string> Interests { get; set; } = new List<string>();
    }
    
    // Search and Discovery
    public class RecommendationRequest
    {
        public IEnumerable<int>? BasedOnCourses { get; set; }
        public IEnumerable<string>? PreferredCategories { get; set; }
        public IEnumerable<string>? PreferredLevels { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Count { get; set; } = 10;
    }
    
    // Mobile-specific requests
    public class DeviceRegistrationRequest
    {
        [Required]
        public string DeviceToken { get; set; } = string.Empty;
        
        [Required]
        public string Platform { get; set; } = string.Empty; // "ios", "android"
        
        public string? DeviceId { get; set; }
        public string? AppVersion { get; set; }
        public string? OSVersion { get; set; }
    }
    
    public class OfflineContentRequest
    {
        [Required]
        public int CourseId { get; set; }
        
        public IEnumerable<int>? LessonIds { get; set; } // Specific lessons or all if null
        public string Quality { get; set; } = "medium"; // "low", "medium", "high"
    }
    
    // Admin and Instructor requests
    public class BulkActionRequest<T>
    {
        [Required]
        public IEnumerable<T> Items { get; set; } = new List<T>();
        
        [Required]
        public string Action { get; set; } = string.Empty; // "delete", "activate", "deactivate", etc.
    }
    
    public class CourseAnalyticsRequest
    {
        public int? CourseId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Metric { get; set; } = "enrollments"; // "enrollments", "completions", "revenue", etc.
        public string GroupBy { get; set; } = "day"; // "day", "week", "month"
    }
} 