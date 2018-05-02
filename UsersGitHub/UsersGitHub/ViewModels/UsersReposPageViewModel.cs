using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Plugin.Connectivity;
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
        private string firstNameCheckLabel;
        private string lastNameCheckLabel;
        private bool isPresented;
        private string version;
        private readonly ICurrentUserService currentUserService;
        private readonly IPageDialogService pageDialogService;
        private readonly INavigationService navigationService;

        public ObservableCollection<UsersReposPageMenuItem> MenuItems { get; set; }
        public ICommand ShowDetailCommand { get; }

        public bool IsPresented
        {
            get => isPresented;
            set => SetProperty(ref isPresented, value);
        }

        public string FirstNameCheckLabel
        {
            get => firstNameCheckLabel;
            set => SetProperty(ref firstNameCheckLabel, value);
        }

        public string LastNameCheckLabel
        {
            get => lastNameCheckLabel;
            set => SetProperty(ref lastNameCheckLabel, value);
        }

        public string CurrentVersion
        {
            get => version;
            set => SetProperty(ref version, value);
        }

        public UsersReposPageViewModel(INavigationService navigationService,
               ICurrentUserService currentUserService,
               IPageDialogService pageDialogService,
               IAppVersion appVersion,
               IInternetConnectionService internetConnectionService)
            : base(navigationService)
        {
            this.navigationService = navigationService;
            this.currentUserService = currentUserService;
            this.pageDialogService = pageDialogService;
            internetConnectionService.Init();
            CurrentVersion = "v. " + appVersion.GetVersion();
            ShowDetailCommand = new Command(ShowDetail);
            MenuItems = new ObservableCollection<UsersReposPageMenuItem>(new[]
            {
                new UsersReposPageMenuItem { Id = 0, Title = "Users" },
                new UsersReposPageMenuItem { Id = 1, Title = "Settings" }
            });
        }

        private async void ShowDetail(object detailPage)
        {
            var detailPageName = ((UsersReposPageMenuItem) detailPage).Title;
            var navigationString = string.Empty;//$"{nameof(UsersReposPage)}/{nameof(NavigationPage)}/";

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
