using System.Windows.Input;
using Xamarin.Forms;

namespace UsersGitHub.Controls
{
    public class ExtendedEntry : Entry
    {
        public static readonly BindableProperty TextChangedCommandProperty =
            BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand),
                typeof(ExtendedEntry), default(ICommand));

        public ICommand TextChangedCommand
        {
            get => (ICommand) GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }

        public ExtendedEntry()
        {
            TextChanged += OnTextChanged;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
        {
            TextChangedCommand?.Execute(null);
        }
    }
}
