using CampusConnect.Mobile.Models;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace CampusConnect.Mobile.Views
{
    public class ProfilePage : ContentPage
    {
        private readonly string _userEmail;
        private readonly ApiService _apiService = new ApiService();

        // Color palette
        readonly Color Pink = Color.FromArgb("#EF4CA6");
        readonly Color LightPink = Color.FromArgb("#FFF1F6");
        readonly Color Purple = Color.FromArgb("#778899");
        readonly Color BgGray = Color.FromArgb("#F7F8FA");

        // Profile info labels (for later updating)
        private Label nameValue, matricValue, genderValue, courseValue, emailValue, contactValue, facultyValue, yearValue;

        public ProfilePage(string userEmail)
        {
            _userEmail = userEmail;
            Title = "My Profile";
            BackgroundColor = BgGray;

            // Profile icon (FontAwesome: \uf007 or system default)
            var profileIcon = new Label
            {
                Text = "\uf007",
                FontFamily = "FASolid", // Requires FontAwesome in your project. Otherwise, comment this line.
                FontSize = 62,
                TextColor = Pink,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 8)
            };

            // Name (big and bold)
            nameValue = new Label
            {
                FontSize = 23,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Purple
            };

            // Each profile field row (label with icon and value)
            var infoStack = new StackLayout
            {
                Spacing = 10,
                Children =
                {
                    CreateFieldRow("\uf2bb", "Matric No", out matricValue),  // id-badge
                    CreateFieldRow("\uf228", "Gender", out genderValue),     // genderless
                    CreateFieldRow("\uf19d", "Course", out courseValue),     // book
                    CreateFieldRow("\uf0e0", "Email", out emailValue),       // envelope
                    CreateFieldRow("\uf095", "Contact", out contactValue),   // phone
                    CreateFieldRow("\uf19c", "Faculty", out facultyValue),   // university
                    CreateFieldRow("\uf073", "Year", out yearValue),         // calendar
                }
            };

            // Card for profile
            var card = new Frame
            {
                BackgroundColor = Colors.White,
                CornerRadius = 18,
                Padding = new Thickness(22, 18),
                Margin = new Thickness(18, 24),
                HasShadow = true,
                Content = new StackLayout
                {
                    Spacing = 16,
                    Children =
                    {
                        profileIcon,
                        nameValue,
                        new BoxView { HeightRequest = 2, Color = LightPink, Margin = new Thickness(20, 0) },
                        infoStack
                    }
                }
            };

            // Buttons below card
            var editBtn = new Button
            {
                Text = "Edit Profile",
                BackgroundColor = Pink,
                TextColor = Colors.White,
                CornerRadius = 16,
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 5)
            };
            editBtn.Clicked += async (s, e) => await Navigation.PushAsync(new EditProfilePage(_userEmail));

            var changePwBtn = new Button
            {
                Text = "Change Password",
                BackgroundColor = Purple,
                TextColor = Colors.White,
                CornerRadius = 16,
                FontAttributes = FontAttributes.Bold,
                FontSize = 16,
                Margin = new Thickness(0, 0, 0, 24)
            };
            changePwBtn.Clicked += async (s, e) => await Navigation.PushAsync(new ChangePasswordPage(_userEmail));

            Content = new ScrollView
            {
                Content = new StackLayout
                {
                    Spacing = 0,
                    Children =
                    {
                        card,
                        new StackLayout
                        {
                            Margin = new Thickness(24, 0, 24, 0),
                            Spacing = 12,
                            Children =
                            {
                                editBtn,
                                changePwBtn
                            }
                        }
                    }
                }
            };
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadProfile();
        }

        private async Task LoadProfile()
        {
            var friend = await _apiService.GetFriendByEmailAsync(_userEmail);
            if (friend == null)
            {
                await DisplayAlert("Error", "Profile not found.", "OK");
                await Navigation.PopAsync();
                return;
            }

            nameValue.Text = friend.Name;
            matricValue.Text = friend.MatricNumber;
            genderValue.Text = friend.Gender;
            courseValue.Text = friend.Course;
            emailValue.Text = friend.Email;
            contactValue.Text = friend.Contact;
            facultyValue.Text = friend.Faculty;
            yearValue.Text = friend.Year.ToString();
        }

        /// <summary>
        /// Helper for creating a field row with icon and value
        /// </summary>
        private StackLayout CreateFieldRow(string icon, string label, out Label valueLabel)
        {
            valueLabel = new Label
            {
                FontSize = 16,
                TextColor = Colors.Black,
                VerticalOptions = LayoutOptions.Center
            };

            // Row: [icon] [label:] [value]
            return new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 8,
                Children =
                {
                    new Label
                    {
                        Text = icon,
                        FontFamily = "FASolid", // FontAwesome (see note below)
                        FontSize = 18,
                        TextColor = Pink,
                        VerticalOptions = LayoutOptions.Center
                    },
                    new Label
                    {
                        Text = $"{label}:",
                        TextColor = Color.FromArgb("#808080"),
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold,
                        VerticalOptions = LayoutOptions.Center
                    },
                    valueLabel
                }
            };
        }
    }
}
