using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UsersGitHub.Interfaces;

[assembly: Xamarin.Forms.Dependency(typeof(UsersGitHub.iOS.CurrentVersion))]
namespace UsersGitHub.iOS
{
    public class CurrentVersion : IAppVersion
    {
        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
    }
}