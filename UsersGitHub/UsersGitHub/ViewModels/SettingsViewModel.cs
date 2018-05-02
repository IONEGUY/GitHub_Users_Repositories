using System;
using System.IO;
using System.Reactive.Linq;
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
        private readonly IPageDialogService dialogService;
        private User currentUser;
        private string imageSource;
        private readonly ICurrentUserService currentUserService;

        public ICommand SaveCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand GetPhotoCommand { get; set; }

        public User CurrentUser
        {
            get => currentUser;
            set => SetProperty(ref currentUser, value);
        }

        public string Source
        {
            get => imageSource;
            set => SetProperty(ref imageSource, value);
        }

        public SettingsViewModel(INavigationService navigationService,
            ICurrentUserService currentUserService,
            IPageDialogService dialogService)
            : base(navigationService)
        {
            this.dialogService = dialogService;
            this.currentUserService = currentUserService;
            SetCurrentUser();
            SaveCommand = new Command(Save);
            GetPhotoCommand = new Command(GetPhoto);
            TakePhotoCommand = new DelegateCommand(TakePhoto);
        }

        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "Sample",
                Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
            });

            if (file == null)
                return;

            Source = file.Path;
        }

        private async void GetPhoto()
        {
            if (CrossMedia.Current.IsPickPhotoSupported)
            {
                MediaFile photo = await CrossMedia.Current.PickPhotoAsync();
                Source = photo.Path;
            }
        }

        private async void Save()
        {
            var login = currentUserService.User.Login;
            await BlobCache.UserAccount.Invalidate(login);
            currentUserService.User.UserName = 
                $"{CurrentUser.FirstName} {CurrentUser.LastName}";
            currentUserService.User.ImagePath = Source;
            await BlobCache.UserAccount.InsertObject(login, currentUserService.User);
        }

        private void SetCurrentUser()
        {
            CurrentUser = new User
            {
                FirstName = currentUserService.User.UserName.Split(' ')[1],
                LastName = currentUserService.User.UserName.Split(' ')[0],
            };
            Source = currentUserService.User.ImagePath;
        }
    }
}
