using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CampusConnect.Mobile.Models;
using Microsoft.Maui.Controls;
using System.Net.Http.Headers;
using Microsoft.Maui.Storage; // For FileResult

namespace CampusConnect.Mobile.Models
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly string _baseImageUrl;

        public ApiService()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (msg, cert, chain, errors) => true
            };
            _httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(30) };
            _baseUrl = "https://10.0.2.2:8081/api/";
            _baseImageUrl = "https://10.0.2.2:8081";
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
        // ------------------ Registration, Login, Friends, Requests ------------------

        public async Task<ApiResponse<FriendResponse>> RegisterAsync(RegistrationRequest request)
        {
            try
            {
                var validationErrors = ValidateRegistration(request);
                if (validationErrors.Any())
                    return new ApiResponse<FriendResponse> { Success = false, Message = "Validation failed", Errors = validationErrors };

                var registerDto = new
                {
                    name = request.Name,
                    matricNumber = request.MatricNumber,
                    gender = request.Gender,
                    course = request.Course,
                    faculty = request.Faculty,
                    year = request.Year,
                    email = request.Email,
                    contact = request.Contact,
                    password = request.Password
                };
                var json = JsonSerializer.Serialize(registerDto, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync($"{_baseUrl}RegisterApi/register", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return new ApiResponse<FriendResponse> { Success = true, Message = "Registration successful!" };
                }
                else
                {
                    try
                    {
                        string errorMessage = "Registration failed";
                        if (responseContent.Contains("message"))
                        {
                            var errorResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                            if (errorResponse.TryGetProperty("message", out JsonElement messageElement))
                                errorMessage = messageElement.GetString() ?? "Registration failed";
                        }
                        return new ApiResponse<FriendResponse> { Success = false, Message = errorMessage };
                    }
                    catch
                    {
                        return new ApiResponse<FriendResponse>
                        {
                            Success = false,
                            Message = $"Registration failed: {response.StatusCode}\nResponse: {responseContent}"
                        };
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return new ApiResponse<FriendResponse>
                {
                    Success = false,
                    Message = $"Network error: {ex.Message}\nCheck if your web API is running and accessible at {_baseUrl}",
                    ErrorCode = "NETWORK_ERROR"
                };
            }
            catch (TaskCanceledException)
            {
                return new ApiResponse<FriendResponse> { Success = false, Message = "Request timed out. Please try again.", ErrorCode = "TIMEOUT" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<FriendResponse> { Success = false, Message = $"An unexpected error occurred: {ex.Message}", ErrorCode = "UNKNOWN_ERROR" };
            }
        }

        public async Task<ApiResponse<FriendResponse>> LoginAsync(LoginRequest request)
        {
            try
            {
                var loginModel = new { email = request.Email, password = request.Password };
                var json = JsonSerializer.Serialize(loginModel, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}LoginApi/login", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var loginResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                        string userName = "";
                        if (loginResponse.TryGetProperty("name", out JsonElement nameElement))
                            userName = nameElement.GetString() ?? "";
                        return new ApiResponse<FriendResponse>
                        {
                            Success = true,
                            Message = "Login successful!",
                            Data = new FriendResponse { Name = userName }
                        };
                    }
                    catch
                    {
                        return new ApiResponse<FriendResponse> { Success = true, Message = "Login successful!" };
                    }
                }
                else
                {
                    try
                    {
                        string errorMessage = "Login failed";
                        if (responseContent.Contains("message"))
                        {
                            var errorResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);
                            if (errorResponse.TryGetProperty("message", out JsonElement messageElement))
                                errorMessage = messageElement.GetString() ?? "Login failed";
                        }
                        return new ApiResponse<FriendResponse> { Success = false, Message = errorMessage };
                    }
                    catch
                    {
                        return new ApiResponse<FriendResponse>
                        {
                            Success = false,
                            Message = $"Login failed: {response.StatusCode}\nURL: {_baseUrl}LoginApi/login\nResponse: {responseContent}"
                        };
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return new ApiResponse<FriendResponse>
                {
                    Success = false,
                    Message = $"Network error: {ex.Message}\nTrying to connect to: {_baseUrl}LoginApi/login\nCheck if your web API is running."
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<FriendResponse>
                {
                    Success = false,
                    Message = $"An unexpected error occurred: {ex.Message}"
                };
            }
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseUrl}RegisterApi/check-email?email=test@test.com");
                return response.IsSuccessStatusCode;
            }
            catch { return false; }
        }

        private List<string> ValidateRegistration(RegistrationRequest request)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(request.Name)) errors.Add("Name is required");
            if (string.IsNullOrWhiteSpace(request.MatricNumber)) errors.Add("Matric number is required");
            if (string.IsNullOrWhiteSpace(request.Gender)) errors.Add("Gender is required");
            if (string.IsNullOrWhiteSpace(request.Course)) errors.Add("Course is required");
            if (string.IsNullOrWhiteSpace(request.Faculty)) errors.Add("Faculty is required");
            if (request.Year <= 0) errors.Add("Year is required");
            if (string.IsNullOrWhiteSpace(request.Email)) errors.Add("Email is required");
            else if (!IsValidEmail(request.Email)) errors.Add("Please enter a valid email address");
            if (string.IsNullOrWhiteSpace(request.Contact)) errors.Add("Contact is required");
            if (string.IsNullOrWhiteSpace(request.Password)) errors.Add("Password is required");
            else if (request.Password.Length < 6) errors.Add("Password must be at least 6 characters");
            if (request.Password != request.ConfirmPassword) errors.Add("Passwords do not match");
            return errors;
        }

        private bool IsValidEmail(string email)
        {
            try { var addr = new System.Net.Mail.MailAddress(email); return addr.Address == email; }
            catch { return false; }
        }

        // Friend search - GET
        public async Task<FriendSearchResult?> SearchFriendsAsync(string userEmail, string searchTerm = null)
        {
            try
            {
                var url = $"{_baseUrl}FriendsApi/Search?userEmail={Uri.EscapeDataString(userEmail)}";
                if (!string.IsNullOrWhiteSpace(searchTerm)) url += $"&term={Uri.EscapeDataString(searchTerm)}";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode) return null;
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<FriendSearchResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch { return null; }
        }

        public async Task<(bool Success, string Message)> SendFriendRequestAsync(string senderEmail, int receiverId)
        {
            var data = new { SenderEmail = senderEmail, ReceiverId = receiverId };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}FriendsApi/SendFriendRequest", content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return (true, "Request sent.");
            else return (false, responseString);
        }

        public async Task<List<PendingFriendRequestViewModel>> GetPendingRequestsAsync(string userEmail)
        {
            var url = $"{_baseUrl}FriendRequestsApi/Pending?userEmail={Uri.EscapeDataString(userEmail)}";
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode) return new List<PendingFriendRequestViewModel>();
            return JsonSerializer.Deserialize<List<PendingFriendRequestViewModel>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<PendingFriendRequestViewModel>();
        }

        public async Task<bool> AcceptFriendRequestAsync(string userEmail, int requestId)
        {
            var data = new { UserEmail = userEmail, RequestId = requestId };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}FriendRequestsApi/Accept", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeclineFriendRequestAsync(string userEmail, int requestId)
        {
            var data = new { UserEmail = userEmail, RequestId = requestId };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}FriendRequestsApi/Decline", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<(bool Success, string Message)> RemoveFriendAsync(string userEmail, int friendId)
        {
            var data = new { UserEmail = userEmail, FriendId = friendId };
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}FriendsApi/RemoveFriend", content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode) return (true, "Friend removed.");
            else return (false, responseString);
        }

        // ------------------ Posts (Feed, My Posts, Create, Delete, Like) ------------------

        public async Task<List<Post>> GetFeedAsync(string userEmail)
        {
            try
            {
                var friend = await GetFriendByEmailAsync(userEmail);
                if (friend == null)
                {
                    System.Diagnostics.Debug.WriteLine($"GetFeedAsync - Friend not found for email: {userEmail}");
                    await Application.Current.MainPage.DisplayAlert("Debug", $"User not found for {userEmail}", "OK");
                    return new List<Post>();
                }

                var url = $"{_baseUrl}PostApi/GetFeed?userEmail={Uri.EscapeDataString(userEmail)}";
                System.Diagnostics.Debug.WriteLine($"GetFeedAsync - Calling URL: {url}");
                var response = await _httpClient.GetAsync(url);
                var responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"GetFeedAsync - Status code: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"GetFeedAsync - Response start: {responseBody.Substring(0, Math.Min(100, responseBody.Length))}");

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                        var posts = JsonSerializer.Deserialize<List<Post>>(responseBody, options);
                        if (posts != null) return posts;
                        else return new List<Post>();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"GetFeedAsync - Error deserializing: {ex.Message}");
                        await Application.Current.MainPage.DisplayAlert("Debug", $"Failed to parse posts: {ex.Message}", "OK");
                        return new List<Post>();
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"GetFeedAsync - Request failed: {response.StatusCode}");
                    await Application.Current.MainPage.DisplayAlert("Debug", $"GetFeed API failed: {response.StatusCode}\n{responseBody}", "OK");
                    return new List<Post>();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetFeedAsync - Exception: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Debug", $"GetFeed exception: {ex.Message}", "OK");
                return new List<Post>();
            }
        }

        public async Task<List<Post>> GetMyPostsAsync(string userEmail)
        {
            var url = $"{_baseUrl}PostApi/MyPosts?userEmail={Uri.EscapeDataString(userEmail)}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Post>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<Post>();
            }
            return new List<Post>();
        }

        public async Task<Friend?> GetFriendByEmailAsync(string email)
        {
            try
            {
                var url = $"{_baseUrl}FriendsApi/GetByEmail?email={Uri.EscapeDataString(email)}";
                System.Diagnostics.Debug.WriteLine($"GetFriendByEmailAsync - Calling: {url}");
                var response = await _httpClient.GetAsync(url);
                var responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"GetFriendByEmailAsync - Status: {response.StatusCode}");
                System.Diagnostics.Debug.WriteLine($"GetFriendByEmailAsync - Response: {responseBody}");
                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var friend = JsonSerializer.Deserialize<Friend>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        if (friend != null) return friend;
                        else return null;
                    }
                    catch { return null; }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound) { return null; }
                else { return null; }
            }
            catch { return null; }
        }

        // Create a post
        public async Task<Post?> CreatePostAsync(Post post)
        {
            try
            {
                var json = JsonSerializer.Serialize(post, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}PostApi/Create", content);
                var responseBody = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    return JsonSerializer.Deserialize<Post>(responseBody, options);
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Post Error", $"Status: {response.StatusCode}\n{responseBody}", "OK");
                    return null;
                }
            }
            catch { return null; }
        }

        public async Task<bool> DeletePostAsync(int postId, string userEmail)
        {
            var url = $"{_baseUrl}PostApi/Delete/{postId}?userEmail={Uri.EscapeDataString(userEmail)}";
            var response = await _httpClient.PostAsync(url, null);
            return response.IsSuccessStatusCode;
        }

        public async Task<LikeResponse?> ToggleLikeAsync(int postId, int friendId)
        {
            try
            {
                var like = new { PostId = postId, FriendId = friendId };
                var json = JsonSerializer.Serialize(like, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"{_baseUrl}PostApi/ToggleLike", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var likeResponse = JsonSerializer.Deserialize<LikeResponse>(responseBody, options);
                    if (likeResponse != null) return likeResponse;
                }
                return null;
            }
            catch { return null; }
        }

        public async Task<bool> UpdateProfileAsync(Friend updatedFriend)
        {
            var json = JsonSerializer.Serialize(updatedFriend, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}FriendsApi/UpdateProfile", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<ApiResponse<bool>> ChangePasswordAsync(string email, string currentPassword, string newPassword, string confirmPassword)

        {
            var data = new
            {
                Email = email,
                OldPassword = currentPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword
            };


            var json = System.Text.Json.JsonSerializer.Serialize(data, new System.Text.Json.JsonSerializerOptions { PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl + "ProfileApi/ChangePassword", content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return new ApiResponse<bool> { Success = true, Message = "Password changed!", Data = true };
            else
                return new ApiResponse<bool> { Success = false, Message = $"Failed: {response.StatusCode}\n{responseContent}" };
        }

        public async Task<ApiResponse<bool>> EditProfileAsync(EditProfileViewModel model)
        {
            try
            {
                var json = JsonSerializer.Serialize(model, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync(_baseUrl + "ProfileApi/EditProfile", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                    return new ApiResponse<bool> { Success = true, Message = "Profile updated!", Data = true };
                else
                    return new ApiResponse<bool> { Success = false, Message = $"Failed: {response.StatusCode}\n{responseContent}" };
            }
            catch (Exception ex)
            {
                return new ApiResponse<bool> { Success = false, Message = ex.Message };
            }
        }
        public async Task<Post?> CreatePostWithImageAsync(string title, string content, int friendId, FileResult? imageFile)
        {
            using var form = new MultipartFormDataContent();
            form.Add(new StringContent(title), "title");
            form.Add(new StringContent(content), "content");
            form.Add(new StringContent(friendId.ToString()), "friendId");

            if (imageFile != null)
            {
                var stream = await imageFile.OpenReadAsync(); // Remove 'using'
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                form.Add(fileContent, "imageFile", imageFile.FileName);
            }

            var response = await _httpClient.PostAsync($"{_baseUrl}PostApi/CreateWithImage", form);
            var responseBody = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<Post>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            else
            {
                await Application.Current.MainPage.DisplayAlert("Post Error", $"Status: {response.StatusCode}\n{responseBody}", "OK");
                return null;
            }
        }


    }
}
