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
using UsersGitHub.ViewModel;

namespace UsersGitHub.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersReposPageMaster : ContentPage
    {
        public UsersReposPageMaster()
        {
            InitializeComponent();
            //BindingContext = new UsersReposViewModel();
        }
    }
}