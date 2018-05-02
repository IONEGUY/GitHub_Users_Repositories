using System;
using System.Collections.Generic;
using System.Text;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;

namespace UsersGitHub.Classes
{
    public class CurrentUserervice : ICurrentUserService
    {
        public User User { get; set; }
    }
}
