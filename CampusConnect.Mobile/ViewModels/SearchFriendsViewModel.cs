using CampusConnect.Mobile.Models;
using CampusConnect.Mobile.Services;
using System.Collections.ObjectModel;

namespace CampusConnect.Mobile.ViewModels
{
    public class SearchFriendsViewModel
    {
        public ObservableCollection<Friend> Results { get; set; } = new();
        public List<int> FriendIds { get; set; } = new();
        public List<int> PendingRequestReceiverIds { get; set; } = new();
        public int CurrentUserId { get; set; }
        public string SearchTerm { get; set; } = "";
        public bool IsLoading { get; set; }

        private readonly ApiService _apiService = new();
        private readonly string _userEmail;

        public SearchFriendsViewModel(string userEmail)
        {
            _userEmail = userEmail;
        }

        public async Task SearchAsync()
        {
            IsLoading = true;
            var result = await _apiService.SearchFriendsAsync(_userEmail, SearchTerm);
            Results.Clear();
            if (result != null)
            {
                foreach (var friend in result.Results)
                    Results.Add(friend);

                CurrentUserId = result.CurrentUserId;
                FriendIds = result.FriendIds ?? new();
                PendingRequestReceiverIds = result.PendingRequestReceiverIds ?? new();
            }
            IsLoading = false;
        }

        public async Task SendFriendRequestAsync(Friend friend)
        {
            IsLoading = true;
            await _apiService.SendFriendRequestAsync(_userEmail, friend.Id);
            await SearchAsync();
            IsLoading = false;
        }
    }
}
