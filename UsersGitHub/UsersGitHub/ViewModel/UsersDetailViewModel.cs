using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using Akavache;
using UsersGitHub.Model;
using Xamarin.Forms;

namespace UsersGitHub.ViewModel
{
    public class UsersDetailViewModel : BindableObject
    {
        private ObservableCollection<User> users;
        private string userName;
        public ICommand AddUserCommand { get; set; }
        public ICommand MoreCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public UsersDetailViewModel()
        {
            AddUserCommand = new Command(AddUser);
            DeleteCommand = new Command(RemoveUser);
            GetUserListFromStorage();
        }

        private async void RemoveUser(object userObject)
        {
            var user = (User)userObject;
            await BlobCache.UserAccount.Invalidate(user.UserName);
            Users.Remove(user);
        }

        private void AddUser()
        {
            BlobCache.UserAccount.InsertObject(UserName, new User
            {
                UserName = UserName
            });
            Users.Add(new User
            {
                UserName = UserName
            });
        }

        private async void GetUserListFromStorage()
        {
            var deserializedUsers = await BlobCache.UserAccount.GetAllObjects<User>();
            Users = new ObservableCollection<User>(deserializedUsers.Reverse());
        }

        public string UserName
        {
            get => userName;
            set
            {
                if (userName == value)
                {
                    return;
                }

                userName = value;
                OnPropertyChanged();
            }
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
