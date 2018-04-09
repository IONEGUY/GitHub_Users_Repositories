using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Akavache;
using UsersGitHub.Model;
using UsersGitHub.Services;
using UsersGitHub.View;
using Xamarin.Forms;

namespace UsersGitHub.ViewModel
{
    public class UsersDetailViewModel : BindableObject
    {
        private ObservableCollection<User> users;
        private string userName;
        private readonly Action errorMessage;
        public ICommand AddUserCommand { get; set; }
        public ICommand MoreCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        private readonly INavigation navigation;

        public UsersDetailViewModel(INavigation navigation, Action errorMessage)
        {
            this.errorMessage = errorMessage;
            this.navigation = navigation;
            AddUserCommand = new Command(AddUser);
            DeleteCommand = new Command(RemoveUser);
            MoreCommand = new Command(GetUserRepositories);
            GetUserListFromStorage();
        }

        private void GetUserRepositories(object userObject)
        {
            var user = (User) userObject;
            navigation.PushAsync(new Repos(user));
        }

        private async void RemoveUser(object userObject)
        {
            var user = (User)userObject;
            await BlobCache.UserAccount.Invalidate(user.UserName);
            Users.Remove(user);
        }

        private async void AddUser()
        {
            var userService = new UserService();
            var name = await userService.GetUserInfo(UserName);
            if (name == null)
            {
                errorMessage();
                return;
            }
            var user = new User
            {
                UserName = name,
                Repositories = await userService.GetUserRepositories(UserName)
            };
            await BlobCache.UserAccount.InsertObject(user.UserName, user);
            Users.Add(user);
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
