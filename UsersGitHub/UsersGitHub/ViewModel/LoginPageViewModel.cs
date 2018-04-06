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

namespace UsersGitHub.ViewModel
{
    public class LoginPageViewModel : BindableObject
    {
        public INavigation Navigation { get; set; }
        public ICommand GoToUserReposPageCommand { get; set; }
        private string _userLogin;

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
            var repositories = await GetUserRepositories(UserLogin);
            var name = await GetUserInfo(UserLogin);
            var user = new User { UserName = name, Repositories = repositories};
            BlobCache.ApplicationName = "UsersGitHub";
            await BlobCache.UserAccount.InsertObject(UserLogin, user);
            Application.Current.MainPage = new UsersReposPage();
        }

        private async Task<string> GetUserInfo(string userLogin)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.github.com"),
            };
            var gitHubApi = RestService.For<IGitHubApi>(httpClient);

            var user = await gitHubApi.GetUser(userLogin);
            return user.Name;
        }

        private async Task<ObservableCollection<Repository>> GetUserRepositories(string userLogin)
        {
            var httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://api.github.com"),
            };

            var gitHubApi = RestService.For<IGitHubApi>(httpClient);
            var deserializedRepositories = await gitHubApi.GetRepositories(userLogin);
            var repositories = new ObservableCollection<Repository>();
            foreach (var repos in deserializedRepositories)
            {
                repositories.Add(new Repository{ Name = repos.Name});
            }
            return repositories;
        }
    }
}
