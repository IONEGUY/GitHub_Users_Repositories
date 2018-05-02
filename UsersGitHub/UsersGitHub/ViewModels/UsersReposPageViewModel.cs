using System;
using System.Collections.ObjectModel;
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
        private bool isPresented;
        private readonly ICurrentUserService currentUserService;
        private readonly IPageDialogService pageDialogService;

        public ObservableCollection<UsersReposPageMenuItem> MenuItems { get; set; }
        public ICommand ShowDetailCommand { get; }

        public bool IsPresented
        {
            get => isPresented;
            set => SetProperty(ref isPresented, value);
        }

        public UsersReposPageViewModel(INavigationService navigationService,
               ICurrentUserService currentUserService,
               IPageDialogService pageDialogService)
            : base(navigationService)
        {
            this.currentUserService = currentUserService;
            this.pageDialogService = pageDialogService;
            CheckInternetConnection();
            ShowDetailCommand = new Command(ShowDetail);
            MenuItems = new ObservableCollection<UsersReposPageMenuItem>(new[]
            {
                new UsersReposPageMenuItem { Id = 0, Title = "Users" },
                new UsersReposPageMenuItem { Id = 1, Title = "Settings" },
            });
        }

        private void CheckInternetConnection()
        {
            var isConnected = CrossConnectivity.Current.IsConnected;
            if (!isConnected)
            {
                UserDialogs.Instance.Loading("Waiting for internet connection!!!");
            }
        }

        private void ShowDetail(object detailPage)
        {
            var detailPageName = ((UsersReposPageMenuItem) detailPage).Title;
            if (!(Application.Current.MainPage is MasterDetailPage page))
            {
                return;
            }
            switch (detailPageName)
            {
                case "Users":
                    page.Detail = new NavigationPage(new Users());
                    break;
                case "Settings":
                    SetSettingsPage(ref page);
                    break;
            }
            IsPresented = false;
        }

        private void SetSettingsPage(ref MasterDetailPage page)
        {
            if (currentUserService.User != null)
            {
                page.Detail = new NavigationPage(new Settings());
                return;
            }
            pageDialogService.DisplayAlertAsync("Error", @"Сurrent user not specified", "OK");
        }
    }
}
