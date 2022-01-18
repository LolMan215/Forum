using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumBL.DTOs
{
    public class CommentDTO
    {
        public int? Id { get; set; }

        public int? PostId { get; set; }

        public string UserId { get; set; }

        public int? ParentId { get; set; }

        public string Body { get; set; }

        public DateTime? Updated { get; set; }

        public DateTime? Created { get; set; }

        public PostDTO Post { get; set; }

        public UserDTO User { get; set; }

        public CommentDTO Parent { get; set; }

        public List<CommentDTO> Children { get; set; }
    }
}
