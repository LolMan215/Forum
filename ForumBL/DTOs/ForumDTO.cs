using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumBL.DTOs
{
    public class ForumDTO
    {
        public int Id { get; set; }

        public int? ParentId { get; set; }

        public string Name { get; set; }

        public DateTime? Created { get; set; }

        public ForumDTO Parent { get; set; }

        public List<ForumDTO> SubForums { get; set; }

        public List<PostDTO> Posts { get; set; }
    }
}
