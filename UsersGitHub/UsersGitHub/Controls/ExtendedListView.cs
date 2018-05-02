using System.Threading;
using System.Windows.Input;
using UsersGitHub.Model;
using UsersGitHub.Views;
using Xamarin.Forms;

namespace UsersGitHub.Controls
{
    public class ExtendedListView : ListView
    {
        public static BindableProperty ItemSelectedCommandProperty =
            BindableProperty.Create(nameof(ItemSelectedCommand), typeof(ICommand),
                typeof(ExtendedListView), default(ICommand));

        public ICommand ItemSelectedCommand
        {
            get => (ICommand)GetValue(ItemSelectedCommandProperty);
            set => SetValue(ItemSelectedCommandProperty, value);
        }

        public ExtendedListView()
        {
            ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var item = selectedItemChangedEventArgs.SelectedItem;
            ItemSelectedCommand?.Execute(item);
        }
    }
}
