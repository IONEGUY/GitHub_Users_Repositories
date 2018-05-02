using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using Akavache;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;
using UsersGitHub.Model;
using UsersGitHub.Services;
using UsersGitHub.Views;
using Xamarin.Forms;

namespace UsersGitHub.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        private string userLogin;
        private readonly IPageDialogService dialogService;

        public ICommand GoToUserReposPageCommand { get; set; }

        public string UserLogin
        {
            get => userLogin;
            set => SetProperty(ref userLogin, value);
        }

        public LoginPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            this.dialogService = dialogService;
            CheckInternetConnection();
            UserLogin = String.Empty;
            GoToUserReposPageCommand = new Command(GoToUserReposPage);
        }


        private void CheckInternetConnection()
        {
            var isConnected = CrossConnectivity.Current.IsConnected;
            if (!isConnected)
            {
                UserDialogs.Instance.Loading("Waiting for internet connection!!!");
            }
        }

        private async void GoToUserReposPage()
        {
            var userService = new UserService();
            var name = await userService.GetUserInfo(UserLogin);
            if (name == null)
            {
                await dialogService.DisplayAlertAsync("Error", @"This name doesn't exist", "OK");
                return;
            }
            var user = new User
            {
                UserName = name,
                Login = userLogin
            };
            await BlobCache.UserAccount.InsertObject(UserLogin, user);
            Application.Current.MainPage = new UsersReposPage();
        }
    }
}
