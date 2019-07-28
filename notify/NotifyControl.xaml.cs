using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace notify
{
    /// <summary>
    /// NotifyControl.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyControl : UserControl
    {
        private readonly ObservableCollection<NotifyClass> _baseCollection;
        private readonly Timer _timer;
        private bool _hidden;

        public NotifyClass ViewModel { get; }

        public NotifyControl(NotifyClass notifyClass, ObservableCollection<NotifyClass> baseCollection)
        {
            _baseCollection = baseCollection;
            InitializeComponent();
            if (notifyClass.IsEmpty)
            {
                _baseCollection.Remove(notifyClass);
                this.Visibility = Visibility.Collapsed;
                return;
            }

            DataContext = notifyClass;
            ViewModel = notifyClass;
            if (notifyClass.FadeoutTime > TimeSpan.FromSeconds(1))
            {
                _timer = new Timer(obj =>
                {
                    Dispatcher.Invoke(HideThis);
                    _timer?.Dispose();
                }, null, (int)notifyClass.FadeoutTime.TotalMilliseconds, Timeout.Infinite);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            ShowThis();
        }

        private void ShowThis()
        {
            var height = NotifyBorder.ActualHeight;
            var sineEase = new CircleEase
            {
                EasingMode = EasingMode.EaseOut,
            };
            var timeSpan = TimeSpan.FromMilliseconds(200);

            var heightAni = new DoubleAnimation
            {
                From = 0,
                To = height,
                EasingFunction = sineEase,
                Duration = new Duration(timeSpan)
            };

            Storyboard.SetTargetName(heightAni, NotifyBorder.Name);
            Storyboard.SetTargetProperty(heightAni,
                new PropertyPath(Border.HeightProperty));
            var opacityAni = new DoubleAnimation
            {
                From = 0,
                To = 0,
                EasingFunction = sineEase,
                Duration = new Duration(timeSpan)
            };
            Storyboard.SetTargetName(opacityAni, NotifyBorder.Name);
            Storyboard.SetTargetProperty(opacityAni,
                new PropertyPath(Border.OpacityProperty));

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(heightAni);
            myStoryboard.Children.Add(opacityAni);
            myStoryboard.Completed += ShowThisNextAction;
            myStoryboard.Begin(this);
        }

        private void ShowThisNextAction(object sender, EventArgs e)
        {
            var width = NotifyBorder.ActualWidth;
            var sineEase = new CircleEase
            {
                EasingMode = EasingMode.EaseOut,
            };
            var timeSpan = TimeSpan.FromMilliseconds(300);
            var opacityAni = new DoubleAnimation
            {
                From = 0,
                To = 1,
                EasingFunction = sineEase,
                Duration = new Duration(timeSpan)
            };
            Storyboard.SetTargetName(opacityAni, NotifyBorder.Name);
            Storyboard.SetTargetProperty(opacityAni,
                new PropertyPath(Border.OpacityProperty));

            var marginAni = new ThicknessAnimation
            {
                From = new Thickness(width, 0, -width, 0),
                To = new Thickness(0),
                EasingFunction = sineEase,
                Duration = new Duration(timeSpan)
            };
            Storyboard.SetTargetName(marginAni, NotifyBorder.Name);
            Storyboard.SetTargetProperty(marginAni,
                new PropertyPath(Border.MarginProperty));

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(opacityAni);
            myStoryboard.Children.Add(marginAni);
            myStoryboard.Begin(this);
        }

        private void HideThis()
        {
            if (_hidden) return;
            _hidden = true;

            var width = NotifyBorder.ActualWidth;
            var sineEase = new CubicEase
            {
                EasingMode = EasingMode.EaseOut,
            };
            var timeSpan = TimeSpan.FromMilliseconds(300);
            var opacityAni = new DoubleAnimation
            {
                From = 1,
                To = 0,
                EasingFunction = sineEase,
                Duration = new Duration(timeSpan)
            };
            Storyboard.SetTargetName(opacityAni, NotifyBorder.Name);
            Storyboard.SetTargetProperty(opacityAni,
                new PropertyPath(Border.OpacityProperty));

            var marginAni = new ThicknessAnimation
            {
                From = new Thickness(0),
                To = new Thickness(width, 0, -width, 0),
                EasingFunction = sineEase,
                Duration = new Duration(timeSpan)
            };
            Storyboard.SetTargetName(marginAni, NotifyBorder.Name);
            Storyboard.SetTargetProperty(marginAni,
                new PropertyPath(Border.MarginProperty));

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(opacityAni);
            myStoryboard.Children.Add(marginAni);
            myStoryboard.Completed += HideThisNextAction;
            myStoryboard.Begin(this);
            _baseCollection.Remove(this.ViewModel);
        }

        private void HideThisNextAction(object sender, EventArgs e)
        {
            var height = NotifyBorder.ActualHeight;
            var sineEase = new CircleEase
            {
                EasingMode = EasingMode.EaseOut,
            };
            var timeSpan = TimeSpan.FromMilliseconds(200);

            var heightAni = new DoubleAnimation
            {
                From = height,
                To = 0,
                EasingFunction = sineEase,
                Duration = new Duration(timeSpan)
            };

            Storyboard.SetTargetName(heightAni, NotifyBorder.Name);
            Storyboard.SetTargetProperty(heightAni,
                new PropertyPath(Border.HeightProperty));
            var opacityAni = new DoubleAnimation
            {
                From = 0,
                To = 0,
                EasingFunction = sineEase,
                Duration = new Duration(timeSpan)
            };
            Storyboard.SetTargetName(opacityAni, NotifyBorder.Name);
            Storyboard.SetTargetProperty(opacityAni,
                new PropertyPath(Border.OpacityProperty));

            var myStoryboard = new Storyboard();
            myStoryboard.Children.Add(heightAni);
            myStoryboard.Children.Add(opacityAni);
            myStoryboard.Begin(this);
        }

        private void NotifyControl_Click(object sender, RoutedEventArgs e)
        {
            HideThis();
        }
    }
}