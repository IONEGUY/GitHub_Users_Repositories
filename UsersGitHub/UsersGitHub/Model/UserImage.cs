using System;
using System.Collections.Generic;
using System.Text;

namespace UsersGitHub.Model
{
    public class UserImage
    {
        public bool IsTimeSaving { get; set; } = false;
        public string Path { get; set; }
        public TimeSpan? Time { get; set; }
    }
}
