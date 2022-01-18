using System;
using System.Collections.Generic;
using System.Text;

namespace ForumDAL.Entities
{
    public class Forum
    {
        public Forum()
        {
            SubForums = new List<Forum>();
            Posts = new List<Post>();
        }

        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public virtual Forum Parent { get; set; }
        public virtual ICollection<Forum> SubForums { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
