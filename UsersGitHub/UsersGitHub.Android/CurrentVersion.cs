using Android.Content.PM;
using UsersGitHub.Interfaces;

namespace UsersGitHub.Droid
{
    public class CurrentVersionService : ICurrentVersionService
    {
        public string GetVersion()
        {
            var context = global::Android.App.Application.Context;
            var manager = context.PackageManager;
            var info = manager.GetPackageInfo(context.PackageName, 0);
            return info.VersionName;
        }
    }
}