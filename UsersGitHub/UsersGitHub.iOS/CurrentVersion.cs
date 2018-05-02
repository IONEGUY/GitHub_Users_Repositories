using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UsersGitHub.Interfaces;

namespace UsersGitHub.iOS
{
    public class CurrentVersionService : ICurrentVersionService
    {
        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
    }
}