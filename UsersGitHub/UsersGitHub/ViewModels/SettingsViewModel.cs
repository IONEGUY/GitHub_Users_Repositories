using System.Reactive.Linq;
using System.Windows.Input;
using Akavache;
using Prism.Events;
using Prism.Navigation;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;
using Xamarin.Forms;

namespace UsersGitHub.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private User currentUser;
        private readonly ICurrentUserService currentUserService;

        public ICommand SaveCommand { get; set; }

        public User CurrentUser
        {
            get => currentUser;
            set => SetProperty(ref currentUser, value);
        }

        public SettingsViewModel(INavigationService navigationService,
            ICurrentUserService currentUserService)
            : base(navigationService)
        {
            this.currentUserService = currentUserService;
            SetCurrentUser();
            SaveCommand = new Command(Save);
        }

        private async void Save()
        {
            var login = currentUserService.User.Login;
            await BlobCache.UserAccount.Invalidate(login);
            currentUserService.User.UserName = 
                $"{CurrentUser.FirstName} {CurrentUser.LastName}";
            await BlobCache.UserAccount.InsertObject(login, currentUserService.User);
        }

        private void SetCurrentUser()
        {
            CurrentUser = new User
            {
                FirstName = currentUserService.User.UserName.Split(' ')[1],
                LastName = currentUserService.User.UserName.Split(' ')[0]
            };
        }
    }
}
