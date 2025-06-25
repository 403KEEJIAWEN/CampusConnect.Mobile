using CampusConnect.Mobile.Models;


namespace CampusConnect.Mobile.Services
{
    public interface IApiService
    {
        Task<ApiResponse<Friend>> LoginAsync(LoginRequest request);
        Task<ApiResponse<Friend>> RegisterAsync(RegistrationRequest request);
        Task<ApiResponse<Friend>> GetCurrentUserAsync();
        Task<ApiResponse<Friend>> UpdateProfileAsync(EditProfileViewModel profile);
       
        Task<ApiResponse<List<Friend>>> SearchFriendsAsync(string searchTerm = "");
        Task<ApiResponse<bool>> SendFriendRequestAsync(int receiverId);
        Task<ApiResponse<bool>> AcceptFriendRequestAsync(int requestId);
        Task<ApiResponse<bool>> DeclineFriendRequestAsync(int requestId);
        Task<ApiResponse<bool>> RemoveFriendAsync(int friendId);
       
        Task<ApiResponse<List<Post>>> GetPostsAsync();
        Task<ApiResponse<List<Post>>> GetMyPostsAsync();
        Task<ApiResponse<Post>> CreatePostAsync(Post post);
        Task<ApiResponse<bool>> DeletePostAsync(int postId);
        Task<ApiResponse<bool>> ToggleLikeAsync(int postId);
    }
}
