using System.Reactive.Linq;
using System.Windows.Input;
using Akavache;
using Prism.Navigation;
using UsersGitHub.Model;
using UsersGitHub.Services;
using UsersGitHub.Views;
using Xamarin.Forms;

namespace UsersGitHub.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        public ICommand GoToUserReposPageCommand { get; set; }
        private string userLogin;

        public string UserLogin
        {
            get => userLogin;
            set => SetProperty(ref userLogin, value);
        }

        public LoginPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
             GoToUserReposPageCommand = new Command(GoToUserReposPage);
        }

        private async void GoToUserReposPage()
        {
            var userService = new UserService();
            var name = await userService.GetUserInfo(UserLogin);
            if (name == null)
            {
                return;
            }
            var repositories = await userService.GetUserRepositories(UserLogin);
            var user = new User
            {
                UserName = name,
                Repositories = repositories
            };
            await BlobCache.UserAccount.InsertObject(name, user);
            Application.Current.MainPage = new UsersReposPage();
        }
    }
}
