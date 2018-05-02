using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;

namespace UsersGitHub.Interfaces
{
    public interface ICameraService
    {
        Task<MediaFile> TakePhoto();
        Task<MediaFile> GetPhoto();
    }
}
