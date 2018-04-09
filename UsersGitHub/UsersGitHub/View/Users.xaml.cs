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
    public partial class Users
    {
        public Users()
        {
            InitializeComponent();
            BindingContext = new UsersDetailViewModel(Navigation, () => DisplayAlert("Error", "This name doesn't exist", "OK"));
        }
    }
}