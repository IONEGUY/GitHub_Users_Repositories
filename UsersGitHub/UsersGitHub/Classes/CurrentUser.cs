using System;
using System.Collections.Generic;
using System.Text;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;

namespace UsersGitHub.Classes
{
    public class CurrentUserService : ICurrentUserService
    {
        public User User { get; set; }
    }
}
