using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace notify
{
    public class NotifyClass : INotifyPropertyChanged
    {
        private ControlTemplate _iconTemplate;
        private string _title = "Title";
        private string _content = "This is your content here";
        private TimeSpan _fadeoutTime;

        public ControlTemplate IconTemplate
        {
            get => _iconTemplate;
            set
            {
                _iconTemplate = value;
                OnPropertyChanged();
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public string Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan FadeoutTime
        {
            get => _fadeoutTime;
            set
            {
                _fadeoutTime = value;
                OnPropertyChanged();
            }
        }

        public bool IsEmpty => string.IsNullOrEmpty(Title) && string.IsNullOrEmpty(Content) && IconTemplate == null;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}