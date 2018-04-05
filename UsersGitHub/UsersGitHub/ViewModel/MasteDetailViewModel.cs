using System;
using System.Reflection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using UsersGitHub.View;
using Xamarin.Forms;
using MasterDetailPage = UsersGitHub.View.MasterDetailPage;

namespace UsersGitHub.ViewModel
{
    public class MasteDetailViewModel : INotifyPropertyChanged
    {
        public ICommand ShowDetailCommand { get; }

        public ObservableCollection<MasterDetailPageMenuItem> MenuItems { get; set; }

        public MasteDetailViewModel()
        {           
            ShowDetailCommand = new Command(parameter => ShowDetail(parameter.ToString()));

            MenuItems = new ObservableCollection<MasterDetailPageMenuItem>(new[]
            {
                new MasterDetailPageMenuItem { Id = 0, Title = "Users" },
                new MasterDetailPageMenuItem { Id = 1, Title = "Repos" },
            });
        }

        private void ShowDetail(string detailPageName)
        {
            if (!(Application.Current.MainPage is MasterDetailPage page)) return;     
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
