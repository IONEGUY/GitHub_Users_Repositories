using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Navigation;
using Prism.Services;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;
using UsersGitHub.Views;
using Xamarin.Forms;

namespace UsersGitHub.ViewModels
{
    public class UsersReposPageViewModel : BaseViewModel
    {
        private bool isPresented;
        private readonly INavigationService navigationService;

        public ObservableCollection<UsersReposPageMenuItem> MenuItems { get; set; }
        public ICommand ShowDetailCommand { get; }

        public UsersReposPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            this.navigationService = navigationService;
            ShowDetailCommand = new Command(ShowDetail);
            MenuItems = new ObservableCollection<UsersReposPageMenuItem>(new[]
            {
                new UsersReposPageMenuItem { Id = 0, Title = "Users" },
                new UsersReposPageMenuItem { Id = 1, Title = "Settings" },
            });
        }

        public bool IsPresented
        {
            get => isPresented;
            set => SetProperty(ref isPresented, value);
        }

        private async void ShowDetail(object detailPage)
        {
            var detailPageName = ((UsersReposPageMenuItem)detailPage).Title;
            var navigationString = $"{nameof(NavigationPage)}/";
            switch (detailPageName)
            {
                case nameof(Users):
                    navigationString += nameof(Users);
                    break;
                case nameof(Settings):
                    navigationString += nameof(Settings);
                    break;
            }
            await navigationService.NavigateAsync(navigationString);
            IsPresented = false;
        }
    }
}
