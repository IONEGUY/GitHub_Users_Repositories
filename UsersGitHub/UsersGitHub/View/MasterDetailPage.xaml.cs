using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersGitHub.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsersGitHub.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailPage
    {
        public MasterDetailPage()
        {
            InitializeComponent();
            BindingContext = new ViewModel.MasteDetailViewModel(Detail);
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            
            if (!(e.SelectedItem is MasterDetailPageMenuItem item))
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;

            Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
    }
}