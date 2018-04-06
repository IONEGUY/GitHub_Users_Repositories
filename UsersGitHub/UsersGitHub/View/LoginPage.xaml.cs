using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using UsersGitHub.Model;
using UsersGitHub.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UsersGitHub.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            BindingContext = new LoginPageViewModel();
		}
    }
}