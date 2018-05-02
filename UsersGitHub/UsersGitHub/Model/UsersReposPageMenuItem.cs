using System;
using Xamarin.Forms;

namespace UsersGitHub.Model
{
    public class UsersReposPageMenuItem
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public static explicit operator UsersReposPageMenuItem(Page v)
        {
            throw new NotImplementedException();
        }
    }
}