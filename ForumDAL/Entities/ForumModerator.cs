using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ForumDAL.Entities
{
    public class ForumModerator
    {
        public string UserId { get; set; }
        public int ForumId { get; set; }
        public DateTime Created { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual Forum Forum { get; set; }
    }
}
