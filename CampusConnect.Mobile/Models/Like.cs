using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusConnect.Mobile.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int FriendId { get; set; }
        public Post Post { get; set; }
        public Friend Friend { get; set; }
    }

}
