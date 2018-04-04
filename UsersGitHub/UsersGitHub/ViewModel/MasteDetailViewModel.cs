using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UsersGitHub.View;
using Xamarin.Forms;

namespace UsersGitHub.ViewModel
{
    public class MasteDetailViewModel : INotifyPropertyChanged
    {
        private Page Detail { get; set; }

        public MasteDetailViewModel(Page detail)
        {
            Detail = detail;
        }

        public void ShowDetail()
        {
            Detail = new Repos();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
