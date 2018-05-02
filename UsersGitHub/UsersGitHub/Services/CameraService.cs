using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using UsersGitHub.Interfaces;

namespace UsersGitHub.Services
{
    public class CameraService : ICameraService
    {
        public CameraService()
        {
            CrossMedia.Current.Initialize();
        }

        public async Task<MediaFile> TakePhoto()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return await Task.FromResult<MediaFile>(null);
            }

            return await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Directory = "Sample",
                Name = $"{DateTime.Now:dd.MM.yyyy_hh.mm.ss}.jpg"
            });
        }

        public async Task<MediaFile> GetPhoto()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return await Task.FromResult<MediaFile>(null);
            }
            return await CrossMedia.Current.PickPhotoAsync();
        }
    }
}


