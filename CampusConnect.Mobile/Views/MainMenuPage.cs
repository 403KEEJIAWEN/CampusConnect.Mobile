using Microsoft.Maui.Controls;
using System.Collections.Generic;

namespace CampusConnect.Mobile.Views
{
    public class MainMenuPage : FlyoutPage
    {
        private string _userEmail;

        public MainMenuPage(string userEmail)
        {
            _userEmail = userEmail;
            Title = "CampusConnect";

            // Menu items with FontAwesome icons
            var menuList = new List<MenuItemModel>
            {
                new MenuItemModel("Posts", "\uf015"),        // fa-home
                new MenuItemModel("Friends", "\uf0c0"),      // fa-users
                new MenuItemModel("Create Post", "\uf067"),  // fa-plus
                new MenuItemModel("My Posts", "\uf02d"),     // fa-book
                new MenuItemModel("Profile", "\uf007")       // fa-user
            };

            var listView = new ListView
            {
                ItemsSource = menuList,
                SeparatorVisibility = SeparatorVisibility.None,
                BackgroundColor = Colors.Transparent,
                Margin = new Thickness(0, 0, 0, 0),
                RowHeight = 55, // Increased row height for better spacing
                ItemTemplate = new DataTemplate(() =>
                {
                    var icon = new Label
                    {
                        FontSize = 18,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromArgb("#374151"),
                        FontFamily = "FASolid",
                    };
                    var text = new Label
                    {
                        FontSize = 17,
                        VerticalOptions = LayoutOptions.Center,
                        TextColor = Color.FromArgb("#374151"),
                        Margin = new Thickness(16, 0, 0, 0)
                    };
                    icon.SetBinding(Label.TextProperty, "Icon");
                    text.SetBinding(Label.TextProperty, "Title");

                    var grid = new Grid
                    {
                        Padding = new Thickness(18, 15), // Increased padding for better spacing
                        ColumnDefinitions =
                        {
                            new ColumnDefinition { Width = GridLength.Auto },
                            new ColumnDefinition { Width = GridLength.Star }
                        }
                    };
                    Grid.SetColumn(icon, 0);
                    Grid.SetRow(icon, 0);
                    Grid.SetColumn(text, 1);
                    Grid.SetRow(text, 0);
                    grid.Children.Add(icon);
                    grid.Children.Add(text);

                    return new ViewCell { View = grid };
                }),
                SelectionMode = ListViewSelectionMode.None
            };

            // Navigation logic
            listView.ItemTapped += (s, e) =>
            {
                if (e.Item is MenuItemModel item)
                {
                    switch (item.Title)
                    {
                        case "Friends":
                            Detail = CreateNavigationPage(new SearchFriendsPage(_userEmail));
                            break;
                        case "Posts":
                            Detail = CreateNavigationPage(new PostPage(_userEmail));
                            break;
                        case "My Posts":
                            Detail = CreateNavigationPage(new MyPostsPage(_userEmail));
                            break;
                        case "Create Post":
                            Detail = CreateNavigationPage(new CreatePostPage(_userEmail));
                            break;
                        case "Profile":
                            Detail = CreateNavigationPage(new ProfilePage(_userEmail));
                            break;
                    }
                    IsPresented = false;
                }
            };

            // Header StackLayout
            var headerStackLayout = new StackLayout
            {
                Padding = new Thickness(24, 32, 0, 16),
                Orientation = StackOrientation.Horizontal,
                Spacing = 14,
                Children =
                {
                    new Frame
                    {
                        CornerRadius = 32,
                        HeightRequest = 48,
                        WidthRequest = 48,
                        BackgroundColor = Color.FromArgb("#EF4CA6"),
                        Padding = 0,
                        Content = new Label
                        {
                            Text = "CC",
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            FontSize = 22,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Colors.White
                        }
                    },
                    new StackLayout
                    {
                        VerticalOptions = LayoutOptions.Center,
                        Spacing = 0,
                        Children =
                        {
                            new Label
                            {
                                FormattedText = new FormattedString
                                {
                                    Spans =
                                    {
                                        new Span { Text = "Campus", FontAttributes = FontAttributes.Bold, FontSize = 21, TextColor = Color.FromArgb("#EF4CA6") },
                                        new Span { Text = "Connect", FontAttributes = FontAttributes.Bold, FontSize = 21, TextColor = Color.FromArgb("#374151") }
                                    }
                                }
                            },
                            new Label
                            {
                                Text = "Stay connected",
                                FontSize = 13,
                                TextColor = Color.FromArgb("#6B7280"),
                                Margin = new Thickness(0, 2, 0, 0)
                            }
                        }
                    }
                }
            };

            // Navigation label
            var navigationLabel = new Label
            {
                Text = "Navigation",
                FontAttributes = FontAttributes.Bold,
                FontSize = 15,
                TextColor = Color.FromArgb("#EF4CA6"),
                Margin = new Thickness(24, 0, 0, 12) // Increased bottom margin
            };

            // Menu list container
            var menuContainer = new StackLayout
            {
                Children = { listView }
            };

            // Settings and Logout section (moved up and improved)
            var actionButtonsSection = new StackLayout
            {
                Padding = new Thickness(24, 20, 24, 22), // Moved up with more top padding
                Spacing = 12,
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 12,
                        Children =
                        {
                            new Button
                            {
                                Text = "\uf013 Settings", // FontAwesome gear icon
                                CornerRadius = 18,
                                BorderColor = Color.FromArgb("#EF4CA6"),
                                BorderWidth = 1,
                                TextColor = Color.FromArgb("#EF4CA6"),
                                BackgroundColor = Colors.Transparent,
                                FontSize = 15,
                                FontFamily = "FASolid",
                                Padding = new Thickness(16, 8),
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Command = new Command(() =>
                                {
                                    Detail = CreateNavigationPage(new ChangePasswordPage(_userEmail));
                                    IsPresented = false;
                                })
                            },
                            new Button
                            {
                                Text = "\uf2f5 Logout", // FontAwesome sign-out icon
                                CornerRadius = 18,
                                BackgroundColor = Color.FromArgb("#EF4CA6"),
                                TextColor = Colors.White,
                                FontSize = 15,
                                FontFamily = "FASolid",
                                Padding = new Thickness(16, 8),
                                HorizontalOptions = LayoutOptions.FillAndExpand,
                                Command = new Command(() =>
                                {
                                    Application.Current.MainPage = new LoginPage();
                                })
                            }
                        }
                    }
                }
            };

            // Main grid
            var mainGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },    // Header
                    new RowDefinition { Height = GridLength.Auto },    // Navigation label
                    new RowDefinition { Height = GridLength.Star },    // Menu List
                    new RowDefinition { Height = GridLength.Auto },    // Action buttons section
                }
            };

            // Add children to grid with proper row assignment
            Grid.SetRow(headerStackLayout, 0);
            Grid.SetRow(navigationLabel, 1);
            Grid.SetRow(menuContainer, 2);
            Grid.SetRow(actionButtonsSection, 3);

            mainGrid.Children.Add(headerStackLayout);
            mainGrid.Children.Add(navigationLabel);
            mainGrid.Children.Add(menuContainer);
            mainGrid.Children.Add(actionButtonsSection);

            // Sidebar (Flyout) content
            Flyout = new ContentPage
            {
                Title = "Menu",
                BackgroundColor = Color.FromArgb("#F8F9FA"),
                Content = mainGrid
            };

            // Default page after login
            Detail = CreateNavigationPage(new SearchFriendsPage(_userEmail));
        }

        // Helper method to create NavigationPage with consistent styling
        private NavigationPage CreateNavigationPage(ContentPage page)
        {
            var navPage = new NavigationPage(page);
            navPage.BarBackgroundColor = Color.FromArgb("#E91E63");
            navPage.BarTextColor = Colors.White;
            return navPage;
        }

        // Menu item class
        class MenuItemModel
        {
            public string Title { get; }
            public string Icon { get; }
            public MenuItemModel(string title, string icon)
            {
                Title = title;
                Icon = icon;
            }
        }
    }
}