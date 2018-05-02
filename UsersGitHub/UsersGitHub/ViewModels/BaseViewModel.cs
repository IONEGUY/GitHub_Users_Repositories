using System;
using Acr.UserDialogs;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using Prism.Mvvm;
using Prism.Navigation;

namespace UsersGitHub.ViewModels
{
    public class BaseViewModel : BindableBase
    {
        public BaseViewModel()
        {
            
        }

        public BaseViewModel(INavigationService navigationService)
        {
        }
    }
}
