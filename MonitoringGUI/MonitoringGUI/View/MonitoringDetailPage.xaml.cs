using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Chart.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MonitoringDetailPage : Page
    {
        public MonitoringDetailPage()
        {
            InitializeComponent();

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MonitoringMenuPage monitoring_menu_page = new MonitoringMenuPage();
            NavigationService.Navigate(monitoring_menu_page);
        }
        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var item = (ListViewItem)sender;         
            if (item.Name.Equals("item1"))
            {
                Content1.Visibility = Visibility.Visible;
                Content2.Visibility = Visibility.Hidden;
            }
            else
            {
                Content2.Visibility = Visibility.Visible;
                Content1.Visibility = Visibility.Hidden;
            }

        }
    }
    public class DataPoint
    {
        public string Argument { get; set; }
        public double Value { get; set; }
        public static ObservableCollection<DataPoint> GetDataPoints()
        {
            return new ObservableCollection<DataPoint>
            {
                new DataPoint { Argument = "Asia", Value = 5.289D},
                    new DataPoint { Argument = "Australia", Value = 2.2727D},
                    new DataPoint { Argument = "Europe", Value = 3.7257D},
                    new DataPoint { Argument = "North America", Value = 4.1825D},
                    new DataPoint { Argument = "South America", Value = 2.1172D},

            };
        }
        }
    public class MonitoringDetailPageViewModel
    {
        public ObservableCollection<DataPoint> Data { get; private set; }
        public MonitoringDetailPageViewModel()
        {
            this.Data = DataPoint.GetDataPoints();
        }
    }
    
}
