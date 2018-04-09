using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using Akavache;
using UsersGitHub.Model;
using Xamarin.Forms;
using Refit;

namespace UsersGitHub.ViewModel
{
    public class ReposViewModel : BindableObject
    {
        private ObservableCollection<Repository> repositories;

        public ObservableCollection<Repository> Repositories
        {
            get => repositories;
            set
            {
                if (repositories == value)
                {
                    return;
                }
                repositories = value;
                OnPropertyChanged();
            }
        }

        public ReposViewModel()
        {
            GetUrerRepositories();
        }

        private async void GetUrerRepositories()
        {
            Repositories = (await BlobCache.UserAccount.GetAllObjects<User>()).Last().Repositories;
        }
    }
}
