using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Acr.UserDialogs;
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
    public class UsersViewModel : DisplayLoadingViewModel
    {
        private ObservableCollection<User> users;
        private string userLogin = string.Empty;
        private readonly ICurrentUserService currentUserService;
        private readonly IPageDialogService dialogService;
        private readonly IUserService userService;
        private readonly INavigationService navigationService;

        public ICommand AddUserCommand { get; set; }
        public ICommand MoreCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SetCurrentUserCommand { get; set; }


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

        public UsersViewModel(
            IInternetConnectionService internetConnectionService,
            INavigationService navigationService, 
            IPageDialogService dialogService,
            ICurrentUserService currentUserService,
            IUserService userService)
            : base(internetConnectionService)
        {
            this.navigationService = navigationService;
            this.userService = userService;
            this.currentUserService = currentUserService;
            this.dialogService = dialogService;
            AddUserCommand = new Command(AddUser);
            DeleteCommand = new Command(RemoveUser);
            MoreCommand = new Command(GoToUserRepositories);
            SetCurrentUserCommand = new Command(SetCurrentUser);
            GetUserListFromStorage();
            SetDefaultCurrentUser();
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
            navigationService.NavigateAsync(nameof(Repos), navigationParams);
        }

        private async void RemoveUser(object userObject)
        {
            var user = (User)userObject;
            await BlobCache.UserAccount.Invalidate(user.Login);
            Users.Remove(user);
        }

        private async void AddUser()
        {
            var loading = UserDialogs.Instance.Loading("");
            loading.Show();
            var name = await userService.GetUserInfo(UserLogin);
            loading.Hide();
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

        private void SetDefaultCurrentUser()
        {
            if (currentUserService.User == null)
            {
                currentUserService.User = Users.First();
            }
        }
    }
}
