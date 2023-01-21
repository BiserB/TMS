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

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Comment>().HasKey(c => c.Id);
            mb.Entity<Comment>().HasOne(c => c.Task).WithMany(t => t.Comments).HasForeignKey(c => c.TaskId).IsRequired().OnDelete(DeleteBehavior.ClientCascade);
            mb.Entity<Comment>().HasOne(c => c.Commenter).WithMany(u => u.Comments).HasForeignKey(c => c.CommenterId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            mb.Entity<Comment>().Property(c => c.CommentTypeId).HasConversion<int>();
            mb.Entity<Comment>().Property(c => c.Content).HasMaxLength(2048).IsRequired();

            mb.Entity<CommentType>().HasKey(c => c.CommentTypeId);
            mb.Entity<CommentType>().Property(c => c.CommentTypeId).HasConversion<int>();
            mb.Entity<CommentType>().Property(c => c.Name).HasMaxLength(256).IsRequired();

            mb.Entity<Task>().HasKey(t => t.Id);
            mb.Entity<Task>().HasOne(t => t.Creator).WithMany(u => u.CreatedTasks).HasForeignKey(t => t.CreatorId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            mb.Entity<Task>().Property(t => t.TaskTypeId).HasConversion<int>();
            mb.Entity<Task>().Property(t => t.TaskStatusId).HasConversion<int>();
            mb.Entity<Task>().Property(t => t.Name).HasMaxLength(256).IsRequired();
            mb.Entity<Task>().Property(t => t.Description).HasMaxLength(32767);

            mb.Entity<UserTask>().HasKey(ut => new { ut.UserId, ut.TaskId});
            mb.Entity<UserTask>().HasOne(ut => ut.User).WithMany(u => u.AssignedTasks).HasForeignKey(ut => ut.UserId);
            mb.Entity<UserTask>().HasOne(ut => ut.Task).WithMany(t => t.UserTasks).HasForeignKey(ut => ut.TaskId);

            mb.Entity<TaskType>().HasKey(t => t.TaskTypeId);
            mb.Entity<TaskType>().Property(t => t.TaskTypeId).HasConversion<int>();
            mb.Entity<TaskType>().Property(t => t.Name).HasMaxLength(256).IsRequired();

            mb.Entity<TaskStatus>().HasKey(t => t.TaskStatusId);
            mb.Entity<TaskStatus>().Property(t => t.TaskStatusId).HasConversion<int>();
            mb.Entity<TaskStatus>().Property(t => t.Name).HasMaxLength(256).IsRequired();

            mb.Entity<User>().Property(t => t.FirstName).HasMaxLength(256).IsRequired();
            mb.Entity<User>().Property(t => t.LastName).HasMaxLength(256).IsRequired();

            mb.Entity<CommentType>()
                .HasData(Enum.GetValues(typeof(CommentTypeId))
                    .Cast<CommentTypeId>()
                    .Select(e => new CommentType()
                    {
                        CommentTypeId = e,
                        Name = e.ToString()
                    })
                );

            mb.Entity<TaskType>()
                .HasData(Enum.GetValues(typeof(TaskTypeId))
                    .Cast<TaskTypeId>()
                    .Select(e => new TaskType()
                    {
                        TaskTypeId = e,
                        Name = e.ToString()
                    })
                );

            mb.Entity<TaskStatus>()
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
