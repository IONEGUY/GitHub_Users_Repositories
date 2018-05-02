using System;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Akavache;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Events;
using Prism.Navigation;
using Prism.Services;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;
using Xamarin.Forms;

namespace UsersGitHub.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly INavigationService navigationService;
        private readonly ICurrentUserService currentUserService;
        private readonly IPageDialogService dialogService;
        private readonly ICameraService cameraService;
        private readonly UserImage userImage;
        private User currentUser;
        private string imageSource;
        private TimeSpan? time;
        private DateTime date;
        private const string postfix = "_img";

        public ICommand SaveCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand GetPhotoCommand { get; set; }
        public ICommand SetTimeCommand { get; set; }
        public ICommand CheckingCommand { get; set; }

        public User CurrentUser
        {
            get => currentUser;
            set => SetProperty(ref currentUser, value);
        }

        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }

        public TimeSpan? Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }

        public string Source
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public SettingsViewModel(INavigationService navigationService,
            ICurrentUserService currentUserService,
            IPageDialogService dialogService,
            ICameraService cameraService)
            : base(navigationService)
        {
            this.navigationService = navigationService;
            this.cameraService = cameraService;
            this.dialogService = dialogService;
            this.currentUserService = currentUserService;
            userImage = new UserImage();
            SetCurrentUser();
            SaveCommand = new Command(Save);
            GetPhotoCommand = new Command(GetPhoto);
            TakePhotoCommand = new DelegateCommand(TakePhoto);
        }

        private async void TakePhoto()
        {
            var file = await cameraService.TakePhoto();
            if (file == null)
                return;
            Source = file.Path;
        }

        private async void GetPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return;
            }
            var photo = await cameraService.GetPhoto();
            Source = photo.Path;
        }

        private async void Save()
        {
            var login = currentUserService.User.Login;
            currentUserService.User.UserName = 
                $"{CurrentUser.FirstName} {CurrentUser.LastName}";
            var dateTime = new DateTime
            (
                Date.Year,
                Date.Month,
                Date.Day,
                Time.Value.Hours,
                Time.Value.Minutes,
                0
            );
            userImage.Offset = dateTime.Date - DateTime.Now;
            userImage.Path = Source;
            currentUserService.User.Image = userImage;
            await BlobCache.UserAccount.Invalidate(login);
            await BlobCache.UserAccount.InsertObject(login, currentUserService.User);
            await BlobCache.UserAccount.InsertObject(login + postfix, userImage);
            await dialogService.DisplayAlertAsync("","Saved","OK");
        }

        private async void SetCurrentUser()
        {
            CurrentUser = new User
            {
                FirstName = currentUserService.User.UserName.Split(' ')[0],
                LastName = currentUserService.User.UserName.Split(' ')[1],
            };
            Source = await GetUserImageAsync();
        }

        private async Task<string> GetUserImageAsync()
        {
            Date = DateTime.Now;
            Date = Date.AddDays(2);
            Time = Date.TimeOfDay;
            var user = currentUserService.User;
            if (user.Image == null)
            {
                return string.Empty;
            }
            var image = await
                BlobCache.UserAccount.GetAndFetchLatest(user.Login + postfix,
                    () => Task.FromResult<UserImage>(null),
                    createdAt => DateTime.Now - createdAt > user.Image.Offset);
            return image.Path;
        }
    }
}
