using Microsoft.Maui.Controls;
using CampusConnect.Mobile.Services;
using CampusConnect.Mobile.Models;

#nullable enable

namespace CampusConnect.Mobile.Views
{
    public partial class RegistrationPage : ContentPage
    {
        private readonly ApiService _apiService;
        private Entry _nameEntry;
        private Entry _emailEntry;
        private Entry _contactEntry; // <-- Added contact entry
        private Entry _passwordEntry;
        private Entry _confirmPasswordEntry;
        private Entry _studentIdEntry;
        private Picker _genderPicker;
        private Picker _facultyPicker;
        private Picker _coursePicker;
        private Picker _yearPicker;
        private Button _registerButton;

        public RegistrationPage()
        {
            Title = "Join CampusConnect";
            BackgroundColor = Color.FromArgb("#F8F9FA");
            NavigationPage.SetHasNavigationBar(this, false);
            _apiService = new ApiService();

            InitializeControls();
            CreateLayout();
        }

        private void InitializeControls()
        {
            _nameEntry = new Entry
            {
                Placeholder = "Full name",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            _emailEntry = new Entry
            {
                Placeholder = "Email address",
                Keyboard = Keyboard.Email,
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            _contactEntry = new Entry // <-- Contact field
            {
                Placeholder = "Contact number",
                Keyboard = Keyboard.Telephone,
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            _studentIdEntry = new Entry
            {
                Placeholder = "Student ID",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            _genderPicker = new Picker
            {
                Title = "Select gender",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };
            _genderPicker.ItemsSource = new string[] { "Male", "Female"};

            _facultyPicker = new Picker
            {
                Title = "Select faculty",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };
            _facultyPicker.ItemsSource = new string[] { "FKAAB", "FKEE", "FSKTM", "FKMP", "FPTP", "FPTV", "FAST", "FTK" };

            _coursePicker = new Picker
            {
                Title = "Select course",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };
            _coursePicker.ItemsSource = new string[] { "BFF", "BFR", "BEJ", "KEE", "MEE", "MET", "PEE", "BIW", "BIM", "BIP", "BIS", "BIT", "BDC", "BDD", "BDJ", "BDM", "BDX", "BPA", "BPB", "BPC", "BPD", "MPA", "MPC", "MPE", "BBA", "BBC", "BBD", "BBE", "BBG", "BBDN", "BBO", "BBX", "BBY", "BBZ", "MBV", "PBD", "BWA", "BWC", "BWD", "BWK", "BWQ", "BWS", "BWW", "BNA", "BNB", "BNC", "BND", "BNE", "BNM", "BNS", "BNT" };

            _yearPicker = new Picker
            {
                Title = "Select year",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };
            _yearPicker.ItemsSource = new string[] { "1", "2", "3", "4" };

            _passwordEntry = new Entry
            {
                Placeholder = "Password (must be at least 6 characters)",
                IsPassword = true,
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            _confirmPasswordEntry = new Entry
            {
                Placeholder = "Confirm Password",
                IsPassword = true,
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            _registerButton = new Button
            {
                Text = "Create Account",
                BackgroundColor = Color.FromArgb("#E91E63"),
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Bold,
                HeightRequest = 50,
                Margin = new Thickness(0, 20, 0, 10)
            };

            _registerButton.Clicked += OnRegisterClicked;
        }

        private void CreateLayout()
        {
            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Padding = new Thickness(30),
                    Spacing = 10,
                    Children =
                    {
                        new Label
                        {
                            Text = "Join CampusConnect",
                            FontSize = 28,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#E91E63"),
                            HorizontalTextAlignment = TextAlignment.Center
                        },
                        new Label
                        {
                            Text = "Create your account and start connecting",
                            FontSize = 14,
                            TextColor = Colors.Gray,
                            HorizontalTextAlignment = TextAlignment.Center,
                            Margin = new Thickness(0,0,0,20)
                        },
                        _nameEntry,
                        _emailEntry,
                        _contactEntry, // <-- Add contact to layout
                        _studentIdEntry,
                        _genderPicker,
                        _facultyPicker,
                        _coursePicker,
                        _yearPicker,
                        _passwordEntry,
                        _confirmPasswordEntry,
                        _registerButton,
                        new Button
                        {
                            Text = "Already have an account? Sign in",
                            BackgroundColor = Colors.Transparent,
                            TextColor = Color.FromArgb("#E91E63"),
                            Command = new Command(() => Navigation.PushAsync(new LoginPage())),
                            FontSize = 14
                        }
                    }
                }
            };
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_nameEntry.Text) ||
                string.IsNullOrWhiteSpace(_emailEntry.Text) ||
                string.IsNullOrWhiteSpace(_contactEntry.Text) || // <-- Contact required
                string.IsNullOrWhiteSpace(_passwordEntry.Text) ||
                string.IsNullOrWhiteSpace(_confirmPasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(_studentIdEntry.Text) ||
                _facultyPicker.SelectedIndex == -1 ||
                _coursePicker.SelectedIndex == -1 ||
                _yearPicker.SelectedIndex == -1 ||
                _genderPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Please fill all required fields.", "OK");
                return;
            }

            if (_passwordEntry.Text != _confirmPasswordEntry.Text)
            {
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            var registrationRequest = new RegistrationRequest
            {
                Name = _nameEntry.Text.Trim(),
                Email = _emailEntry.Text.Trim(),
                Contact = _contactEntry.Text.Trim(), // <-- Include contact
                Password = _passwordEntry.Text,
                ConfirmPassword = _confirmPasswordEntry.Text,
                MatricNumber = _studentIdEntry.Text.Trim(),
                Gender = _genderPicker.SelectedItem?.ToString() ?? "",
                Faculty = _facultyPicker.SelectedItem?.ToString() ?? "",
                Course = _coursePicker.SelectedItem?.ToString() ?? "",
                Year = int.Parse(_yearPicker.SelectedItem?.ToString() ?? "1")
            };

            _registerButton.IsEnabled = false;
            _registerButton.Text = "Creating...";

            var response = await _apiService.RegisterAsync(registrationRequest);

            if (response.Success)
            {
                await DisplayAlert("Success", "Registration successful!", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
            else
            {
                await DisplayAlert("Error", response.Message, "OK");
            }

            _registerButton.IsEnabled = true;
            _registerButton.Text = "Create Account";
        }
    }
}
