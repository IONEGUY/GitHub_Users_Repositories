using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using UsersGitHub.Model;

namespace UsersGitHub.Interfaces
{
    public interface IUserService
    {
        Task<string> GetUserInfo(string userName);
        Task<ObservableCollection<Repository>> GetUserRepositories(string userName);
    }
}
