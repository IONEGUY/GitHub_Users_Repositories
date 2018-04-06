using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
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
using UsersGitHub.Model;

namespace UsersGitHub.ViewModel
{
    public class LoginPageViewModel : BindableObject
    {
        public INavigation Navigation { get; set; }
        public ICommand GoToUserReposPageCommand { get; set; }
        private string _userLogin;
        private bool _isDbEmpty;

        public string UserLogin
        {
            get => _userLogin;
            set
            {
                if (_userLogin == value)
                {
                    return;
                }

                _userLogin = value;
                OnPropertyChanged();
            }
        }

        public LoginPageViewModel()
        {
             GoToUserReposPageCommand = new Command(GoToUserReposPage);
        }

        private async void GoToUserReposPage()
        {          
            BlobCache.ApplicationName = "UsersGitHub";
            var user = new User { Login = UserLogin };
            await BlobCache.UserAccount.InsertObject(UserLogin, user);
            Application.Current.MainPage = new UsersReposPage();
        }
    }
}
