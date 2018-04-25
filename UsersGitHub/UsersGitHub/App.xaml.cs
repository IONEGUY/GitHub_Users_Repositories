using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
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

        protected override void OnInitialized()
        {
            InitializeComponent();
            BlobCache.ApplicationName = "UsersGitHub";
            var userCollection = BlobCache.UserAccount.GetAllObjects<User>().Wait();
            if (userCollection.Any())
            {
                NavigationService.NavigateAsync(nameof(UsersReposPage)).Wait();
            }
            else
            {
                NavigationService.NavigateAsync(nameof(LoginPage)).Wait();
            }

        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Repos>();
            containerRegistry.RegisterForNavigation<Settings>();
            containerRegistry.RegisterForNavigation<Users>();
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<UsersReposPage>();
        }
    }
}
