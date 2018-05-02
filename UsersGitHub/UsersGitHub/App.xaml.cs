using System.Linq;
using System.Reactive.Linq;
using Akavache;
using AutoMapper;
using Prism;
using Prism.Ioc;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;
using UsersGitHub.Model.DtoModel;
using UsersGitHub.Services;
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
            Mapper.Initialize(cfg => cfg.CreateMap<RepositoryDto, Repository>());
            var userCollection = BlobCache.UserAccount.GetAllObjects<User>().Wait();
            if (userCollection.Any())
            {
                NavigationService.NavigateAsync($"{nameof(UsersReposPage)}/{nameof(NavigationPage)}/{nameof(Users)}").Wait();
            }
            else
            {
                NavigationService.NavigateAsync(nameof(LoginPage)).Wait();
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ICameraService, CameraService>();
            containerRegistry.Register<IInternetConnectionService, InternetConnectionService>();
            containerRegistry.Register<IUserService, UserService>();
            containerRegistry.RegisterSingleton<ICurrentUserService, CurrentUserService>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Repos>();
            containerRegistry.RegisterForNavigation<Settings>();
            containerRegistry.RegisterForNavigation<Users>();
            containerRegistry.RegisterForNavigation<LoginPage>();
            containerRegistry.RegisterForNavigation<UsersReposPage>();
        }
    }
}
