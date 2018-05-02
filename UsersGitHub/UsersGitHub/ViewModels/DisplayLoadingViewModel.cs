using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Navigation;
using UsersGitHub.Interfaces;

namespace UsersGitHub.ViewModels
{
    public class DisplayLoadingViewModel : BaseViewModel, IDestructible
    {
        protected readonly IInternetConnectionService internetConnectionService;

        public DisplayLoadingViewModel(IInternetConnectionService internetConnectionService)
        {
            this.internetConnectionService = internetConnectionService;
            internetConnectionService.Init();
        }

        public void Destroy()
        {
            internetConnectionService.Destroy();
        }
    }
}
