using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using UsersGitHub.Annotations;
using UsersGitHub.View;
using Xamarin.Forms;

namespace UsersGitHub.ViewModel
{
    public class LoginPageViewModel : BindableObject
    {
        private readonly Page _page;
        public INavigation Navigation { get; set; }
        public ICommand GoToUserReposPageCommand { get; set; }

        public LoginPageViewModel(Page page)
        {
            _page = page;
            GoToUserReposPageCommand = new Command(GoToUserReposPage);
        }

        private void GoToUserReposPage()
        {
            Application.Current.MainPage = new UsersReposPage();
        }
    }
}
