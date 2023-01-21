using System;
using TMS.Core.Enums;

namespace TMS.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public int TaskId { get; set; }

        public Task Task { get; set; }

        public CommentTypeId CommentTypeId { get; set; }

        public CommentType CommentType { get; set; }

        public string Content { get; set; }

        public DateTime ReminderDate { get; set; }

        public string CommenterId { get; set; }

        public User Commenter { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
