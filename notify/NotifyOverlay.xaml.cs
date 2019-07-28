using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace notify
{
    /// <summary>
    /// NotifyOverlay.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyOverlay : UserControl
    {
        private ObservableCollection<NotifyClass> _itemsSource;

        public NotifyOverlay()
        {
            InitializeComponent();
        }

        public ObservableCollection<NotifyClass> ItemsSource
        {
            get => _itemsSource;
            set
            {
                if (_itemsSource != null)
                {
                    _itemsSource.CollectionChanged -= Oc_CollectionChanged;
                }

                _itemsSource = value;
                _itemsSource.CollectionChanged += Oc_CollectionChanged;
            }
        }

        private void Oc_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (var newItem in e.NewItems)
                {
                    TriggerIn((NotifyClass)newItem);
                }
            }
        }

        private void TriggerIn(NotifyClass newItem)
        {
            var sb = new NotifyControl(newItem, _itemsSource);
            NotifyStack.Children.Add(sb);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
      
        }
    }
}
