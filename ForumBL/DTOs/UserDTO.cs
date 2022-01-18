using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ForumBL.DTOs
{
    public class UserDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("user_name")]
        public string UserName { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }
    }
}
