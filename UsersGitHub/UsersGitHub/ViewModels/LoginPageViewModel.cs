using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Akavache;
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
        private string userLogin = String.Empty;
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
            GoToUserReposPageCommand = new Command(GoToUserReposPage);
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
