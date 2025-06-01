using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models.Entities
{
    public class Quiz
    {
        public int QuizId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;
        
        [StringLength(1000)]
        public string? Description { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Type { get; set; } = "Quiz"; // Quiz, Assignment, FinalExam
        
        public int PassingScore { get; set; } = 70; // Percentage
        public int TimeLimit { get; set; } = 0; // Minutes, 0 = no limit
        public int MaxAttempts { get; set; } = 3;
        public bool AllowRetakes { get; set; } = true;
        public bool ShowCorrectAnswers { get; set; } = true;
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Key
        public int? LessonId { get; set; } // Null for course-level assessments
        public int CourseId { get; set; }
        
        // Navigation Properties
        public Lesson? Lesson { get; set; }
        public Course Course { get; set; } = null!;
        public ICollection<QuizQuestion> Questions { get; set; } = new List<QuizQuestion>();
        public ICollection<QuizAttempt> Attempts { get; set; } = new List<QuizAttempt>();
    }
    
    public class QuizQuestion
    {
        public int QuizQuestionId { get; set; }
        
        [Required]
        public string QuestionText { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50)]
        public string QuestionType { get; set; } = "MultipleChoice"; // MultipleChoice, TrueFalse, Essay, FillInBlank
        
        public int Points { get; set; } = 1;
        public int OrderIndex { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Key
        public int QuizId { get; set; }
        
        // Navigation Properties
        public Quiz Quiz { get; set; } = null!;
        public ICollection<QuizOption> Options { get; set; } = new List<QuizOption>();
        public ICollection<QuizAnswer> Answers { get; set; } = new List<QuizAnswer>();
    }
    
    public class QuizOption
    {
        public int QuizOptionId { get; set; }
        
        [Required]
        public string OptionText { get; set; } = string.Empty;
        
        public bool IsCorrect { get; set; } = false;
        public int OrderIndex { get; set; }
        
        // Foreign Key
        public int QuizQuestionId { get; set; }
        
        // Navigation Property
        public QuizQuestion Question { get; set; } = null!;
    }
    
    public class QuizAttempt
    {
        public int QuizAttemptId { get; set; }
        
        public int Score { get; set; } = 0; // Percentage
        public int MaxScore { get; set; } = 100;
        public bool IsPassed { get; set; } = false;
        public bool IsCompleted { get; set; } = false;
        
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public DateTime? SubmittedAt { get; set; }
        
        // Foreign Keys
        public int QuizId { get; set; }
        [Required]
        public string UserId { get; set; } = string.Empty;
        
        // Navigation Properties
        public Quiz Quiz { get; set; } = null!;
        public IdentityUser User { get; set; } = null!;
        public ICollection<QuizAnswer> Answers { get; set; } = new List<QuizAnswer>();
    }
    
    public class QuizAnswer
    {
        public int QuizAnswerId { get; set; }
        
        public string? AnswerText { get; set; }
        public bool IsCorrect { get; set; } = false;
        public int PointsEarned { get; set; } = 0;
        
        public DateTime AnsweredAt { get; set; } = DateTime.UtcNow;
        
        // Foreign Keys
        public int QuizAttemptId { get; set; }
        public int QuizQuestionId { get; set; }
        public int? SelectedOptionId { get; set; } // For multiple choice
        
        // Navigation Properties
        public QuizAttempt Attempt { get; set; } = null!;
        public QuizQuestion Question { get; set; } = null!;
        public QuizOption? SelectedOption { get; set; }
    }
} 