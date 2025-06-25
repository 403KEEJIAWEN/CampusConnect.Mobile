using Microsoft.Maui.Controls;
using CampusConnect.Mobile.Services;
using CampusConnect.Mobile.Models;
using CampusConnect.Mobile.Views;

#nullable enable

namespace CampusConnect.Mobile.Views
{
    public partial class LoginPage : ContentPage
    {
        private Entry _emailEntry;
        private Entry _passwordEntry;
        private Button _loginButton;
        private readonly ApiService _apiService;

        public LoginPage()
        {
            System.Diagnostics.Debug.WriteLine("LoginPage constructor called");

            Title = "Login";
            BackgroundColor = Color.FromArgb("#F8F9FA");
            _apiService = new ApiService();
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeControls();
            CreateLayout();
        }

        private void InitializeControls()
        {
            _emailEntry = new Entry
            {
                Placeholder = "Email address",
                Text = "",
                Keyboard = Keyboard.Email,
                BackgroundColor = Colors.Transparent,
                FontSize = 16,
                HeightRequest = 50,
                Margin = new Thickness(15, 0),
                TextColor = Color.FromArgb("#374151")
            };

            _passwordEntry = new Entry
            {
                Placeholder = "Password",
                Text = "",
                IsPassword = true,
                BackgroundColor = Colors.Transparent,
                FontSize = 16,
                HeightRequest = 50,
                Margin = new Thickness(15, 0),
                TextColor = Color.FromArgb("#374151")
            };

            _loginButton = new Button
            {
                Text = "Sign In",
                BackgroundColor = Color.FromArgb("#E91E63"),
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Bold,
                HeightRequest = 50,
                CornerRadius = 16,
                FontSize = 18,
                Margin = new Thickness(0, 10, 0, 10)
            };
            _loginButton.Clicked += OnLoginClicked;
        }

        private void CreateLayout()
        {
            // Email row with icon
            var emailRow = new Frame
            {
                CornerRadius = 12,
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#E9D5EC"),
                Padding = new Thickness(0),
                Margin = new Thickness(0, 0, 0, 10),
                HasShadow = false,
                HeightRequest = 55,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Image
                        {
                            Source = "email_icon.png",
                            HeightRequest = 20,
                            WidthRequest = 20,
                            Margin = new Thickness(15, 0, 0, 0),
                            VerticalOptions = LayoutOptions.Center
                        },
                        _emailEntry
                    }
                }
            };

            // Password row with icon
            var passwordRow = new Frame
            {
                CornerRadius = 12,
                BackgroundColor = Colors.White,
                BorderColor = Color.FromArgb("#E9D5EC"),
                Padding = new Thickness(0),
                Margin = new Thickness(0, 0, 0, 20),
                HasShadow = false,
                HeightRequest = 55,
                Content = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        new Image
                        {
                            Source = "lock_icon.png",
                            HeightRequest = 20,
                            WidthRequest = 20,
                            Margin = new Thickness(15, 0, 0, 0),
                            VerticalOptions = LayoutOptions.Center
                        },
                        _passwordEntry
                    }
                }
            };

            var signUpLabel = new Label
            {
                Text = "Don't have an account? ",
                FontSize = 14,
                TextColor = Color.FromArgb("#374151"),
                HorizontalTextAlignment = TextAlignment.Center
            };

            var signUpLink = new Label
            {
                Text = "Sign up",
                TextColor = Color.FromArgb("#E91E63"),
                FontSize = 14,
                FontAttributes = FontAttributes.Bold
            };

            var signUpTap = new TapGestureRecognizer();
            signUpLink.GestureRecognizers.Add(signUpTap);
            signUpTap.Tapped += OnRegisterLinkTapped;

            var signUpStack = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Children = { signUpLabel, signUpLink }
            };

            var card = new Frame
            {
                CornerRadius = 20,
                Padding = new Thickness(30, 30, 30, 20),
                BackgroundColor = Colors.White,
                HasShadow = false,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Content = new StackLayout
                {
                    Spacing = 10,
                    HorizontalOptions = LayoutOptions.Fill,
                    Children =
                    {
                        // Avatar circle
                        new Frame
                        {
                            CornerRadius = 50,
                            HeightRequest = 80,
                            WidthRequest = 80,
                            BackgroundColor = Color.FromArgb("#E91E63"),
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Padding = new Thickness(0),
                            Content = new Label
                            {
                                Text = "CC",
                                TextColor = Colors.White,
                                FontAttributes = FontAttributes.Bold,
                                FontSize = 30,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center,
                                HorizontalTextAlignment = TextAlignment.Center,
                                VerticalTextAlignment = TextAlignment.Center
                            }
                        },
                        new Label
                        {
                            Text = "Welcome Back",
                            FontSize = 28,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#E91E63"),
                            HorizontalTextAlignment = TextAlignment.Center,
                            Margin = new Thickness(0, 10, 0, 0)
                        },
                        new Label
                        {
                            Text = "Sign in to your CampusConnect account",
                            FontSize = 15,
                            TextColor = Color.FromArgb("#6B7280"),
                            HorizontalTextAlignment = TextAlignment.Center,
                            Margin = new Thickness(0, 0, 0, 10)
                        },
                        emailRow,
                        passwordRow,
                        _loginButton,
                        signUpStack
                    }
                }
            };

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(20, 60),
                    VerticalOptions = LayoutOptions.Center,
                    Children = { card }
                }
            };
        }

        private async void OnRegisterLinkTapped(object sender, EventArgs e)
        {
            try
            {
                // Fixed: Add NavigationPage wrapper like in your working code
                Application.Current.MainPage = new NavigationPage(new RegistrationPage());
            }
            catch (Exception)
            {
                await DisplayAlert("Error", "Navigation failed", "OK");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = _emailEntry?.Text?.Trim() ?? "";
            var password = _passwordEntry?.Text ?? "";

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayAlert("Error", "Please enter both email and password", "OK");
                return;
            }

            _loginButton.Text = "Signing in...";
            _loginButton.IsEnabled = false;

            try
            {
                // Use fully qualified name like in your working code
                var loginRequest = new CampusConnect.Mobile.Models.LoginRequest
                {
                    Email = email,
                    Password = password
                };

                var result = await _apiService.LoginAsync(loginRequest);

                if (result.Success)
                {
                    await DisplayAlert("Success", "Login successful! 🎉", "OK");
                    Application.Current.MainPage = new MainMenuPage(email);
                }
                else
                {
                    var errorMessage = result.Message;
                    if (result.Errors?.Any() == true)
                        errorMessage += "\n\n" + string.Join("\n", result.Errors);

                    await DisplayAlert("Login Failed", errorMessage, "Try Again");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Login failed: {ex.Message}", "OK");
            }
            finally
            {
                _loginButton.Text = "Sign In";
                _loginButton.IsEnabled = true;
            }
        }
    }
}