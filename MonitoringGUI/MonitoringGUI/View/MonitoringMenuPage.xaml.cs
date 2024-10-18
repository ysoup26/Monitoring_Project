using MySql.Data.MySqlClient;
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
using MonitoringGUI.DB_info;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Google.Protobuf.Collections;
using System.Diagnostics;
using DevExpress.Mvvm.Native;

namespace MonitoringGUI.View
{
    /// <summary>
    /// MonitoringMenuPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MonitoringMenuPage : Page
    {
        //private List<MonitoringInfo> _list;
        public MonitoringMenuPage()
        {
            InitializeComponent();
            this.DataContext = new MonitoringMenuPageViewModel();
        }
        //메뉴 페이지로 돌아가는 버픈
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MenuPage menu_page = new MenuPage();
            NavigationService.Navigate(menu_page);
        }
        //세부 모니터링을 선택하는 버튼
        private void Monitoring_Button_Click(object sender, RoutedEventArgs e)
        {
            MonitoringDetailPage monitoring_detail_page = new MonitoringDetailPage();
            NavigationService.Navigate(monitoring_detail_page);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //_list = LoadData();
            //InspectionList.ItemsSource = _list;
            //this.Window_Loaded(sender, e);
        }
        //신규 모니터링 등록 버튼
        private void Monitoring_Resister_Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MonitoringMenuPageViewModel)DataContext;
            string query = "INSERT INTO  " + AWS.target_table + "(name) VALUES('InWPF2')";
            viewModel.MySqlQueryExecuter(query);
            viewModel.GetAllTarget();
        }
        // 모니터링 삭제 버튼
        private void Monitoring_Delete_Button_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (MonitoringMenuPageViewModel)DataContext;
            string query = "DELETE FROM " + AWS.target_table + " WHERE id = 6";
            viewModel.MySqlQueryExecuter(query);
            viewModel.GetAllTarget();
        }
        //모니터링 조회 버튼
        private void Monitoring_Check_Button_Click(object sender, RoutedEventArgs e)
        {
            //InspectionList.ItemsSource = _list;
        }
    }

    public class MonitoringMenuPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MonitoringInfo> data { get; set; }
        public MonitoringMenuPageViewModel() {
            GetAllTarget(); //데이터 로드
        }

        public class MonitoringInfo
        {
            public string Name { get; set; }
            public int Id { get; set; }
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
                            list.Add(new MonitoringInfo { Name = reader["name"].ToString() });
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
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
