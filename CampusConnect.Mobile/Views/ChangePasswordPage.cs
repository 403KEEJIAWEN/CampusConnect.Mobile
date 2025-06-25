using CampusConnect.Mobile.Models;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using System.Text;
using System.Net.Http;

namespace CampusConnect.Mobile.Views
{
    public class ChangePasswordPage : ContentPage
    {
        private readonly string _userEmail;
        private readonly ApiService _apiService = new ApiService();

        private Entry currentPwEntry, newPwEntry, confirmPwEntry;

        public ChangePasswordPage(string userEmail)
        {
            _userEmail = userEmail;
            Title = "Change Password";

            currentPwEntry = new Entry { Placeholder = "Current Password", IsPassword = true };
            newPwEntry = new Entry { Placeholder = "New Password (min 6 characters)", IsPassword = true };
            confirmPwEntry = new Entry { Placeholder = "Confirm New Password", IsPassword = true };

            var changeBtn = new Button
            {
                Text = "Change Password",
                BackgroundColor = Color.FromArgb("#E91E63"), // Pink color
                TextColor = Colors.White
            };

            changeBtn.Clicked += async (s, e) => await ChangePassword();

            Content = new StackLayout
            {
                Padding = 25,
                Spacing = 14,
                Children =
                {
                    new Label { Text = "Change your password:", FontAttributes = FontAttributes.Bold, FontSize = 18 },
                    currentPwEntry,
                    newPwEntry,
                    confirmPwEntry,
                    changeBtn
                }
            };
        }



        private async Task ChangePassword()
        {
            if (string.IsNullOrWhiteSpace(currentPwEntry.Text) ||
                string.IsNullOrWhiteSpace(newPwEntry.Text) ||
                string.IsNullOrWhiteSpace(confirmPwEntry.Text))
            {
                await DisplayAlert("Validation", "All fields are required.", "OK");
                return;
            }

            if (newPwEntry.Text.Length < 6)
            {
                await DisplayAlert("Validation", "New password must be at least 6 characters long.", "OK");
                return;
            }

            if (newPwEntry.Text != confirmPwEntry.Text)
            {
                await DisplayAlert("Validation", "New password and confirmation do not match.", "OK");
                return;
            }

            try
            {
                var apiResult = await _apiService.ChangePasswordAsync(
                _userEmail,
                currentPwEntry.Text ?? "",
                newPwEntry.Text ?? "",
                confirmPwEntry.Text ?? ""
);


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
