using System;
using System.Threading.Tasks;
using CampusConnect.Mobile.Models;
using CampusConnect.Mobile.Services;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace CampusConnect.Mobile.Views
{
    public class PendingRequestsPage : ContentPage
    {
        readonly ApiService _apiService = new();
        readonly ObservableCollection<PendingFriendRequestViewModel> _pendingRequests = new();
        readonly ListView _pendingListView;
        readonly ActivityIndicator _loading = new ActivityIndicator { IsVisible = false, IsRunning = false };
        readonly string _userEmail;

        public PendingRequestsPage(string userEmail)
        {
            Title = "Pending Requests";
            _userEmail = userEmail;

            _pendingListView = new ListView
            {
                ItemsSource = _pendingRequests,
                ItemTemplate = new DataTemplate(() =>
                {
                    var name = new Label { FontAttributes = FontAttributes.Bold, FontSize = 17 };
                    name.SetBinding(Label.TextProperty, "SenderName");

                    var matric = new Label { FontSize = 14, TextColor = Color.FromArgb("#808080") };
                    matric.SetBinding(Label.TextProperty, "MatricNumber");

                    var email = new Label { FontSize = 13, TextColor = Colors.Gray };
                    email.SetBinding(Label.TextProperty, "Email");

                    var acceptBtn = new Button
                    {
                        Text = "Accept",
                        BackgroundColor = Colors.SeaGreen,
                        TextColor = Colors.White,
                        FontSize = 13,
                        HeightRequest = 32,
                        CornerRadius = 10,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(0, 0, 5, 0)
                    };

                    var declineBtn = new Button
                    {
                        Text = "Decline",
                        BackgroundColor = Colors.IndianRed,
                        TextColor = Colors.White,
                        FontSize = 13,
                        HeightRequest = 32,
                        CornerRadius = 10,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(5, 0, 0, 0)
                    };

                    acceptBtn.Clicked += async (s, e) =>
                    {
                        if (acceptBtn.BindingContext is PendingFriendRequestViewModel req)
                        {
                            _loading.IsRunning = _loading.IsVisible = true;
                            var success = await _apiService.AcceptFriendRequestAsync(_userEmail, req.RequestId);
                            await DisplayAlert(success ? "Success" : "Error", success ? "Friend accepted!" : "Failed to accept.", "OK");
                            await RefreshPendingAsync();
                            _loading.IsRunning = _loading.IsVisible = false;
                        }
                    };

                    declineBtn.Clicked += async (s, e) =>
                    {
                        if (declineBtn.BindingContext is PendingFriendRequestViewModel req)
                        {
                            _loading.IsRunning = _loading.IsVisible = true;
                            var success = await _apiService.DeclineFriendRequestAsync(_userEmail, req.RequestId);
                            await DisplayAlert(success ? "Declined" : "Error", success ? "Request declined!" : "Failed to decline.", "OK");
                            await RefreshPendingAsync();
                            _loading.IsRunning = _loading.IsVisible = false;
                        }
                    };

                    var infoStack = new StackLayout
                    {
                        Spacing = 2,
                        Children = { name, matric, email }
                    };

                    // Use a Grid for button row (much better for responsive sizing)
                    var buttonGrid = new Grid
                    {
                        ColumnSpacing = 10,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Margin = new Thickness(0, 4, 0, 0)
                    };
                    buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    buttonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    // Correct way for all versions:
                    buttonGrid.Children.Add(acceptBtn);
                    buttonGrid.Children.Add(declineBtn);

                    Grid.SetColumn(acceptBtn, 0);
                    Grid.SetRow(acceptBtn, 0);

                    Grid.SetColumn(declineBtn, 1);
                    Grid.SetRow(declineBtn, 0);



                    var outer = new Frame
                    {
                        CornerRadius = 15,
                        Margin = new Thickness(0, 6, 0, 6),
                        Padding = new Thickness(15, 10),
                        BackgroundColor = Color.FromArgb("#F8F9FA"),
                        HasShadow = true,
                        Content = new StackLayout
                        {
                            Spacing = 8,
                            Children = { infoStack, buttonGrid }
                        }
                    };

                    return new ViewCell { View = outer };
                }),

                SeparatorVisibility = SeparatorVisibility.None,
                RowHeight = 180// Good for vertical layout
            };


            Content = new StackLayout
            {
                Padding = 12,
                Children =
                {
                    new Label { Text = "Pending Friend Requests:", FontAttributes = FontAttributes.Bold },
                    _loading,
                    _pendingListView
                }
            };

            Appearing += async (s, e) => await RefreshPendingAsync();
        }

        private async Task RefreshPendingAsync()
        {
            _loading.IsVisible = _loading.IsRunning = true;
            var result = await _apiService.GetPendingRequestsAsync(_userEmail);
            _pendingRequests.Clear();
            foreach (var req in result)
                _pendingRequests.Add(req);
            _loading.IsVisible = _loading.IsRunning = false;
        }
    }
}
