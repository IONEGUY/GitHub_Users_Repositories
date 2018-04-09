using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using Akavache;
using UsersGitHub.Model;
using Xamarin.Forms;

namespace UsersGitHub.ViewModel
{
    public class UsersDetailViewModel : BindableObject
    {
        private ObservableCollection<User> users;

        public UsersDetailViewModel()
        {
            GetUserListFromStorage();
        }

        private async void GetUserListFromStorage()
        {
            var deserializedUsers = await BlobCache.UserAccount.GetAllObjects<User>();
            Users = new ObservableCollection<User>(deserializedUsers.Reverse());
        }

        public ObservableCollection<User> Users
        {
            get => users;
            set
            {
                if (users == value)
                {
                    return;
                }
                users = value;
                OnPropertyChanged();
            }
        }
    }
}
