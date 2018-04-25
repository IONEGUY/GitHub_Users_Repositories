using System.Collections.ObjectModel;
using Prism.Navigation;
using UsersGitHub.Model;

namespace UsersGitHub.ViewModels
{
    public class ReposViewModel : BaseViewModel, INavigatedAware
    {
        private ObservableCollection<Repository> repositories;

        public ObservableCollection<Repository> Repositories
        {
            get => repositories;
            set => SetProperty(ref repositories, value);
        }

        public ReposViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public async void OnNavigatedFrom(NavigationParameters parameters)
        {
            await NavigationService.GoBackAsync();
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            var userLogin = (string)parameters["Login"];
            Repositories = await new Services.UserService().GetUserRepositories(userLogin);
        }
    }
}
