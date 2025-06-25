using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusConnect.Mobile.Models
{
    public class PendingFriendRequestViewModel
    {
        public int RequestId { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public string MatricNumber { get; set; }
        public string Email { get; set; }
    }
}