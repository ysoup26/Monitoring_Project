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
using System.Windows.Controls.Primitives;

namespace MonitoringGUI.View
{
    /// <summary>
    /// MonitoringMenuPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MonitoringMenuPage : Page
    {
        bool IsDelete = false;
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
            //https://shine10e.tistory.com/68 이전 페이지로 돌아올 때 추가 처리를 해야되면 참고 
        }
        //세부 모니터링 삭제/선택 버튼
        private void Monitoring_Button_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var id = button.Tag.ToString();
            //해당 모니터링 삭제
            if (IsDeleteButton.IsChecked == true)
            {
                if (MessageBox.Show("정말 삭제하시겠습니까?\n", "모니터링 삭제", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    return;
                }
                var viewModel = (MonitoringMenuPageViewModel)DataContext;
                string query = "DELETE FROM " + AWS.target_table + " WHERE id = "+id;
                viewModel.MySqlQueryExecuter(query);
                viewModel.GetAllTarget();
                
            }
            else
            {
                MonitoringDetailPage monitoring_detail_page = new MonitoringDetailPage(id);
                NavigationService.Navigate(monitoring_detail_page);
            }
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
            EditPopup popup = new EditPopup();
            if (popup.ShowDialog() == true)
            {
                MessageBox.Show(popup.query);
                var viewModel = (MonitoringMenuPageViewModel)DataContext;
                //string query = "INSERT INTO  " + AWS.target_table + "(name) VALUES('InWPF2')";
                viewModel.MySqlQueryExecuter(popup.query);
                viewModel.GetAllTarget();
            }
            
        }
        //모니터링 조회 버튼
        private void Monitoring_Check_Button_Click(object sender, RoutedEventArgs e)
        {
            //InspectionList.ItemsSource = _list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
                            list.Add(new MonitoringInfo { Name = reader["name"].ToString(), Id = int.Parse(reader["id"].ToString()) });
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
