using System;
using System.Collections.Generic;
using TMS.Core.Enums;

namespace TMS.Core.Entities
{
    public class Task
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TaskTypeId TaskTypeId { get; set; }

        public TaskType TaskType { get; set; }

        public TaskStatusId TaskStatusId { get; set; }

        public TaskStatus TaskStatus { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public User Creator { get; set; }

        public DateTime RequiredByDate { get; set; }               

        public IList<Comment> Comments { get; set; }

        public IList<UserTask> UserTasks { get; set; }
    }
}
