using CampusConnect.Mobile.Models;
using CampusConnect.Mobile.Services;
using System.Collections.ObjectModel;

namespace CampusConnect.Mobile.ViewModels
{
    public class PendingRequestsViewModel
    {
        public ObservableCollection<PendingFriendRequestViewModel> PendingRequests { get; set; } = new();
        public bool IsLoading { get; set; }

        private readonly ApiService _apiService = new();
        private readonly string _userEmail;

        public PendingRequestsViewModel(string userEmail)
        {
            _userEmail = userEmail;
        }

        public async Task LoadPendingAsync()
        {
            IsLoading = true;
            var results = await _apiService.GetPendingRequestsAsync(_userEmail);
            PendingRequests.Clear();
            foreach (var req in results)
                PendingRequests.Add(req);
            IsLoading = false;
        }

        public async Task AcceptAsync(PendingFriendRequestViewModel req)
        {
            IsLoading = true;
            await _apiService.AcceptFriendRequestAsync(_userEmail, req.RequestId);
            await LoadPendingAsync();
            IsLoading = false;
        }
        public async Task DeclineAsync(PendingFriendRequestViewModel req)
        {
            IsLoading = true;
            await _apiService.DeclineFriendRequestAsync(_userEmail, req.RequestId);
            await LoadPendingAsync();
            IsLoading = false;
        }
    }
}
