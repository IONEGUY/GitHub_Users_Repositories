using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using Akavache;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;
using UsersGitHub.Services;
using UsersGitHub.Views;
using Xamarin.Forms;

namespace UsersGitHub.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private ObservableCollection<User> users;
        private string userLogin = String.Empty;
        private ICurrentUserService currentUserService;
        private readonly IPageDialogService dialogService;

        public ICommand AddUserCommand { get; set; }
        public ICommand MoreCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SetCurrentUserCommand { get; set; }

        public UsersViewModel(INavigationService navigationService, 
            IPageDialogService dialogService,
            ICurrentUserService currentUserService)
            : base(navigationService)
        {
            this.currentUserService = currentUserService;
            this.dialogService = dialogService;
            AddUserCommand = new Command(AddUser);
            DeleteCommand = new Command(RemoveUser);
            MoreCommand = new Command(GoToUserRepositories);
            SetCurrentUserCommand = new Command(SetCurrentUser);
            GetUserListFromStorage();
        }

        private void SetCurrentUser(object selectedUser)
        {
            currentUserService.User = (User)selectedUser;
        }

        private void GoToUserRepositories(object userObject)
        {
            var user = (User) userObject;
            var navigationParams = new NavigationParameters
            {
                {"Login", user.Login}
            };
            NavigationService.NavigateAsync(nameof(Repos), navigationParams);
        }

        private async void RemoveUser(object userObject)
        {
            var user = (User)userObject;
            await BlobCache.UserAccount.Invalidate(user.Login);
            Users.Remove(user);
        }

        private async void AddUser()
        {
            var userService = new UserService();
            var name = await userService.GetUserInfo(UserLogin);
            if (name == null)
            {
                await dialogService.DisplayAlertAsync("Error", @"This name doesn't exist", "OK");
                return;
            }
            var user = new User
            {
                UserName = name,
                Login = UserLogin
            };
            await BlobCache.UserAccount.InsertObject(UserLogin, user);
            Users.Add(user);
        }

        private async void GetUserListFromStorage()
        {
            var deserializedUsers = await BlobCache.UserAccount.GetAllObjects<User>();
            Users = new ObservableCollection<User>(deserializedUsers);
        }

        public string UserLogin
        {
            get => userLogin;
            set => SetProperty(ref userLogin, value);
        }

        public ObservableCollection<User> Users
        {
            get => users;
            set => SetProperty(ref users, value);
        }
    }
}
