using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BlazorAppForDataStructures.Models;

namespace BlazorAppForDataStructures.Data
{
    public class BlazorAppForDataStructuresContext : IdentityDbContext<ApplicationUser>
    {
        public BlazorAppForDataStructuresContext(DbContextOptions<BlazorAppForDataStructuresContext> options)
            : base(options)
        {
        }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Topic entity
            modelBuilder.Entity<Topic>()
                .ToTable("Topics")
                .HasKey(t => t.TopicId); // Primary Key

            modelBuilder.Entity<Topic>()
                .Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            // Configure the Question entity
            modelBuilder.Entity<Question>()
                .ToTable("Questions")
                .HasKey(q => q.QuestionId); // Primary Key

            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionText)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Topic)
                .WithMany(t => t.Questions)
                .HasForeignKey(q => q.TopicId); // Foreign Key

            // Configure the Answer entity
            modelBuilder.Entity<Answer>()
                .ToTable("Answers")
                .HasKey(a => a.AnswerId); // Primary Key

            modelBuilder.Entity<Answer>()
                .Property(a => a.AnswerText)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId); // Foreign Key
        }
    }
}

