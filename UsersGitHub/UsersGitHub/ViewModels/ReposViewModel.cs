using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Acr.UserDialogs;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Prism.Navigation;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;

namespace UsersGitHub.ViewModels
{
    public class ReposViewModel : DisplayLoadingViewModel, INavigatedAware
    {
        private ObservableCollection<Repository> repositories;
        private readonly IUserService userService;
        private readonly INavigationService navigationService;

        public ObservableCollection<Repository> Repositories
        {
            get => repositories;
            set => SetProperty(ref repositories, value);
        }

        public ReposViewModel(
               INavigationService navigationService,
               IUserService userService,
               IInternetConnectionService internetConnectionService)
            : base(internetConnectionService)
        {
            internetConnectionService.Init();
            this.navigationService = navigationService;
            this.userService = userService;
        }

        public async void OnNavigatedFrom(NavigationParameters parameters)
        {
            await navigationService.GoBackAsync();
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            var userLogin = (string)parameters["Login"];
            var loading = UserDialogs.Instance.Loading("");
            loading.Show();
            Repositories = await userService.GetUserRepositories(userLogin);
            if (Repositories == null)
            {
                UserDialogs.Instance.Alert("Something went wrong!!!");
            }
            loading.Hide();
        }
    }
}
