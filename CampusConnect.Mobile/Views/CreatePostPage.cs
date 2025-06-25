using Microsoft.Maui.Controls;
using CampusConnect.Mobile.Models;
using CampusConnect.Mobile.Services;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
namespace CampusConnect.Mobile.Views
{
    public class CreatePostPage : ContentPage
    {
        readonly Entry titleEntry;
        readonly Editor contentEditor;
        readonly Button postButton;
        readonly ActivityIndicator activityIndicator;
        readonly ApiService apiService = new ApiService();
        readonly string userEmail;
        FileResult? selectedImageFile = null;
        Image imagePreview;
        public CreatePostPage(string userEmail)
        {
            

            this.userEmail = userEmail;
            Title = "Create Post";

            titleEntry = new Entry { Placeholder = "Title" };
            contentEditor = new Editor { Placeholder = "Content", HeightRequest = 200 };

            var pickImageButton = new Button
            {
                Text = "Pick Image (optional)",
                BackgroundColor = Colors.LightGray,
                TextColor = Colors.Black
            };

            imagePreview = new Image
            {
                HeightRequest = 150,
                WidthRequest = 150,
                Aspect = Aspect.AspectFit,
                IsVisible = false
            };

            pickImageButton.Clicked += async (s, e) =>
            {
                try
                {
                    var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Pick a photo" });
                    if (file != null)
                    {
                        selectedImageFile = file;
                        imagePreview.Source = ImageSource.FromFile(file.FullPath);
                        imagePreview.IsVisible = true;
                    }
                }
                catch
                {
                    await DisplayAlert("Error", "Unable to pick image.", "OK");
                }
            };


            activityIndicator = new ActivityIndicator { IsRunning = false, Color = Colors.Purple };

            postButton = new Button
            {
                Text = "Post",
                BackgroundColor = Color.FromArgb("#E91E63"), // Same pink as EditProfilePage
                TextColor = Colors.White,
                Margin = new Thickness(0, 20, 0, 0)
            };


            postButton.Clicked += async (s, e) => await CreatePost();

            Content = new StackLayout
            {
                Padding = 20,
                Spacing = 15,
                Children = {
        new Label { Text = "Create a New Post", FontSize = 20, FontAttributes = FontAttributes.Bold },
        titleEntry,
        contentEditor,
        pickImageButton,     // <- Add this
        imagePreview,        // <- And this
        activityIndicator,
        postButton
    }
            };

        }

        async Task CreatePost()
        {
            string title = titleEntry.Text?.Trim() ?? "";
            string content = contentEditor.Text?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(content))
            {
                await DisplayAlert("Validation", "Title and content are required.", "OK");
                return;
            }

            // Show loading state
            activityIndicator.IsRunning = true;
            postButton.IsEnabled = false;

            try
            {
                // Get user info (FriendId) first if needed
                var user = await apiService.GetFriendByEmailAsync(userEmail);
                if (user == null)
                {
                    await DisplayAlert("Error", "User not found.", "OK");
                    return;
                }

                Post result;

                // Check if we have an image selected
                if (selectedImageFile != null)
                {
                    // Use the image upload method
                    System.Diagnostics.Debug.WriteLine($"Creating post with image: {selectedImageFile.FileName}");
                    result = await apiService.CreatePostWithImageAsync(title, content, user.Id, selectedImageFile);
                }
                else
                {
                    // Use the regular method (no image)
                    var post = new Post
                    {
                        Title = title,
                        Content = content,
                        FriendId = user.Id
                    };
                    System.Diagnostics.Debug.WriteLine($"Creating post without image: Title={title}, Content={content}, FriendId={user.Id}");
                    result = await apiService.CreatePostAsync(post);
                }

                if (result != null)
                {
                    // Debug the successful result
                    System.Diagnostics.Debug.WriteLine($"Post created successfully: ID={result.Id}");

                    await DisplayAlert("Success", "Post created successfully!", "OK");

                    // Send a message to notify PostPage that a new post was created
                    MessagingCenter.Send(this, "PostCreated");

                    // Go back to previous page
                    await Navigation.PopAsync();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Post creation failed: result was null");
                    await DisplayAlert("Error", "Failed to create post. Please try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error creating post: {ex.Message}");
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            finally
            {
                // Hide loading state
                activityIndicator.IsRunning = false;
                postButton.IsEnabled = true;
            }
        }
    }
}