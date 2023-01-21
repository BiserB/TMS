using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TMS.Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsDeleted { get; set; }

        public IList<Task> CreatedTasks { get; set; }

        public IList<UserTask> AssignedTasks { get; set; }

        public IList<Comment> Comments { get; set; }
    }
}
