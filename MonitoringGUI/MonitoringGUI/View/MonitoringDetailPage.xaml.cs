using DevExpress.Mvvm;
using DevExpress.Xpf.Editors.Helpers;
using MonitoringGUI.DB_info;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
        public MonitoringDetailPage(string id)
        {
            InitializeComponent();
            TEST.Text = id;
            this.DataContext = new MonitoringDetailPageViewModel(id);

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
        public DateTime time {  get; set; }
        public double temp { get; set; }
        
    }


    public class MonitoringDetailPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Temp> Data { get; set; }
        public MonitoringInfo info { get; set; }

        public ObservableCollection<MonitoringInfo> data { get; set; }
        public ObservableCollection<Temp> Temp_Data { get; set; }
        public MonitoringDetailPageViewModel()
        {
            this.Data = GetTempData();

        }
        public MonitoringDetailPageViewModel(string id)
        {
            //GetAllTarget();
            GetTarget(id);
            this.Data = GetTempData();

        }
        public class MonitoringInfo
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public DateTime Time { get; set; }
            public string Temp { get; set; }
            public string Hum {  get; set; }
        }

        //Do query: Insert, Delete, Update
        public void MySqlQueryExecuter(string query)
        {
            try
            {
                //using이 있는 동안만 연결(끝나고 DB 연결 자동 해제)
                using (var connection = new MySqlConnection(AWS.url))
                {
                    connection.Open();
                    var command = new MySqlCommand(query, connection);
                    if (command.ExecuteNonQuery() == 1)
                    {
                        Debug.WriteLine("값 저장 성공");
                    }
                    else
                        Debug.WriteLine("값 저장 실패");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //모니터링 대상 전체 출력: query select
        public void GetAllTarget()
        {
            try
            {
                using (var connection = new MySqlConnection(AWS.url))
                {
                    connection.Open();
                    string query = "SELECT * FROM " + AWS.target_table;
                    var command = new MySqlCommand(query, connection);
                    ObservableCollection<MonitoringInfo> list = new ObservableCollection<MonitoringInfo> { };
                    using (var reader = command.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            // 데이터 처리 로직
                            list.Add(new MonitoringInfo { Name = reader["name"].ToString(), Id = reader["id"].ToString() });
                        }
                        data = list;
                        OnPropertyChanged(nameof(data));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        //모니터링 대상 전체 출력: query select
        public void GetAllTemp()
        {
            try
            {
                using (var connection = new MySqlConnection(AWS.url))
                {
                    connection.Open();
                    string query = "SELECT * FROM " + AWS.temp_table+" WHERE target_id = "+info.Id;
                    var command = new MySqlCommand(query, connection);
                    Temp_Data = new ObservableCollection<Temp> { };
                    using (var reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            // 데이터 처리 로직
                            Temp_Data.Add(new Temp { time = reader["time"].TryConvertToDateTime(), temp = Convert.ToDouble(reader["temp"]), });
                        }
                        OnPropertyChanged(nameof(Temp_Data));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        //모니터링 대상 단일 출력
        public void GetTarget(string id)
        {
            try
            {
                using (var connection = new MySqlConnection(AWS.url))
                {
                    connection.Open();
                    string query = "SELECT * FROM " + AWS.target_table+" WHERE id = "+id;
                    var command = new MySqlCommand(query, connection);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            info = new MonitoringInfo
                            {
                                Name = reader["name"].ToString(),
                                Id = reader["id"].ToString(),
                                Time = reader["time"].TryConvertToDateTime(),
                                Temp = reader["temp"].ToString(),
                                Hum = reader["hum"].ToString(),
                            };
                        }  
                        OnPropertyChanged(nameof(info));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        
        public static ObservableCollection<Temp> GetTempData()
        {
            return new ObservableCollection<Temp>
            {
                new Temp{time = new DateTime(2024, 5, 6, 7, 0, 0), temp=25},
                new Temp{time = new DateTime(2024, 5, 6, 8, 0, 0), temp=30},
                new Temp{time = new DateTime(2024, 5, 6, 9, 0, 0), temp=20},
                new Temp{time = new DateTime(2024, 5, 6, 10, 0, 0), temp=28},
                new Temp{time = new DateTime(2024, 5, 6, 11, 0, 0), temp=35},
            };
        }
        public void UpdateTempData()
        {
            
            this.Data = new ObservableCollection<Temp>
            {
                new Temp { time = DateTime.Parse("2024-05-06 07:00:00"), temp = 0 },
                new Temp { time = new DateTime(2024, 5, 6, 8, 0, 0), temp = 40 },
                new Temp { time = new DateTime(2024, 5, 6, 9, 0, 0), temp = 22 },
                new Temp { time = new DateTime(2024, 5, 6, 10, 0, 0), temp = 38 },
                new Temp { time = new DateTime(2024, 5, 6, 11, 0, 0), temp = 50 },
            };
            OnPropertyChanged(nameof(Data));
        }
        
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
}
