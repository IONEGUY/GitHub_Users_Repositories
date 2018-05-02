using Android.Content.PM;
using UsersGitHub.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(UsersGitHub.Droid.CurrentVersion))]
namespace UsersGitHub.Droid
{
    public class CurrentVersion : IAppVersion
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