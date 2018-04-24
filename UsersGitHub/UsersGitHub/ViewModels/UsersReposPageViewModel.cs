using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Prism.Navigation;
using UsersGitHub.Model;
using Xamarin.Forms;

namespace UsersGitHub.ViewModels
{
    public class UsersReposPageViewModel : BaseViewModel
    {
        public ObservableCollection<UsersReposPageMenuItem> MenuItems { get; set; }
        public ICommand ShowDetailCommand { get; }
        private bool isPresented;

        public UsersReposPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {           
            ShowDetailCommand = new Command(parameter => ShowDetail(parameter.ToString()));
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

        private void ShowDetail(string detailPageName)
        {
            if (!(Application.Current.MainPage is MasterDetailPage page))
            {
                return;
            }     
            page.Detail = new NavigationPage(GetDetailPageInstaceByName(detailPageName));
            IsPresented = false;
        }

        private Page GetDetailPageInstaceByName(string detailPageName)
        {
            var detailPageType = Type.GetType("UsersGitHub.Views." + detailPageName, false, true);
            var detailPageConstructor = detailPageType.GetConstructor(new Type[] { });
            return detailPageConstructor.Invoke(new object[] {  }) as Page;
        }
    }
}
