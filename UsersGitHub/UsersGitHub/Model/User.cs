using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UsersGitHub.Model
{
    public class User
    {
        public string UserName { get; set; }
        public ObservableCollection<Repository> Repositories { get; set; }
    }
}
