using DevExpress.Mvvm;
using MonitoringGUI.DataModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class MonitoringDetailPage : Page
    {
        public MonitoringDetailPage()
        {
            InitializeComponent();
            this.DataContext = new MonitoringDetailPageViewModel();

        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MonitoringMenuPage monitoring_menu_page = new MonitoringMenuPage();
            NavigationService.Navigate(monitoring_menu_page);
        }

        private void ChartUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MonitoringDetailPageViewModel)DataContext;
            viewModel.UpdateTempData();
        }
    }

    public class Temp
    {
        public DateTime date {  get; set; }
        public double temp { get; set; }
        
    }
    public class MonitoringDetailPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Temp> Data { get; set; }
        public static ObservableCollection<Temp> GetTempData()
        {
            return new ObservableCollection<Temp>
            {
                new Temp{date = new DateTime(2024, 5, 6, 7, 0, 0), temp=25},
                new Temp{date = new DateTime(2024, 5, 6, 8, 0, 0), temp=30},
                new Temp{date = new DateTime(2024, 5, 6, 9, 0, 0), temp=20},
                new Temp{date = new DateTime(2024, 5, 6, 10, 0, 0), temp=28},
                new Temp{date = new DateTime(2024, 5, 6, 11, 0, 0), temp=35},
            };
        }
        public void UpdateTempData()
        {
            
            this.Data = new ObservableCollection<Temp>
            {
                new Temp { date = DateTime.Parse("2024-05-06 07:00:00"), temp = 0 },
                new Temp { date = new DateTime(2024, 5, 6, 8, 0, 0), temp = 40 },
                new Temp { date = new DateTime(2024, 5, 6, 9, 0, 0), temp = 22 },
                new Temp { date = new DateTime(2024, 5, 6, 10, 0, 0), temp = 38 },
                new Temp { date = new DateTime(2024, 5, 6, 11, 0, 0), temp = 50 },
            };
            OnPropertyChanged(nameof(Data));
        }
        public MonitoringDetailPageViewModel()
        {
            this.Data = GetTempData();
            
        }
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
