using System;
using System.Collections.Generic;

namespace ForumBL.DTOs
{
    public class PostDTO
    {
        public int? Id { get; set; }

        public string UserId { get; set; }

        public int? ForumId { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public bool? IsLocked { get; set; }

        public string LockedReason { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Created { get; set; }

        public UserDTO User { get; set; }

        public ForumDTO Forum { get; set; }

        public List<CommentDTO> Comments { get; set; }
    }
}
