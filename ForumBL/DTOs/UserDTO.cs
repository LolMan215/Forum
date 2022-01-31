using Newtonsoft.Json;
using System;

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
