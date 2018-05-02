using System;
using System.Collections.Generic;
using System.Text;
using Acr.UserDialogs;
using Plugin.Connectivity;
using Plugin.Connectivity.Abstractions;
using UsersGitHub.Interfaces;

namespace UsersGitHub.Services
{
    public class InternetConnectionService : IInternetConnectionService
    {
        public void Init()
        {
            CrossConnectivity.Current.ConnectivityChanged += CurrentOnConnectivityChanged;
        }

        public void Destroy()
        {
            CrossConnectivity.Current.ConnectivityChanged -= CurrentOnConnectivityChanged;
        }

        private void CurrentOnConnectivityChanged(object sender, ConnectivityChangedEventArgs connectivityChangedEventArgs)
        {
            if (connectivityChangedEventArgs.IsConnected)
            {
                UserDialogs.Instance.Loading("").Hide();
            }
            else
            {
                UserDialogs.Instance.Loading("").Show();
            }
        }
    }
}
