using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace UsersGitHub.Behaviors
{
    public class EntryValidationBehavior : Behavior<Entry>
    {
        private int maxValue = 50;

        protected override void OnAttachedTo(Entry bindable)
        {
            base.OnAttachedTo(bindable);
            bindable.TextChanged += BindableOnTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.TextChanged -= BindableOnTextChanged;
        }

        private void BindableOnTextChanged(object sender, TextChangedEventArgs e)
        {
            var newText = e.NewTextValue;
            var oldText = e.OldTextValue;
            var entry = sender as Entry;
            if (newText.Length > maxValue)
            {
                entry.Text = oldText;
                return;
            }
            if (!Regex.IsMatch(newText, "^[a-zA-Z0-9]+$"))
            {
                entry.TextColor = Color.Red;
                return;
            }
            entry.TextColor = Color.Black;
        }
    }
}
