﻿using System;
using System.IO;
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
        private readonly IPageDialogService dialogService;
        private readonly UserImage userImage;
        private User currentUser;
        private string imageSource;
        private TimeSpan? time;
        private readonly ICurrentUserService currentUserService;

        public ICommand SaveCommand { get; set; }
        public ICommand TakePhotoCommand { get; set; }
        public ICommand GetPhotoCommand { get; set; }
        public ICommand SetTimeCommand { get; set; }

        public User CurrentUser
        {
            get => currentUser;
            set => SetProperty(ref currentUser, value);
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
            var photo = await CrossMedia.Current.PickPhotoAsync();
            Source = photo.Path;
        }

        private async void Save()
        {
            var login = currentUserService.User.Login;
            currentUserService.User.UserName = 
                $"{CurrentUser.FirstName} {CurrentUser.LastName}";
            userImage.Time = Time != new TimeSpan(0,0,0,0) ? Time : null;
            userImage.Path = Source;
            currentUserService.User.Image = userImage;
            await BlobCache.UserAccount.Invalidate(login);
            await BlobCache.UserAccount.InsertObject(login, currentUserService.User);
            await dialogService.DisplayAlertAsync("","Saved","OK");
        }

        private void SetCurrentUser()
        {
            CurrentUser = new User
            {
                FirstName = currentUserService.User.UserName.Split(' ')[0],
                LastName = currentUserService.User.UserName.Split(' ')[1],
            };
            Source = GetUserImage();
        }

        private string GetUserImage()
        {
            var image = currentUserService.User.Image;
            if (image == null)
            {
                Time = new TimeSpan(0, 0, 0, 0);
                return string.Empty;
            }
            if (currentUserService.User.Image.Time == null)
            {
                Time = new TimeSpan(0, 0, 0, 0);
                return image.Path;
            }
            var imageTime = image.Time;
            if (imageTime < DateTime.Now.TimeOfDay)
            {
                Time = new TimeSpan(0, 0, 0, 0);
                userImage.Time = null;
                return string.Empty;
            }
            Time = imageTime;
            return image.Path;
        }
    }
}
