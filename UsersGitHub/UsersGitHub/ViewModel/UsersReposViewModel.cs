using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using UsersGitHub.View;
using Xamarin.Forms;

namespace UsersGitHub.ViewModel
{
    public class UsersReposViewModel : INotifyPropertyChanged
    {
        public ICommand ShowDetailCommand { get; }

        public ObservableCollection<UsersReposPageMenuItem> MenuItems { get; set; }

        public UsersReposViewModel()
        {           
            ShowDetailCommand = new Command(parameter => ShowDetail(parameter.ToString()));

            MenuItems = new ObservableCollection<UsersReposPageMenuItem>(new[]
            {
                new UsersReposPageMenuItem { Id = 0, Title = "Users" },
                new UsersReposPageMenuItem { Id = 1, Title = "Repos" },
            });
        }

        private void ShowDetail(string detailPageName)
        {
            if (!(Application.Current.MainPage is Xamarin.Forms.MasterDetailPage page)) return;     
            page.Detail = new NavigationPage(GetDetailPageInstaceByName(detailPageName));
        }

        private Page GetDetailPageInstaceByName(string detailPageName)
        {
            var detailPageType = Type.GetType("UsersGitHub.View." + detailPageName, false, true);
            var detailPageConstructor = detailPageType.GetConstructor(new Type[] { });
            return detailPageConstructor.Invoke(new object[] { }) as Page;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
