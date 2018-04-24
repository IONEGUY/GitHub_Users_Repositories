using Prism.Mvvm;
using Prism.Navigation;

namespace UsersGitHub.ViewModels
{
	public class BaseViewModel : BindableBase
	{
	    public INavigationService NavigationService { get; set; }

        public BaseViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
	}
}
