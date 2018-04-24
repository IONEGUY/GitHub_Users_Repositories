using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using Akavache;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Navigation;
using UsersGitHub.Controls;
using UsersGitHub.Model;
using UsersGitHub.Views;
using Xamarin.Forms;

namespace UsersGitHub
{
    public partial class App
    {
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            BlobCache.ApplicationName = "UsersGitHub";
            MainPage = new Page();
            var userCollection = await BlobCache.UserAccount.GetAllObjects<User>();
            if (userCollection.Any())
            {
                NavigationService.NavigateAsync(nameof(LoginPage)).Wait();
            }
            else
            {
                NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LoginPage)}").Wait();
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Repos>();
            containerRegistry.RegisterForNavigation<Settings>();
            containerRegistry.RegisterForNavigation<Users>();
            containerRegistry.RegisterForNavigation<LoginPage>();
        }
    }
}
