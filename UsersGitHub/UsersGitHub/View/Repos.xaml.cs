using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersGitHub.Model;
using UsersGitHub.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsersGitHub.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Repos
    {
        public Repos(User user)
        {
            InitializeComponent();
            BindingContext = new ReposViewModel(user);
        }
    }
}