using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    /// TestMenuPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TestMenuPage : Page
    {
        private List<TestInfo> _list;
        public TestMenuPage()
        {
            InitializeComponent();
            _list = new List<TestInfo> {
                new TestInfo{Name="발사통제기A" },
                new TestInfo{Name="발사통제기B"},
                new TestInfo{Name="발사통제기C"},
                new TestInfo{Name="발사통제기D"},
                };
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menu_page = new MenuPage();
            NavigationService.Navigate(menu_page);
        }
        private void Inspection_Button_Click(object sender, RoutedEventArgs e)
        {
            TestDetailPage test_detail_page = new TestDetailPage();
            NavigationService.Navigate(test_detail_page);
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
                _list = new List<TestInfo> {
                new TestInfo{Name="발사통제기A" },
                new TestInfo{Name="발사통제기B"},
                new TestInfo{Name="발사통제기C"},
                new TestInfo{Name="발사통제기D"},
                };
                //foreach(var l in _list)
                // {
                //     Inspection_Select_Grid.s
                // }
            }
            else
            {
                _list = new List<TestInfo> {
                new TestInfo{Name="유도탄통제기A" },
                new TestInfo{Name="유도탄통제기B"},
                new TestInfo{Name="유도탄통제기C"},
                new TestInfo{Name="유도탄통제기D"},
             };
            }

            InspectionList.ItemsSource = _list;
        }
    }
    public class TestInfo
    {
        public string Name { get; set; }
    }
}