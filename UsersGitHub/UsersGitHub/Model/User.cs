using System.Collections.ObjectModel;

namespace UsersGitHub.Model
{
    public class User
    {
        public string UserName { get; set; }
        public ObservableCollection<Repository> Repositories { get; set; }
    }
}
