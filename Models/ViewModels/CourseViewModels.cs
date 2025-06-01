using System.ComponentModel.DataAnnotations;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.ViewModels
{
    public class CourseDashboardViewModel
    {
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();
        public CourseAnalyticsViewModel Analytics { get; set; } = new();
        public IEnumerable<RecentActivityViewModel> RecentActivity { get; set; } = new List<RecentActivityViewModel>();
        public int TotalCourses { get; set; }
        public int PublishedCourses { get; set; }
        public int TotalStudents { get; set; }
        public decimal TotalEarnings { get; set; }
    }
    
    public class CourseAnalyticsViewModel
    {
        public int TotalEnrollments { get; set; }
        public int ActiveStudents { get; set; }
        public decimal CompletionRate { get; set; }
        public double AverageRating { get; set; }
        public decimal MonthlyEarnings { get; set; }
        public IEnumerable<MonthlyStatsViewModel> MonthlyStats { get; set; } = new List<MonthlyStatsViewModel>();
        public IEnumerable<CoursePerformanceViewModel> TopCourses { get; set; } = new List<CoursePerformanceViewModel>();
    }
    
    public class MonthlyStatsViewModel
    {
        public string Month { get; set; } = string.Empty;
        public int Enrollments { get; set; }
        public decimal Earnings { get; set; }
        public int Completions { get; set; }
    }
    
    public class CoursePerformanceViewModel
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Enrollments { get; set; }
        public decimal CompletionRate { get; set; }
        public double AverageRating { get; set; }
        public decimal Revenue { get; set; }
    }
    
    public class RecentActivityViewModel
    {
        public string Type { get; set; } = string.Empty; // Enrollment, Completion, Review, etc.
        public string Description { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string CourseName { get; set; } = string.Empty;
    }
    
    public class CourseCreateViewModel
    {
        [Required(ErrorMessage = "Course title is required")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters")]
        [Display(Name = "Course Title")]
        public string Title { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Course description is required")]
        [StringLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters")]
        [Display(Name = "Short Description")]
        public string Description { get; set; } = string.Empty;
        
        [StringLength(5000, ErrorMessage = "Long description cannot be longer than 5000 characters")]
        [Display(Name = "Detailed Description")]
        public string? LongDescription { get; set; }
        
        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        [Required(ErrorMessage = "Please select a difficulty level")]
        [Display(Name = "Difficulty Level")]
        public string Level { get; set; } = "Beginner";
        
        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 500, ErrorMessage = "Duration must be between 1 and 500 hours")]
        [Display(Name = "Duration (Hours)")]
        public int DurationHours { get; set; }
        
        [Required(ErrorMessage = "Price is required")]
        [Range(0, 9999.99, ErrorMessage = "Price must be between $0 and $9,999.99")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        [Display(Name = "Course Image")]
        public IFormFile? ImageFile { get; set; }
        
        [Display(Name = "Preview Video")]
        public IFormFile? PreviewVideoFile { get; set; }
        
        [Display(Name = "Featured Course")]
        public bool IsFeatured { get; set; } = false;
        
        // Available options for dropdowns
        public IEnumerable<Category> AvailableCategories { get; set; } = new List<Category>();
        public IEnumerable<string> AvailableLevels { get; set; } = new[] { "Beginner", "Intermediate", "Advanced" };
    }
    
    public class CourseEditViewModel : CourseCreateViewModel
    {
        public int CourseId { get; set; }
        
        [Display(Name = "Current Image")]
        public string? CurrentImageUrl { get; set; }
        
        [Display(Name = "Current Preview Video")]
        public string? CurrentPreviewVideoUrl { get; set; }
        
        [Display(Name = "Published")]
        public bool IsPublished { get; set; }
        
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        // Course statistics
        public int TotalEnrollments { get; set; }
        public double AverageRating { get; set; }
        public int TotalLessons { get; set; }
        public decimal CompletionRate { get; set; }
    }
    
    public class CourseListViewModel
    {
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();
        public CourseSearchFiltersViewModel Filters { get; set; } = new();
        public PaginationViewModel Pagination { get; set; } = new();
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Tag> Tags { get; set; } = new List<Tag>();
    }
    
    public class CourseSearchFiltersViewModel
    {
        [Display(Name = "Search")]
        public string? SearchTerm { get; set; }
        
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }
        
        [Display(Name = "Level")]
        public string? Level { get; set; }
        
        [Display(Name = "Min Price")]
        [DataType(DataType.Currency)]
        public decimal? MinPrice { get; set; }
        
        [Display(Name = "Max Price")]
        [DataType(DataType.Currency)]
        public decimal? MaxPrice { get; set; }
        
        [Display(Name = "Rating")]
        public int? MinRating { get; set; }
        
        [Display(Name = "Sort By")]
        public string SortBy { get; set; } = "Title";
        
        [Display(Name = "Sort Direction")]
        public string SortDirection { get; set; } = "asc";
        
        [Display(Name = "Free Courses Only")]
        public bool FreeOnly { get; set; } = false;
        
        [Display(Name = "Featured Only")]
        public bool FeaturedOnly { get; set; } = false;
    }
    
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 12;
        public int TotalItems { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public int StartItem => (CurrentPage - 1) * PageSize + 1;
        public int EndItem => Math.Min(CurrentPage * PageSize, TotalItems);
    }
    
    public class CourseDetailsViewModel
    {
        public Course Course { get; set; } = null!;
        public IEnumerable<Lesson> Lessons { get; set; } = new List<Lesson>();
        public IEnumerable<CourseReview> Reviews { get; set; } = new List<CourseReview>();
        public bool IsEnrolled { get; set; }
        public bool CanEnroll { get; set; }
        public Enrollment? UserEnrollment { get; set; }
        public LearningProgress? UserProgress { get; set; }
        public IEnumerable<Course> RelatedCourses { get; set; } = new List<Course>();
        public CourseStatsViewModel Stats { get; set; } = new();
    }
    
    public class CourseStatsViewModel
    {
        public int TotalLessons { get; set; }
        public int TotalQuizzes { get; set; }
        public int TotalDurationMinutes { get; set; }
        public int TotalEnrollments { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }
        public decimal CompletionRate { get; set; }
        public string FormattedDuration => FormatDuration(TotalDurationMinutes);
        
        private static string FormatDuration(int minutes)
        {
            var hours = minutes / 60;
            var remainingMinutes = minutes % 60;
            return hours > 0 ? $"{hours}h {remainingMinutes}m" : $"{remainingMinutes}m";
        }
    }
    
    public class LessonCreateViewModel
    {
        [Required(ErrorMessage = "Lesson title is required")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters")]
        [Display(Name = "Lesson Title")]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(2000, ErrorMessage = "Description cannot be longer than 2000 characters")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
        
        [Display(Name = "Content")]
        public string? Content { get; set; }
        
        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 300, ErrorMessage = "Duration must be between 1 and 300 minutes")]
        [Display(Name = "Duration (Minutes)")]
        public int DurationMinutes { get; set; }
        
        [Required(ErrorMessage = "Order index is required")]
        [Display(Name = "Order")]
        public int OrderIndex { get; set; }
        
        [Display(Name = "Preview Lesson")]
        public bool IsPreview { get; set; } = false;
        
        [Display(Name = "Video File")]
        public IFormFile? VideoFile { get; set; }
        
        [Display(Name = "Resources")]
        public IList<IFormFile>? ResourceFiles { get; set; }
        
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
    }
} 