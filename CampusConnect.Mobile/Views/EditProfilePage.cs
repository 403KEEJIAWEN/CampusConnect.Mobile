using CampusConnect.Mobile.Models;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System;

namespace CampusConnect.Mobile.Views
{
    public class EditProfilePage : ContentPage
    {
        private readonly string _userEmail;
        private readonly ApiService _apiService = new ApiService();

        private Entry nameEntry, emailEntry, contactEntry, matricEntry;
        private Picker genderPicker, facultyPicker, coursePicker, yearPicker;
        private Button saveBtn;

        // Picker data
        private static readonly string[] GenderOptions = { "Male", "Female", "Other" };
        private static readonly string[] FacultyOptions = { "FKAAB", "FKEE", "FSKTM", "FKMP", "FPTP", "FPTV", "FAST", "FTK" };
        private static readonly string[] CourseOptions = { "BFF", "BFR", "BEJ", "KEE", "MEE", "MET", "PEE", "BIW", "BIM", "BIP", "BIS", "BIT", "BDC", "BDD", "BDJ", "BDM", "BDX", "BPA", "BPB", "BPC", "BPD", "MPA", "MPC", "MPE", "BBA", "BBC", "BBD", "BBE", "BBG", "BBDN", "BBO", "BBX", "BBY", "BBZ", "MBV", "PBD", "BWA", "BWC", "BWD", "BWK", "BWQ", "BWS", "BWW", "BNA", "BNB", "BNC", "BND", "BNE", "BNM", "BNS", "BNT" };
        private static readonly string[] YearOptions = { "1", "2", "3", "4" };

        public EditProfilePage(string userEmail)
        {
            _userEmail = userEmail;
            Title = "Edit Profile";
            BackgroundColor = Color.FromArgb("#F8F9FA");

            nameEntry = new Entry
            {
                Placeholder = "Full name",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            emailEntry = new Entry
            {
                Placeholder = "Email",
                Keyboard = Keyboard.Email,
                BackgroundColor = Colors.White,
                IsReadOnly = true,
                IsEnabled = false, // Make it visually and functionally read-only
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            contactEntry = new Entry
            {
                Placeholder = "Contact number",
                Keyboard = Keyboard.Telephone,
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            matricEntry = new Entry
            {
                Placeholder = "Matric Number",
                BackgroundColor = Colors.White,
                IsReadOnly = true,
                IsEnabled = false, // Make it visually and functionally read-only
                HeightRequest = 50,
                Margin = new Thickness(0, 5)
            };

            genderPicker = new Picker
            {
                Title = "Select gender",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5),
                ItemsSource = GenderOptions,
                IsEnabled = false // Make it read-only
            };

            facultyPicker = new Picker
            {
                Title = "Select faculty",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5),
                ItemsSource = FacultyOptions
            };

            coursePicker = new Picker
            {
                Title = "Select course",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5),
                ItemsSource = CourseOptions
            };

            yearPicker = new Picker
            {
                Title = "Select year",
                BackgroundColor = Colors.White,
                HeightRequest = 50,
                Margin = new Thickness(0, 5),
                ItemsSource = YearOptions
            };

            saveBtn = new Button
            {
                Text = "Save",
                BackgroundColor = Color.FromArgb("#E91E63"),
                TextColor = Colors.White,
                FontAttributes = FontAttributes.Bold,
                HeightRequest = 50,
                Margin = new Thickness(0, 20, 0, 10)
            };
            saveBtn.Clicked += async (s, e) => await SaveProfile();

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
                            Text = "Edit your profile",
                            FontSize = 28,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromArgb("#E91E63"),
                            HorizontalTextAlignment = TextAlignment.Center
                        },
                        nameEntry,
                        emailEntry,
                        contactEntry,
                        matricEntry,
                        genderPicker,
                        facultyPicker,
                        coursePicker,
                        yearPicker,
                        saveBtn
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

            nameEntry.Text = friend.Name;
            emailEntry.Text = friend.Email;
            contactEntry.Text = friend.Contact;
            matricEntry.Text = friend.MatricNumber;

            // Set pickers
            genderPicker.SelectedIndex = Array.IndexOf(GenderOptions, friend.Gender);
            facultyPicker.SelectedIndex = Array.IndexOf(FacultyOptions, friend.Faculty);
            coursePicker.SelectedIndex = Array.IndexOf(CourseOptions, friend.Course);
            yearPicker.SelectedIndex = Array.IndexOf(YearOptions, friend.Year.ToString());
        }

        private async Task SaveProfile()
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text))
            {
                await DisplayAlert("Validation", "Name is required.", "OK");
                return;
            }

            if (facultyPicker.SelectedIndex == -1 ||
                coursePicker.SelectedIndex == -1 ||
                yearPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Validation", "Faculty, Course, and Year are required.", "OK");
                return;
            }

            if (!int.TryParse(yearPicker.SelectedItem?.ToString(), out int year))
            {
                await DisplayAlert("Validation", "Year must be a number.", "OK");
                return;
            }

            var model = new EditProfileViewModel
            {
                Name = nameEntry.Text.Trim(),
                MatricNumber = matricEntry.Text ?? "",
                Gender = genderPicker.SelectedItem?.ToString() ?? "",
                Course = coursePicker.SelectedItem?.ToString() ?? "",
                Email = emailEntry.Text ?? "",
                Contact = contactEntry.Text ?? "",
                Faculty = facultyPicker.SelectedItem?.ToString() ?? "",
                Year = year
            };

            try
            {
                var apiResult = await _apiService.EditProfileAsync(model);

                if (apiResult.Success)
                {
                    await DisplayAlert("Success", apiResult.Message, "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Error", apiResult.Message, "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
