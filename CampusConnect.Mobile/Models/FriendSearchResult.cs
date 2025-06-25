using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Models/FriendSearchResult.cs
namespace CampusConnect.Mobile.Models
{
    public class FriendSearchResult
    {
        public int CurrentUserId { get; set; }
        public string CurrentUserEmail { get; set; }
        public List<int> FriendIds { get; set; }
        public List<int> PendingRequestReceiverIds { get; set; }
        public List<Friend> Results { get; set; }
    }
}