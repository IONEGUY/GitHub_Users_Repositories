using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using Akavache;
using Prism.Navigation;
using UsersGitHub.Model;
using UsersGitHub.Services;
using UsersGitHub.Views;
using Xamarin.Forms;

namespace UsersGitHub.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private ObservableCollection<User> users;
        private string userName;

        public ICommand AddUserCommand { get; set; }
        public ICommand MoreCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public UsersViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            AddUserCommand = new Command(AddUser);
            DeleteCommand = new Command(RemoveUser);
            MoreCommand = new Command(GetUserRepositories);
            GetUserListFromStorage();
        }

        private void GetUserRepositories(object userObject)
        {
            var user = (User) userObject;
            var navigationParams = new NavigationParameters
            {
                {nameof(user), user}
            };
            NavigationService.NavigateAsync(nameof(Repos), navigationParams);
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
            Users = new ObservableCollection<User>(deserializedUsers);
        }

        public string UserName
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }

        public ObservableCollection<User> Users
        {
            get => users;
            set => SetProperty(ref users, value);
        }
    }
}
