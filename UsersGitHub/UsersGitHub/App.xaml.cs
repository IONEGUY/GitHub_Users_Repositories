using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Akavache;
using UsersGitHub.Model;
using UsersGitHub.View;
using UsersGitHub.ViewModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace UsersGitHub
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

		    MainPage = new NavigationPage(new LoginPage());
		}

		protected override async void OnStart ()
		{
		    BlobCache.ApplicationName = "UsersGitHub";
		    await BlobCache.UserAccount.InvalidateAll();
		    var collection = await BlobCache.UserAccount.GetAllObjects<User>();
            if (collection.Any())
		    {
		        Current.MainPage = new UsersReposPage();
		    }
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
