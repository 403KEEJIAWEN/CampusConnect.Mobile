// Models/UserFriend.cs
using CampusConnect.Mobile.Models;
using System.ComponentModel.DataAnnotations;
namespace CampusConnect.Mobile.Models
{
    public class UserFriend
    {
        public int UserId { get; set; }
        public Friend? User { get; set; }

        public int FriendId { get; set; }
        public Friend? Friend { get; set; }
    }
}