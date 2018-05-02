using System.Linq;
using Prism.Navigation;
using Xamarin.Forms.Xaml;

namespace UsersGitHub.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : IDestructible
    {
        public Settings()
        {
            InitializeComponent();
        }

        public void Destroy()
        {
            lastNameEntry.Behaviors.Clear();
            firstNameEntry.Behaviors.Clear();
        }
    }
}