using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Prism.Logging;
using Refit;
using UsersGitHub.Interfaces;
using UsersGitHub.Model;
using UsersGitHub.Model.DtoModel;
using Xamarin.Forms.Internals;

namespace UsersGitHub.Services
{
    public class UserService : IUserService
    {
        public async Task<string> GetUserInfo(string userName)
        {
            UserDto user;
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.github.com")
            };
            var gitHubApi = RestService.For<IGitHubApi>(httpClient);
            try
            {
                user = await gitHubApi.GetUser(userName);
            }
            catch (Exception)
            {
                return null;
            }
            return user.Name;
        }

        public async Task<ObservableCollection<Repository>> GetUserRepositories(string userName)
        {
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.github.com")
            };
            var gitHubApi = RestService.For<IGitHubApi>(httpClient);
            IList<RepositoryDto> deserializedRepositories;
            try
            {
                deserializedRepositories = await gitHubApi.GetRepositories(userName);
            }
            catch (ApiException)
            {
                return null;
            }
            var repositories = new ObservableCollection<Repository>();
            foreach (var repos in deserializedRepositories)
            {
                repositories.Add(new Repository
                {
                    Name = repos.Name
                });
            }
            return repositories;
        }
    }
}
