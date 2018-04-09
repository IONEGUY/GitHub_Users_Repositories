using UsersGitHub.ViewModel;
using Xamarin.Forms.Xaml;

namespace UsersGitHub.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginPageViewModel(() => DisplayAlert("Error", "This name doesn't exist", "OK"));
        }
    }
}