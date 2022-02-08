using System;
using System.Collections.Generic;

namespace ForumBL.DTOs
{
    public class ForumDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? Created { get; set; }

        public List<PostDTO>? Posts { get; set; }
    }
}
