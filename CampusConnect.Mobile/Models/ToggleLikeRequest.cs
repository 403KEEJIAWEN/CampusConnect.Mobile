using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusConnect.Mobile.Models
{
    public class ToggleLikeRequest
    {
        public int PostId { get; set; }
        public int FriendId { get; set; }
    }

}
