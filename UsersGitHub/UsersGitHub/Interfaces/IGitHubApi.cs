using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using UsersGitHub.Model.DtoModel;

namespace UsersGitHub.Interfaces
{
    public interface IGitHubApi
    {
        [Get("/users/{user}")]
        Task<UserDto> GetUser(string user);

        [Get("/users/{user}/repos")]
        Task<IList<RepositoryDto>> GetRepositories(string user);
    }
}
