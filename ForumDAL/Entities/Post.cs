using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ForumDAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ForumId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsLocked { get; set; }
        public string LockedReason { get; set; }
        public DateTime? Updated { get; set; }
        public DateTime Created { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }

        [Required]
        public virtual Forum Forum { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
