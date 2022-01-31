using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ForumDAL.Entities
{
    public class CommentHistory
    {
        public int Id { get; set; }
        public int CommentId { get; set; }
        public string Body { get; set; }
        public DateTime Created { get; set; }

        public virtual Comment Comment { get; set; }
    }
}
