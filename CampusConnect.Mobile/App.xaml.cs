using CampusConnect.Mobile.Views;
namespace CampusConnect.Mobile;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new NavigationPage(new LoginPage());
    }
}
