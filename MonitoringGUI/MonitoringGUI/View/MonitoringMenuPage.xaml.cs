using System;
using System.Collections;
using System.Collections.Generic;
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

namespace MonitoringGUI.View
{
    /// <summary>
    /// MonitoringMenuPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MonitoringMenuPage : Page
    {
        private List<MonitoringInfo> _list;
        public MonitoringMenuPage()
        {
            InitializeComponent();
            _list = new List<MonitoringInfo> {
                new MonitoringInfo{Name="발사통제기A" },
                new MonitoringInfo{Name="발사통제기B"},
                new MonitoringInfo{Name="발사통제기C"},
                new MonitoringInfo{Name="발사통제기D"},
                };
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menu_page = new MenuPage();
            NavigationService.Navigate(menu_page);
        }
        private void Monitoring_Button_Click(object sender, RoutedEventArgs e)
        {
            MonitoringDetailPage monitoring_detail_page = new MonitoringDetailPage();
            NavigationService.Navigate(monitoring_detail_page);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InspectionList.ItemsSource = _list;
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var item = (ListViewItem)sender;
            item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF25275A"));
            if (item.Name.Equals("Inspection1"))
            {
                _list = new List<MonitoringInfo> {
                new MonitoringInfo{Name="발사통제기A" },
                new MonitoringInfo{Name="발사통제기B"},
                new MonitoringInfo{Name="발사통제기C"},
                new MonitoringInfo{Name="발사통제기D"},
                };
                //foreach(var l in _list)
                // {
                //     Inspection_Select_Grid.s
                // }
            }
            else
            {
                _list = new List<MonitoringInfo> {
                new MonitoringInfo{Name="유도탄통제기A" },
                new MonitoringInfo{Name="유도탄통제기B"},
                new MonitoringInfo{Name="유도탄통제기C"},
                new MonitoringInfo{Name="유도탄통제기D"},
             };
            }

            InspectionList.ItemsSource = _list;
        }
    }
    public class MonitoringInfo
    {
        public string Name { get; set; }
    }
}
