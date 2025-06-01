using WebApplication1.Models.DTOs;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.Responses
{
    // Generic API Response Wrapper
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? RequestId { get; set; }
        
        public static ApiResponse<T> SuccessResult(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message ?? "Request completed successfully"
            };
        }
        
        public static ApiResponse<T> ErrorResult(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
        
        public static ApiResponse<T> ValidationErrorResult(Dictionary<string, string[]> validationErrors)
        {
            var errors = validationErrors
                .SelectMany(x => x.Value.Select(v => $"{x.Key}: {v}"))
                .ToList();
                
            return new ApiResponse<T>
            {
                Success = false,
                Message = "Validation failed",
                Errors = errors
            };
        }
    }
    
    // Paginated Response
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public PaginationMetadata Pagination { get; set; } = new();
    }
    
    public class PaginationMetadata
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public int? PreviousPage => HasPrevious ? CurrentPage - 1 : null;
        public int? NextPage => HasNext ? CurrentPage + 1 : null;
    }
    
    // Authentication Responses
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public UserProfileDto User { get; set; } = null!;
        public IEnumerable<string> Permissions { get; set; } = new List<string>();
    }
    
    public class RefreshTokenResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
    
    public class RegisterResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool EmailConfirmationRequired { get; set; }
        public string? ConfirmationToken { get; set; }
        public string Message { get; set; } = string.Empty;
    }
    
    // Course and Learning Responses
    public class CourseSearchResponse
    {
        public PagedResponse<CourseDto> Courses { get; set; } = new();
        public SearchMetadata Metadata { get; set; } = new();
    }
    
    public class SearchMetadata
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public string? Level { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool FreeOnly { get; set; }
        public bool FeaturedOnly { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public int TotalResults { get; set; }
        public double SearchDurationMs { get; set; }
    }
    
    public class EnrollmentResponse
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public EnrollmentStatus Status { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime EnrolledAt { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? PaymentConfirmation { get; set; }
    }
    
    public class LearningProgressResponse
    {
        public int LessonId { get; set; }
        public string LessonTitle { get; set; } = string.Empty;
        public ProgressStatus Status { get; set; }
        public int TimeSpentMinutes { get; set; }
        public DateTime? CompletedAt { get; set; }
        public bool IsBookmarked { get; set; }
        
        // Course Progress Summary
        public decimal CourseProgress { get; set; }
        public int TotalLessons { get; set; }
        public int CompletedLessons { get; set; }
        public int TotalTimeSpent { get; set; }
    }
    
    public class QuizAttemptResponse
    {
        public int QuizAttemptId { get; set; }
        public int QuizId { get; set; }
        public string QuizTitle { get; set; } = string.Empty;
        public int Score { get; set; }
        public int MaxScore { get; set; }
        public bool IsPassed { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public int TimeLimit { get; set; }
        public int AttemptsRemaining { get; set; }
        
        public IEnumerable<QuizResultDto> Results { get; set; } = new List<QuizResultDto>();
    }
    
    public class QuizResultDto
    {
        public int QuizQuestionId { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string? UserAnswer { get; set; }
        public string? CorrectAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public int PointsEarned { get; set; }
        public int MaxPoints { get; set; }
        public string? Explanation { get; set; }
    }
    
    // Analytics and Reporting Responses
    public class DashboardResponse
    {
        public DashboardStats Stats { get; set; } = new();
        public IEnumerable<RecentActivityDto> RecentActivity { get; set; } = new List<RecentActivityDto>();
        public IEnumerable<CourseDto> FeaturedCourses { get; set; } = new List<CourseDto>();
        public IEnumerable<CourseDto> RecommendedCourses { get; set; } = new List<CourseDto>();
        public IEnumerable<AnnouncementDto> Announcements { get; set; } = new List<AnnouncementDto>();
    }
    
    public class DashboardStats
    {
        public int CoursesEnrolled { get; set; }
        public int CoursesCompleted { get; set; }
        public int TotalLearningHours { get; set; }
        public int AchievementsEarned { get; set; }
        public int CurrentStreak { get; set; }
        public decimal OverallProgress { get; set; }
    }
    
    public class RecentActivityDto
    {
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string? IconUrl { get; set; }
        public string? LinkUrl { get; set; }
    }
    
    public class AnnouncementDto
    {
        public int AnnouncementId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // "info", "warning", "success", "error"
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public bool IsRead { get; set; }
    }
    
    public class AnalyticsResponse
    {
        public string Metric { get; set; } = string.Empty;
        public string Period { get; set; } = string.Empty;
        public IEnumerable<DataPoint> Data { get; set; } = new List<DataPoint>();
        public AnalyticsSummary Summary { get; set; } = new();
    }
    
    public class DataPoint
    {
        public string Label { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
    
    public class AnalyticsSummary
    {
        public decimal Total { get; set; }
        public decimal Average { get; set; }
        public decimal Change { get; set; }
        public decimal ChangePercentage { get; set; }
        public string Trend { get; set; } = string.Empty; // "up", "down", "stable"
    }
    
    // File Upload Response
    public class FileUploadResponse
    {
        public string FileName { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string ContentType { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string? ThumbnailUrl { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
    
    // Health Check Response
    public class HealthCheckResponse
    {
        public string Status { get; set; } = string.Empty; // "Healthy", "Degraded", "Unhealthy"
        public DateTime CheckedAt { get; set; } = DateTime.UtcNow;
        public string Version { get; set; } = string.Empty;
        public Dictionary<string, ServiceHealth> Services { get; set; } = new();
    }
    
    public class ServiceHealth
    {
        public string Status { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double ResponseTimeMs { get; set; }
        public DateTime LastChecked { get; set; }
    }
    
    // Recommendation Response
    public class RecommendationResponse
    {
        public IEnumerable<CourseDto> Courses { get; set; } = new List<CourseDto>();
        public RecommendationMetadata Metadata { get; set; } = new();
    }
    
    public class RecommendationMetadata
    {
        public string Algorithm { get; set; } = string.Empty;
        public IEnumerable<string> Factors { get; set; } = new List<string>();
        public double ConfidenceScore { get; set; }
        public DateTime GeneratedAt { get; set; }
    }
    
    // Search Suggestions Response
    public class SearchSuggestionsResponse
    {
        public IEnumerable<string> CourseTitles { get; set; } = new List<string>();
        public IEnumerable<string> Categories { get; set; } = new List<string>();
        public IEnumerable<string> Instructors { get; set; } = new List<string>();
        public IEnumerable<string> Skills { get; set; } = new List<string>();
    }
    
    // Notification Response
    public class NotificationResponse
    {
        public int NotificationId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ActionUrl { get; set; }
        public Dictionary<string, object> Data { get; set; } = new();
    }
    
    // Statistics Response
    public class StatsResponse
    {
        public Dictionary<string, object> Stats { get; set; } = new();
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        public string Period { get; set; } = string.Empty;
    }
} 