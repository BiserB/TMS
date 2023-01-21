using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TMS.Core.Entities;
using TMS.Core.Enums;

namespace TMS.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<TaskType> TaskTypes { get; set; }

        public DbSet<TaskStatus> TaskStatuses { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<CommentType> CommentTypes { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>().HasKey(c => c.Id);
            modelBuilder.Entity<Comment>().HasOne(c => c.Task).WithMany(t => t.Comments).HasForeignKey(c => c.TaskId).IsRequired().OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<Comment>().HasOne(c => c.Commenter).WithMany(u => u.Comments).HasForeignKey(c => c.CommenterId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Comment>().Property(c => c.CommentTypeId).HasConversion<int>();
            modelBuilder.Entity<Comment>().Property(c => c.Content).HasMaxLength(2048).IsRequired();

            modelBuilder.Entity<CommentType>().HasKey(c => c.CommentTypeId);
            modelBuilder.Entity<CommentType>().Property(c => c.CommentTypeId).HasConversion<int>();
            modelBuilder.Entity<CommentType>().Property(c => c.Name).HasMaxLength(256).IsRequired();

            modelBuilder.Entity<Task>().HasKey(t => t.Id);
            modelBuilder.Entity<Task>().HasOne(t => t.Creator).WithMany(u => u.CreatedTasks).HasForeignKey(t => t.CreatorId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Task>().Property(t => t.TaskTypeId).HasConversion<int>();
            modelBuilder.Entity<Task>().Property(t => t.TaskStatusId).HasConversion<int>();
            modelBuilder.Entity<Task>().Property(t => t.Name).HasMaxLength(256).IsRequired();

            modelBuilder.Entity<UserTask>().HasKey(ut => new { ut.UserId, ut.TaskId});
            modelBuilder.Entity<UserTask>().HasOne(ut => ut.User).WithMany(u => u.AssignedTasks).HasForeignKey(ut => ut.UserId);
            modelBuilder.Entity<UserTask>().HasOne(ut => ut.Task).WithMany(t => t.UserTasks).HasForeignKey(ut => ut.TaskId);

            modelBuilder.Entity<TaskType>().HasKey(t => t.TaskTypeId);
            modelBuilder.Entity<TaskType>().Property(t => t.TaskTypeId).HasConversion<int>();
            modelBuilder.Entity<TaskType>().Property(t => t.Name).HasMaxLength(256).IsRequired();

            modelBuilder.Entity<TaskStatus>().HasKey(t => t.TaskStatusId);
            modelBuilder.Entity<TaskStatus>().Property(t => t.TaskStatusId).HasConversion<int>();
            modelBuilder.Entity<TaskStatus>().Property(t => t.Name).HasMaxLength(256).IsRequired();

            modelBuilder.Entity<User>().Property(t => t.FirstName).HasMaxLength(256).IsRequired();
            modelBuilder.Entity<User>().Property(t => t.LastName).HasMaxLength(256).IsRequired();

            modelBuilder.Entity<CommentType>()
                .HasData(Enum.GetValues(typeof(CommentTypeId))
                    .Cast<CommentTypeId>()
                    .Select(e => new CommentType()
                    {
                        CommentTypeId = e,
                        Name = e.ToString()
                    })
                );

            modelBuilder.Entity<TaskType>()
                .HasData(Enum.GetValues(typeof(TaskTypeId))
                    .Cast<TaskTypeId>()
                    .Select(e => new TaskType()
                    {
                        TaskTypeId = e,
                        Name = e.ToString()
                    })
                );

            modelBuilder.Entity<TaskStatus>()
               .HasData(Enum.GetValues(typeof(TaskStatusId))
                   .Cast<TaskStatusId>()
                   .Select(e => new TaskStatus()
                   {
                       TaskStatusId = e,
                       Name = e.ToString()
                   })
               );

        }
    }
}
