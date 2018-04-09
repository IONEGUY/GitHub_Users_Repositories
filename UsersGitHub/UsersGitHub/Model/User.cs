using System;
using System.Collections.ObjectModel;
using UsersGitHub.Model.DtoModel;

namespace UsersGitHub.Model
{
    public class User
    {
        public string UserName { get; set; }
        public ObservableCollection<Repository> Repositories { get; set; }
    }
}
