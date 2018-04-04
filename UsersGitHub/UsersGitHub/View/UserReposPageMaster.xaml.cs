using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsersGitHub.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserReposPageMaster : ContentPage
    {
        public ListView ListView;

        public UserReposPageMaster()
        {
            InitializeComponent();

            BindingContext = new UserReposPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class UserReposPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<UserReposPageMenuItem> MenuItems { get; set; }
            
            public UserReposPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<UserReposPageMenuItem>(new[]
                {
                    new UserReposPageMenuItem { Id = 0, Title = "Page 1" },
                    new UserReposPageMenuItem { Id = 1, Title = "Page 2" },
                    new UserReposPageMenuItem { Id = 2, Title = "Page 3" },
                    new UserReposPageMenuItem { Id = 3, Title = "Page 4" },
                    new UserReposPageMenuItem { Id = 4, Title = "Page 5" },
                });
            }
            
            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}