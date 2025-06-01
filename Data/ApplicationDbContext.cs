using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Course Management
        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonResource> LessonResources { get; set; }
        
        // Assessment System
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizOption> QuizOptions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }
        
        // Enrollment and Progress
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<LearningProgress> LearningProgress { get; set; }
        public DbSet<CourseReview> CourseReviews { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        
        // User Profile and Social
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserAchievement> UserAchievements { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        
        // Tagging and Analytics
        public DbSet<Tag> Tags { get; set; }
        public DbSet<CourseTag> CourseTags { get; set; }
        public DbSet<LearningAnalytics> LearningAnalytics { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Course Configuration
            builder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.CourseId);
                entity.Property(e => e.Price).HasColumnType("decimal(10,2)");
                entity.HasIndex(e => e.Title);
                entity.HasIndex(e => e.Level);
                entity.HasIndex(e => e.IsPublished);
                
                entity.HasOne(e => e.Instructor)
                    .WithMany()
                    .HasForeignKey(e => e.InstructorId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.Category)
                    .WithMany(c => c.Courses)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Category Configuration
            builder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId);
                entity.HasIndex(e => e.Name).IsUnique();
                entity.HasIndex(e => e.SortOrder);
            });

            // Lesson Configuration
            builder.Entity<Lesson>(entity =>
            {
                entity.HasKey(e => e.LessonId);
                entity.HasIndex(e => new { e.CourseId, e.OrderIndex });
                
                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Lessons)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Lesson Resource Configuration
            builder.Entity<LessonResource>(entity =>
            {
                entity.HasKey(e => e.LessonResourceId);
                
                entity.HasOne(e => e.Lesson)
                    .WithMany(l => l.Resources)
                    .HasForeignKey(e => e.LessonId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Quiz Configuration
            builder.Entity<Quiz>(entity =>
            {
                entity.HasKey(e => e.QuizId);
                
                entity.HasOne(e => e.Course)
                    .WithMany()
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Lesson)
                    .WithMany(l => l.Quizzes)
                    .HasForeignKey(e => e.LessonId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Quiz Question Configuration
            builder.Entity<QuizQuestion>(entity =>
            {
                entity.HasKey(e => e.QuizQuestionId);
                entity.HasIndex(e => new { e.QuizId, e.OrderIndex });
                
                entity.HasOne(e => e.Quiz)
                    .WithMany(q => q.Questions)
                    .HasForeignKey(e => e.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Quiz Option Configuration
            builder.Entity<QuizOption>(entity =>
            {
                entity.HasKey(e => e.QuizOptionId);
                entity.HasIndex(e => new { e.QuizQuestionId, e.OrderIndex });
                
                entity.HasOne(e => e.Question)
                    .WithMany(q => q.Options)
                    .HasForeignKey(e => e.QuizQuestionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Quiz Attempt Configuration
            builder.Entity<QuizAttempt>(entity =>
            {
                entity.HasKey(e => e.QuizAttemptId);
                entity.HasIndex(e => new { e.UserId, e.QuizId });
                
                entity.HasOne(e => e.Quiz)
                    .WithMany(q => q.Attempts)
                    .HasForeignKey(e => e.QuizId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Quiz Answer Configuration
            builder.Entity<QuizAnswer>(entity =>
            {
                entity.HasKey(e => e.QuizAnswerId);
                
                entity.HasOne(e => e.Attempt)
                    .WithMany(a => a.Answers)
                    .HasForeignKey(e => e.QuizAttemptId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Question)
                    .WithMany(q => q.Answers)
                    .HasForeignKey(e => e.QuizQuestionId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(e => e.SelectedOption)
                    .WithMany()
                    .HasForeignKey(e => e.SelectedOptionId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Enrollment Configuration
            builder.Entity<Enrollment>(entity =>
            {
                entity.HasKey(e => e.EnrollmentId);
                entity.Property(e => e.Progress).HasColumnType("decimal(5,2)");
                entity.Property(e => e.PricePaid).HasColumnType("decimal(10,2)");
                entity.HasIndex(e => new { e.UserId, e.CourseId }).IsUnique();
                entity.HasIndex(e => e.Status);
                
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Enrollments)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Learning Progress Configuration
            builder.Entity<LearningProgress>(entity =>
            {
                entity.HasKey(e => e.LearningProgressId);
                entity.Property(e => e.Score).HasColumnType("decimal(5,2)");
                entity.HasIndex(e => new { e.UserId, e.LessonId }).IsUnique();
                
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Lesson)
                    .WithMany(l => l.LearningProgress)
                    .HasForeignKey(e => e.LessonId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Enrollment)
                    .WithMany(en => en.LearningProgress)
                    .HasForeignKey(e => e.EnrollmentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Course Review Configuration
            builder.Entity<CourseReview>(entity =>
            {
                entity.HasKey(e => e.CourseReviewId);
                entity.HasIndex(e => new { e.UserId, e.CourseId }).IsUnique();
                entity.HasIndex(e => e.Rating);
                
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Course)
                    .WithMany(c => c.Reviews)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Certificate Configuration
            builder.Entity<Certificate>(entity =>
            {
                entity.HasKey(e => e.CertificateId);
                entity.HasIndex(e => e.CertificateNumber).IsUnique();
                entity.HasIndex(e => new { e.UserId, e.CourseId });
                
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Course)
                    .WithMany()
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Enrollment)
                    .WithMany()
                    .HasForeignKey(e => e.EnrollmentId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // User Profile Configuration
            builder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.UserProfileId);
                entity.HasIndex(e => e.UserId).IsUnique();
                
                entity.HasOne(e => e.User)
                    .WithOne()
                    .HasForeignKey<UserProfile>(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // User Achievement Configuration
            builder.Entity<UserAchievement>(entity =>
            {
                entity.HasKey(e => e.UserAchievementId);
                entity.HasIndex(e => new { e.UserId, e.Type });
                
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Course)
                    .WithMany()
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // User Skill Configuration
            builder.Entity<UserSkill>(entity =>
            {
                entity.HasKey(e => e.UserSkillId);
                entity.HasIndex(e => new { e.UserId, e.SkillName }).IsUnique();
                
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Course)
                    .WithMany()
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Tag Configuration
            builder.Entity<Tag>(entity =>
            {
                entity.HasKey(e => e.TagId);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // Course Tag Configuration (Many-to-Many)
            builder.Entity<CourseTag>(entity =>
            {
                entity.HasKey(e => e.CourseTagId);
                entity.HasIndex(e => new { e.CourseId, e.TagId }).IsUnique();
                
                entity.HasOne(e => e.Course)
                    .WithMany(c => c.CourseTags)
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Tag)
                    .WithMany(t => t.CourseTags)
                    .HasForeignKey(e => e.TagId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Learning Analytics Configuration
            builder.Entity<LearningAnalytics>(entity =>
            {
                entity.HasKey(e => e.LearningAnalyticsId);
                entity.HasIndex(e => new { e.UserId, e.Timestamp });
                entity.HasIndex(e => e.ActionType);
                
                entity.HasOne(e => e.User)
                    .WithMany()
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(e => e.Course)
                    .WithMany()
                    .HasForeignKey(e => e.CourseId)
                    .OnDelete(DeleteBehavior.SetNull);
                    
                entity.HasOne(e => e.Lesson)
                    .WithMany()
                    .HasForeignKey(e => e.LessonId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Seed Data
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Seed Categories
            builder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Programming",
                    Description = "Learn various programming languages and frameworks",
                    Color = "#007bff",
                    SortOrder = 1,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Design",
                    Description = "UI/UX Design, Graphic Design, and Creative Skills",
                    Color = "#28a745",
                    SortOrder = 2,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Business",
                    Description = "Business skills, Marketing, and Entrepreneurship",
                    Color = "#ffc107",
                    SortOrder = 3,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    CategoryId = 4,
                    Name = "Data Science",
                    Description = "Data Analysis, Machine Learning, and Statistics",
                    Color = "#dc3545",
                    SortOrder = 4,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    CategoryId = 5,
                    Name = "Digital Marketing",
                    Description = "SEO, Social Media, Content Marketing",
                    Color = "#6f42c1",
                    SortOrder = 5,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Tags
            builder.Entity<Tag>().HasData(
                new Tag { TagId = 1, Name = "Beginner Friendly", Color = "#28a745", CreatedAt = DateTime.UtcNow },
                new Tag { TagId = 2, Name = "Hands-on", Color = "#007bff", CreatedAt = DateTime.UtcNow },
                new Tag { TagId = 3, Name = "Certificate", Color = "#ffc107", CreatedAt = DateTime.UtcNow },
                new Tag { TagId = 4, Name = "Popular", Color = "#dc3545", CreatedAt = DateTime.UtcNow },
                new Tag { TagId = 5, Name = "New", Color = "#17a2b8", CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
