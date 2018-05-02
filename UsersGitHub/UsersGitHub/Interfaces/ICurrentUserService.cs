using System;
using System.Collections.Generic;
using System.Text;
using UsersGitHub.Model;

namespace UsersGitHub.Interfaces
{
    public interface ICurrentUserService
    {
        User User { get; set; }
    }
}
