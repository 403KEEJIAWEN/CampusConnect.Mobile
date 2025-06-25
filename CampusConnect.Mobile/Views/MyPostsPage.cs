using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CampusConnect.Mobile.Models;
using CampusConnect.Mobile.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Graphics;

namespace CampusConnect.Mobile.Views
{
    public class MyPostsPage : ContentPage
    {
        ObservableCollection<Post> posts = new();
        ApiService apiService = new ApiService();
        string userEmail;
        int? myFriendId;

        public MyPostsPage(string userEmail)
        {
            this.userEmail = userEmail;

            // === COLORS ===
            var Pink = Color.FromArgb("#EF4CA6");
            var LightPink = Color.FromArgb("#FFF1F6");
            var DarkText = Color.FromArgb("#374151");
            var MetaText = Color.FromArgb("#6B7280");

            Title = "My Posts";

            // === CREATE POST BUTTON ===
            var createPostButton = new Button
            {
                Text = "Create New Post",
                BackgroundColor = Pink,
                TextColor = Colors.White,
                FontSize = 17,
                Margin = new Thickness(18, 14, 18, 12),
                CornerRadius = 14,
                FontAttributes = FontAttributes.Bold,
                Shadow = new Shadow { Brush = Pink, Offset = new Point(0, 2), Radius = 4, Opacity = 0.3f }
            };
            createPostButton.Clicked += (s, e) =>
            {
                Navigation.PushAsync(new CreatePostPage(userEmail));
            };

            // === COLLECTIONVIEW FOR POSTS ===
            var collectionView = new CollectionView
            {
                BackgroundColor = Colors.Transparent,
                ItemsSource = posts,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                ItemTemplate = new DataTemplate(() =>
                {
                    // --- Controls ---
                    var name = new Label { FontAttributes = FontAttributes.Bold, FontSize = 15, TextColor = DarkText };
                    name.SetBinding(Label.TextProperty, "Friend.Name");

                    var year = new Label { FontSize = 13, TextColor = Pink, Margin = new Thickness(8, 0, 0, 0) };
                    year.SetBinding(Label.TextProperty, new Binding("Friend.Year", stringFormat: "Year {0}"));

                    var faculty = new Label { FontSize = 13, TextColor = Color.FromArgb("#F59E42"), Margin = new Thickness(6, 0, 0, 0) };
                    faculty.SetBinding(Label.TextProperty, "Friend.Faculty");

                    var course = new Label { FontSize = 13, TextColor = Color.FromArgb("#36A2EB"), Margin = new Thickness(6, 0, 0, 0) };
                    course.SetBinding(Label.TextProperty, "Friend.Course");

                    var time = new Label { FontSize = 12, TextColor = MetaText };
                    time.SetBinding(Label.TextProperty, new Binding("CreatedAt", stringFormat: "{0:dd/MM/yyyy HH:mm}"));

                    var title = new Label { FontAttributes = FontAttributes.Bold, FontSize = 16, TextColor = DarkText };
                    title.SetBinding(Label.TextProperty, "Title");

                    var content = new Label { FontSize = 14, TextColor = MetaText };
                    content.SetBinding(Label.TextProperty, "Content");

                    var postImage = new Image
                    {
                        Aspect = Aspect.AspectFit,
                        HeightRequest = 200,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        IsVisible = false
                    };

                    var likeBtn = new Button
                    {
                        FontSize = 26,
                        BackgroundColor = Colors.Transparent,
                        WidthRequest = 50,
                        HeightRequest = 36,
                        Padding = new Thickness(0),
                        VerticalOptions = LayoutOptions.Center
                    };

                    var likeCountLbl = new Label
                    {
                        FontSize = 15,
                        TextColor = Pink,
                        VerticalOptions = LayoutOptions.Center,
                        FontAttributes = FontAttributes.Bold
                    };

                    var deleteBtn = new Button
                    {
                        Text = "Delete",
                        BackgroundColor = Color.FromArgb("#FF3B3B"),
                        TextColor = Colors.White,
                        FontSize = 13,
                        CornerRadius = 10,
                        HorizontalOptions = LayoutOptions.End,
                        Margin = new Thickness(10, 0, 0, 0)
                    };

                    // --- Layout ---
                    var profileStack = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 0,
                        Children = { name, year, faculty, course }
                    };
                    var metaStack = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 12,
                        Children = { time }
                    };
                    var likeStack = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 0,
                        Children = { likeBtn, likeCountLbl }
                    };

                    var mainStack = new StackLayout
                    {
                        Spacing = 7,
                        Padding = new Thickness(12, 10, 12, 10),
                        Children =
                        {
                            profileStack,
                            metaStack,
                            title,
                            content,
                            postImage,
                            likeStack,
                            deleteBtn
                        }
                    };

                    var outerBorder = new Border
                    {
                        BackgroundColor = Colors.White,
                        StrokeShape = new RoundRectangle { CornerRadius = 18 },
                        Margin = new Thickness(10, 5),
                        Padding = new Thickness(12, 10),
                        Shadow = new Shadow { Offset = new Point(0, 2), Radius = 8, Opacity = 0.08f },
                        Content = mainStack
                    };

                    // --- Logic ---
                    outerBorder.BindingContextChanged += (s, e) =>
                    {
                        var post = outerBorder.BindingContext as Post;
                        deleteBtn.IsVisible = true; // always visible on your own posts

                        if (!string.IsNullOrEmpty(post?.ImagePath))
                        {
                            string imageUrl = post.ImagePath.StartsWith("http")
                                ? post.ImagePath
                                : $"https://10.0.2.2:8081{post.ImagePath}";

                            postImage.Source = ImageSource.FromUri(new Uri(imageUrl));
                            postImage.IsVisible = true;
                        }
                        else
                        {
                            postImage.IsVisible = false;
                        }

                        if (myFriendId.HasValue)
                        {
                            bool hasLiked = post?.Likes?.Any(l => l.FriendId == myFriendId.Value) == true;
                            likeBtn.Text = hasLiked ? "❤️" : "🤍";
                            likeBtn.TextColor = hasLiked ? Pink : MetaText;
                            likeCountLbl.Text = (post?.Likes?.Count ?? 0).ToString();
                        }
                        else
                        {
                            likeBtn.Text = "🤍";
                            likeBtn.TextColor = MetaText;
                            likeCountLbl.Text = (post?.Likes?.Count ?? 0).ToString();
                        }
                    };

                    likeBtn.Clicked += async (s, e) =>
                    {
                        var btn = (Button)s;
                        var post = outerBorder.BindingContext as Post;
                        if (post == null || !myFriendId.HasValue)
                        {
                            await Application.Current.MainPage.DisplayAlert("Error", "Unable to like: User not found", "OK");
                            return;
                        }
                        try
                        {
                            btn.IsEnabled = false;
                            var likeResp = await apiService.ToggleLikeAsync(post.Id, myFriendId.Value);

                            if (likeResp != null)
                            {
                                if (post.Likes == null) post.Likes = new System.Collections.Generic.List<Like>();
                                var existingLike = post.Likes.FirstOrDefault(l => l.FriendId == myFriendId.Value);

                                if (likeResp.Liked && existingLike == null)
                                    post.Likes.Add(new Like { PostId = post.Id, FriendId = myFriendId.Value });
                                else if (!likeResp.Liked && existingLike != null)
                                    post.Likes.Remove(existingLike);

                                likeBtn.Text = likeResp.Liked ? "❤️" : "🤍";
                                likeBtn.TextColor = likeResp.Liked ? Pink : MetaText;
                                likeCountLbl.Text = likeResp.LikeCount.ToString();
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Error", "Failed to like/unlike post.", "OK");
                            }
                        }
                        finally
                        {
                            btn.IsEnabled = true;
                        }
                    };

                    deleteBtn.Clicked += async (s, e) =>
                    {
                        var post = outerBorder.BindingContext as Post;
                        if (post == null) return;

                        var confirm = await Application.Current.MainPage.DisplayAlert("Delete Post?", "Are you sure?", "Yes", "No");
                        if (!confirm) return;

                        var deleted = await apiService.DeletePostAsync(post.Id, userEmail);
                        if (deleted)
                        {
                            posts.Remove(post);
                            await Application.Current.MainPage.DisplayAlert("Deleted", "Post deleted.", "OK");
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Failed", "Could not delete post.", "OK");
                        }
                    };

                    return outerBorder;
                })
            };

            // === PAGE LAYOUT ===
            BackgroundColor = LightPink;

            Content = new StackLayout
            {
                BackgroundColor = LightPink,
                Spacing = 0,
                Children =
                {
                    createPostButton,
                    collectionView
                }
            };

            MessagingCenter.Subscribe<CreatePostPage>(this, "PostCreated", (sender) =>
            {
                MainThread.BeginInvokeOnMainThread(async () => { await LoadMyPosts(); });
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<CreatePostPage>(this, "PostCreated");
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadMyPosts();
        }

        async Task LoadMyPosts()
        {
            try
            {
                var friend = await apiService.GetFriendByEmailAsync(userEmail);
                myFriendId = friend?.Id;
                if (myFriendId == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Could not retrieve your user information", "OK");
                    return;
                }
                var result = await apiService.GetMyPostsAsync(userEmail); // ONLY fetch current user's posts
                posts.Clear();
                foreach (var post in result) posts.Add(post);
            }
            catch
            {
                // Handle error if needed
            }
        }
    }
}
