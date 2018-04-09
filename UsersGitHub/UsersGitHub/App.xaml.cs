using System.Linq;
using System.Reactive.Linq;
using Akavache;
using UsersGitHub.Model;
using UsersGitHub.View;
using Xamarin.Forms;

namespace UsersGitHub
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            BlobCache.ApplicationName = "UsersGitHub";
            MainPage = new Page();
        }

        protected override async void OnStart()
        {
            var userCollection = await BlobCache.UserAccount.GetAllObjects<User>();
            if (userCollection.Any())
            {
                MainPage = new UsersReposPage();
            }
            else
            {
                MainPage = new NavigationPage(new LoginPage());
            }
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
