using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;

namespace AppAuthenticationModel.Models
{
    public partial class AppSurveyContext : DbContext
    {
        public AppSurveyContext()
        {
        }

        public AppSurveyContext(DbContextOptions<AppSurveyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<UserFeedback> UserFeedback { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    var json = System.IO.File.ReadAllText("appsettings.json");
                    JObject jsonConfig = JObject.Parse(json);
                    string connectionString = jsonConfig["ConnectionStrings"]["AppSurveyDatabase"].ToString();
                    optionsBuilder.UseSqlServer(connectionString);

                    optionsBuilder.EnableSensitiveDataLogging();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentTitle)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CommentedTime).HasColumnType("datetime");

                entity.HasOne(d => d.CommentedByNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.CommentedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommentUser");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CommentPost");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostTitle)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PostedTime).HasColumnType("datetime");

                entity.HasOne(d => d.PostedByNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.PostedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostUser");
            });

            modelBuilder.Entity<UserFeedback>(entity =>
            {
                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.UserFeedback)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFeedbackComment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserFeedback)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFeedbackUser");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasMaxLength(100);
            });
        }
    }
}
