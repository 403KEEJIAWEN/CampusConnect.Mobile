using CampusConnect.Mobile.Models;
using CampusConnect.Mobile.Services;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace CampusConnect.Mobile.Views
{
    public class SearchFriendsPage : ContentPage
    {
        // === Updated Color Palette ===
        readonly Color Pink = Color.FromArgb("#EF4CA6");
        readonly Color LightPink = Color.FromArgb("#FFF1F6");
        readonly Color Purple = Color.FromArgb("#7D3CF8");
        readonly Color LightPurple = Color.FromArgb("#EEE7FB");
        readonly Color Orange = Color.FromArgb("#FF9F47");
        readonly Color LightOrange = Color.FromArgb("#FFF4EA");
        readonly Color Blue = Color.FromArgb("#56C3F6");
        readonly Color LightBlue = Color.FromArgb("#E7F6FB");
        readonly Color MetaText = Color.FromArgb("#6B7280");
        readonly Color BgGray = Color.FromArgb("#F7F8FA");

        // New colors for your request
        readonly Color NewPink = Color.FromArgb("#E91E63");
        readonly Color DarkText = Color.FromArgb("#374151"); // Dark grey for text

        readonly ApiService _apiService = new();
        readonly ObservableCollection<Friend> _friends = new();
        readonly Entry _searchEntry = new() { Placeholder = "Search name/matric/course/faculty" };
        readonly Button _searchBtn = new() { Text = "Search" };
        readonly ActivityIndicator _loading = new() { IsVisible = false, IsRunning = false };
        readonly CollectionView _collectionView;

        int _currentUserId = 0;
        string _userEmail;
        System.Collections.Generic.List<int> _friendIds = new();
        System.Collections.Generic.List<int> _pendingRequestReceiverIds = new();

        public SearchFriendsPage(string userEmail)
        {
            Title = "Search Friends";
            _userEmail = userEmail;

            // Pending Requests button (changed from Orange to #E91E63)
            var pendingButton = new Button
            {
                Text = "Pending Requests",
                BackgroundColor = NewPink, // Changed from Orange
                TextColor = Colors.White,
                Margin = new Thickness(0, 0, 0, 10),
                CornerRadius = 10,
                FontAttributes = FontAttributes.Bold
            };
            pendingButton.Clicked += async (s, e) =>
            {
                await Navigation.PushAsync(new PendingRequestsPage(_userEmail));
            };

            // === Main Friends List (CollectionView) ===
            _collectionView = new CollectionView
            {
                ItemsSource = _friends,
                ItemTemplate = new DataTemplate(() =>
                {
                    // === Card Background matches PostPage (soft pastel) ===
                    var cardFrame = new Frame
                    {
                        BackgroundColor = LightPink,
                        BorderColor = Colors.Transparent,
                        CornerRadius = 16,
                        Padding = new Thickness(16),
                        Margin = new Thickness(8, 6),
                        HasShadow = true
                    };

                    var mainGrid = new Grid
                    {
                        RowDefinitions =
                        {
                            new RowDefinition { Height = GridLength.Auto }, // Name
                            new RowDefinition { Height = GridLength.Auto }, // Tags
                            new RowDefinition { Height = GridLength.Auto }  // Actions
                        },
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Star }
                        }
                    };

                    // === Name Label (changed from Purple to DarkText) ===
                    var nameLabel = new Label
                    {
                        FontAttributes = FontAttributes.Bold,
                        FontSize = 18,
                        TextColor = DarkText, // Changed from Purple
                        Margin = new Thickness(0, 0, 0, 8)
                    };
                    nameLabel.SetBinding(Label.TextProperty, "Name");
                    Grid.SetRow(nameLabel, 0);
                    mainGrid.Children.Add(nameLabel);

                    // === Pastel Pill Tag Container ===
                    var tagsContainer = new FlexLayout
                    {
                        Direction = FlexDirection.Row,
                        Wrap = FlexWrap.Wrap,
                        JustifyContent = FlexJustify.Start,
                        AlignItems = FlexAlignItems.Center,
                        Margin = new Thickness(0, 0, 0, 12),
                        // No background for container
                    };

                    // Pill Tags (updated text colors)
                    // Pill Tags with darker background colors
                    var matricTag = CreateTag("", Color.FromArgb("#5B21B6"), Colors.White); // Dark purple
                    ((Label)matricTag.Content).SetBinding(Label.TextProperty, "MatricNumber");

                    var yearTag = CreateTag("", Color.FromArgb("#BE185D"), Colors.White); // Dark pink
                    ((Label)yearTag.Content).SetBinding(Label.TextProperty, new Binding("Year", stringFormat: "Year {0}"));

                    var facultyTag = CreateTag("", Color.FromArgb("#0369A1"), Colors.White); // Dark blue
                    ((Label)facultyTag.Content).SetBinding(Label.TextProperty, "Faculty");

                    var courseTag = CreateTag("", Color.FromArgb("#C2410C"), Colors.White); // Dark orange
                    ((Label)courseTag.Content).SetBinding(Label.TextProperty, "Course");

                    tagsContainer.Children.Add(matricTag);
                    tagsContainer.Children.Add(yearTag);
                    tagsContainer.Children.Add(facultyTag);
                    tagsContainer.Children.Add(courseTag);

                    Grid.SetRow(tagsContainer, 1);
                    mainGrid.Children.Add(tagsContainer);

                    // === Action Buttons (Add Friend, Friends, Pending, You) ===
                    var actionContainer = new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.End
                    };
                    Grid.SetRow(actionContainer, 2);
                    mainGrid.Children.Add(actionContainer);

                    cardFrame.Content = mainGrid;

                    // --- Handle action buttons and status ---
                    cardFrame.BindingContextChanged += (s, e) =>
                    {
                        actionContainer.Children.Clear();

                        if (cardFrame.BindingContext is Friend friend)
                        {
                            if (friend.Id == _currentUserId)
                            {
                                actionContainer.Children.Add(CreateStatusLabel("You", Colors.LightGray, Colors.Black));
                            }
                            else if (_friendIds.Contains(friend.Id))
                            {
                                var friendsButton = new Button
                                {
                                    Text = "Friends",
                                    BackgroundColor = Colors.LightGreen,
                                    TextColor = Colors.DarkGreen,
                                    FontSize = 13,
                                    Padding = new Thickness(12, 6),
                                    CornerRadius = 10,
                                    HorizontalOptions = LayoutOptions.End
                                };
                                friendsButton.Clicked += async (sender, args) =>
                                {
                                    if (!(cardFrame.BindingContext is Friend btnFriend)) return;
                                    var confirm = await Application.Current.MainPage.DisplayAlert(
                                        "Remove Friend", $"Remove {btnFriend.Name} from your friends?", "Yes", "No");
                                    if (confirm)
                                    {
                                        _loading.IsRunning = _loading.IsVisible = true;
                                        var (success, msg) = await _apiService.RemoveFriendAsync(_userEmail, btnFriend.Id);
                                        await Application.Current.MainPage.DisplayAlert(success ? "Success" : "Error", msg, "OK");
                                        await RefreshSearchAsync(_searchEntry.Text);
                                        _loading.IsRunning = _loading.IsVisible = false;
                                    }
                                };
                                actionContainer.Children.Add(friendsButton);
                            }
                            else if (_pendingRequestReceiverIds.Contains(friend.Id))
                            {
                                var pendingLabel = CreateStatusLabel("Request Sent", MetaText, Colors.White);
                                actionContainer.Children.Add(pendingLabel);
                            }
                            else
                            {
                                var addButton = new Button
                                {
                                    Text = "Add Friend",
                                    BackgroundColor = NewPink, // Changed from Purple to NewPink
                                    TextColor = Colors.White,
                                    FontSize = 13,
                                    Padding = new Thickness(12, 6),
                                    CornerRadius = 10,
                                    HorizontalOptions = LayoutOptions.End
                                };

                                addButton.Clicked += async (sender, args) =>
                                {
                                    if (!(cardFrame.BindingContext is Friend btnFriend)) return;

                                    var confirm = await Application.Current.MainPage.DisplayAlert(
                                        "Send Request", $"Send friend request to {btnFriend.Name}?", "Yes", "No");
                                    if (confirm)
                                    {
                                        _loading.IsRunning = _loading.IsVisible = true;
                                        var (success, msg) = await _apiService.SendFriendRequestAsync(_userEmail, btnFriend.Id);
                                        await Application.Current.MainPage.DisplayAlert(success ? "Success" : "Error", msg, "OK");
                                        await RefreshSearchAsync(_searchEntry.Text);
                                        _loading.IsRunning = _loading.IsVisible = false;
                                    }
                                };
                                actionContainer.Children.Add(addButton);
                            }
                        }
                    };

                    return cardFrame;
                }),
                ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical) { ItemSpacing = 10 },
                BackgroundColor = BgGray
            };

            _searchBtn.Clicked += async (s, e) => await RefreshSearchAsync(_searchEntry.Text);
            _searchBtn.BackgroundColor = NewPink; // Changed from Purple to NewPink
            _searchBtn.TextColor = Colors.White;
            _searchBtn.CornerRadius = 10;
            _searchBtn.FontAttributes = FontAttributes.Bold;
            _searchBtn.Margin = new Thickness(0, 0, 0, 10);

            _searchEntry.Margin = new Thickness(0, 0, 0, 10);

            // === Page Layout ===
            var grid = new Grid
            {
                Padding = 12,
                BackgroundColor = BgGray,
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto }, // Label
                    new RowDefinition { Height = GridLength.Auto }, // Pending Button
                    new RowDefinition { Height = GridLength.Auto }, // Search Entry
                    new RowDefinition { Height = GridLength.Auto }, // Search Btn
                    new RowDefinition { Height = GridLength.Auto }, // Loading
                    new RowDefinition { Height = GridLength.Star }  // CollectionView
                }
            };

            var titleLabel = new Label
            {
                Text = "Search for friends:",
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                TextColor = DarkText, // Changed from Purple to DarkText
                Margin = new Thickness(0, 0, 0, 6)
            };

            grid.Children.Add(titleLabel);
            Grid.SetRow(titleLabel, 0);

            grid.Children.Add(pendingButton);
            Grid.SetRow(pendingButton, 1);

            grid.Children.Add(_searchEntry);
            Grid.SetRow(_searchEntry, 2);

            grid.Children.Add(_searchBtn);
            Grid.SetRow(_searchBtn, 3);

            grid.Children.Add(_loading);
            Grid.SetRow(_loading, 4);

            grid.Children.Add(_collectionView);
            Grid.SetRow(_collectionView, 5);

            Content = grid;

            Appearing += async (s, e) => await RefreshSearchAsync(null);
        }

        // === Beautiful Pastel Pill Tag ===
        private Frame CreateTag(string text, Color backgroundColor, Color textColor)
        {
            return new Frame
            {
                Padding = new Thickness(12, 5),
                Margin = new Thickness(3, 2),
                CornerRadius = 16,
                HasShadow = false,
                BackgroundColor = backgroundColor,
                BorderColor = Colors.Transparent,
                Content = new Label
                {
                    Text = text,
                    TextColor = textColor,
                    FontSize = 12,
                    FontAttributes = FontAttributes.Bold,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Center,
                    LineBreakMode = LineBreakMode.TailTruncation
                }
            };
        }

        // === Status Labels for Friends/Requests/You ===
        private Label CreateStatusLabel(string text, Color backgroundColor, Color textColor)
        {
            return new Label
            {
                Text = text,
                BackgroundColor = backgroundColor,
                TextColor = textColor,
                Padding = new Thickness(14, 7),
                FontSize = 13,
                FontAttributes = FontAttributes.Bold,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.End,

            };
        }

        // === Fetch and Refresh Friend Search Results ===
        private async Task RefreshSearchAsync(string? searchTerm)
        {
            try
            {
                _loading.IsVisible = _loading.IsRunning = true;
                var result = await _apiService.SearchFriendsAsync(_userEmail, searchTerm);
                _friends.Clear();
                if (result != null)
                {
                    foreach (var f in result.Results)
                        _friends.Add(f);

                    _currentUserId = result.CurrentUserId;
                    _friendIds = result.FriendIds ?? new();
                    _pendingRequestReceiverIds = result.PendingRequestReceiverIds ?? new();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                _loading.IsVisible = _loading.IsRunning = false;
            }
        }
    }
}