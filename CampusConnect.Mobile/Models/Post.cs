using CampusConnect.Mobile.Models;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusConnect.Mobile.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int FriendId { get; set; }
        public Friend? Friend { get; set; }
        public string? ImagePath { get; set; }
        public string? FileAttachmentPath { get; set; }

        // Initialize the Likes collection to prevent null reference exceptions
        public List<Like> Likes { get; set; } = new List<Like>();
    }
}