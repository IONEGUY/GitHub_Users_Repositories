using UsersGitHub.Interfaces;
using UsersGitHub.Model;

namespace UsersGitHub.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public User User { get; set; }
    }
}
