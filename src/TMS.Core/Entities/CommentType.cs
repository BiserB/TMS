using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Core.Enums;

namespace TMS.Core.Entities
{
    public class CommentType
    {
        public CommentTypeId CommentTypeId { get; set; }

        public string Name { get; set; }
    }
}
