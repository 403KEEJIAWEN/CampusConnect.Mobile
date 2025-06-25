using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CampusConnect.Mobile.Models
{
    public class LikeResponse
    {
        [JsonPropertyName("likeCount")]
        public int LikeCount { get; set; }
        
        [JsonPropertyName("liked")]
        public bool Liked { get; set; }
    }
}