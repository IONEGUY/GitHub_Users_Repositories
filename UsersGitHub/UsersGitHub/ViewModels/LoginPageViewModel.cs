using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
using Akavache;
using Plugin.Connectivity;
using Prism.Navigation;
using Prism.Services;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;
using UsersGitHub.Services;
using UsersGitHub.Views;
using Xamarin.Forms;

namespace UsersGitHub.ViewModels
{
    public class LoginPageViewModel : DisplayLoadingViewModel
    {
        private string userLogin;
        private readonly IUserService userService;
        private readonly IPageDialogService dialogService;

        public ICommand GoToUserReposPageCommand { get; set; }

        public string UserLogin
        {
            get => userLogin;
            set => SetProperty(ref userLogin, value);
        }

        public LoginPageViewModel(
            IInternetConnectionService internetConnectionService,
            IPageDialogService dialogService,
            IUserService userService) 
            : base(internetConnectionService)
        {
            this.userService = userService;
            this.dialogService = dialogService;
            UserLogin = string.Empty;
            GoToUserReposPageCommand = new Command(GoToUserReposPage);
        }

        private async void GoToUserReposPage()
        {
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
