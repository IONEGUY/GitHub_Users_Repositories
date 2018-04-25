using System;
using System.Collections.ObjectModel;
using UsersGitHub.Model.DtoModel;

namespace UsersGitHub.Model
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Login { get; set; }
        public ObservableCollection<Repository> Repositories { get; set; }
    }
}
