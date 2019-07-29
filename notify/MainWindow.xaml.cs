using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Notification
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] _contentList =
        {
            "",
            "这样更容易快速更新，以及方便别的dev参考开发",
            "我觉得还不如把DefaultPlugin那些功能给拆分了",
            "zhe kan qilai shi lishi yiliu wenti",
            "如果是无边窗全屏或者窗口化是可以让rtpp窗口置顶显示"
        };

        private string[] _titleList =
        {
            "",
            "You。",
            "Dark Prophet",
            "兔肉TROU",
            "黄花菜 v3.5"
        };

        private static Random _rnd = new Random();
        public MainWindow()
        {
            InitializeComponent();
        }

        private readonly ObservableCollection<NotificationOption> _notifyClasses = new ObservableCollection<NotificationOption>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            notify1.ItemsSource = _notifyClasses;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _notifyClasses.Add(new NotificationOption
            {
                IconTemplate = (ControlTemplate)Application.Current.FindResource("SettingsTempl"),
                NotificationType = (NotificationType)_rnd.Next(2),
                FadeoutTime = TimeSpan.FromSeconds(0),
                Content = _contentList[_rnd.Next(_contentList.Length)],
                Title = _titleList[_rnd.Next(_titleList.Length)]
            });
        }
    }
}
