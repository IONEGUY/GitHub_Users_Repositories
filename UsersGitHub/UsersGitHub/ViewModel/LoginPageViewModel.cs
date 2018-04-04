﻿using System;
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
    public class LoginPageViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }
        public ICommand GoTouserReposPageCommand { get; set; }
        private Page LoginPage { get; }

        public LoginPageViewModel(Page page)
        {
            GoTouserReposPageCommand = new Command(GoToUserReposPage);
            LoginPage = page;
        }

        private void GoToUserReposPage()
        {
            Application.Current.MainPage = new View.MasterDetailPage();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
