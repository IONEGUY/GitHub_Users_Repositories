using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using UsersGitHub.Annotations;
using UsersGitHub.View;
using Xamarin.Forms;
using Akavache;
using Refit;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;
using UsersGitHub.Services;

namespace UsersGitHub.ViewModel
{
    public class LoginPageViewModel : BindableObject
    {
        public INavigation Navigation { get; set; }
        public ICommand GoToUserReposPageCommand { get; set; }
        private string userLogin;
        public Action errorMessage;

        public string UserLogin
        {
            get => userLogin;
            set
            {
                if (userLogin == value)
                {
                    return;
                }
                userLogin = value;
                OnPropertyChanged();
            }
        }

        public LoginPageViewModel(Action errorMessage)
        {
            this.errorMessage = errorMessage;
             GoToUserReposPageCommand = new Command(GoToUserReposPage);
        }

        private async void GoToUserReposPage()
        {
            var userService = new UserService();
            var name = await userService.GetUserInfo(UserLogin);
            if (name == null)
            {
                errorMessage();
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
