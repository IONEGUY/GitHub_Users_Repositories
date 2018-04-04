using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using UsersGitHub.Classes;
using UsersGitHub.View;
using Xamarin.Forms;

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

        private void ShowDetail(string pageName)
        {
            var page = Application.Current.MainPage as View.MasterDetailPage;
            if (page == null)
            {
                return;
            }

            switch (pageName)
            {
                case "Users": page.Detail = new NavigationPage(new Users()); break;
                case "Repos": page.Detail = new NavigationPage(new Repos()); break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
