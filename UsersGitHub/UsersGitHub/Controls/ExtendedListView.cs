using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using UsersGitHub.View;
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
            get => (ICommand) GetValue(ItemSelectedCommandProperty);
            set => SetValue(ItemSelectedCommandProperty, value);
        }

        public ExtendedListView()
        {
            ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var page = (UsersReposPageMenuItem)selectedItemChangedEventArgs.SelectedItem;
            ItemSelectedCommand?.Execute(page.Title);
        }
    }
}
