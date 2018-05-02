using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly HttpClient httpClient;
        private readonly IGitHubApi gitHubApi;
        private const string uri = "https://api.github.com";

        public UserService()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(uri)
            };
            gitHubApi = RestService.For<IGitHubApi>(httpClient);
        }

        public async Task<string> GetUserInfo(string userName)
        {
            UserDto user;
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
            IEnumerable<RepositoryDto> deserializedRepositories;
            try
            {
                deserializedRepositories = await gitHubApi.GetRepositories(userName);
            }
            catch (Exception)
            {
                return null;
            }
            return GetRepos(deserializedRepositories);
        }

        private ObservableCollection<Repository> GetRepos(IEnumerable<RepositoryDto> deserializedRepositories)
        {
            var repositories = new ObservableCollection<Repository>();
            foreach (var reposDto in deserializedRepositories)
            {
                var repos = Mapper.Map<Repository>(reposDto);
                repositories.Add(repos);
            }
            return repositories;
        }
    }
}
