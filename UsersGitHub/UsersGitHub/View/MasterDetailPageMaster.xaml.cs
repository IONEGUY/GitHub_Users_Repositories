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
    public partial class MasterDetailPageMaster
    {
        public ListView ListView;

        public MasterDetailPageMaster()
        {
            InitializeComponent();

            BindingContext = new MasterDetailPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterDetailPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPageMenuItem> MenuItems { get; set; }
            
            public MasterDetailPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterDetailPageMenuItem>(new[]
                {
                    new MasterDetailPageMenuItem { Id = 0, Title = "Users" },
                    new MasterDetailPageMenuItem { Id = 1, Title = "Repos" },
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