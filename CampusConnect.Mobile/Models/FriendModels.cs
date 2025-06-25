using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampusConnect.Mobile.Models
{
    // Exact match with your Friend.cs from website
    public class FriendResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string MatricNumber { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public string Course { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Contact { get; set; } = string.Empty;
        public string Faculty { get; set; } = string.Empty;
        public int Year { get; set; }
    }

    // Exact match with your PendingFriendRequestViewModel
    public class PendingFriendRequest
    {
        public int RequestId { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string MatricNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    // Request DTOs that match your API controller
    public class SendFriendRequestDto
    {
        public string UserEmail { get; set; } = string.Empty;
        public int ReceiverId { get; set; }
    }

    public class RemoveFriendDto
    {
        public string UserEmail { get; set; } = string.Empty;
        public int FriendId { get; set; }
    }

    public class AcceptDeclineRequestDto
    {
        public string UserEmail { get; set; } = string.Empty;
        public int RequestId { get; set; }
    }

    // Mobile-specific view model
    public class SearchFriendViewModel
    {
        public string SearchTerm { get; set; } = string.Empty;
        public List<FriendResponse> Results { get; set; } = new();
        public string CurrentUserEmail { get; set; } = string.Empty;
        public int CurrentUserId { get; set; }
        public List<int> FriendIds { get; set; } = new();
        public List<int> PendingRequestReceiverIds { get; set; } = new();
        public bool IsLoading { get; set; }
        public string StatusMessage { get; set; } = string.Empty;
    }
}