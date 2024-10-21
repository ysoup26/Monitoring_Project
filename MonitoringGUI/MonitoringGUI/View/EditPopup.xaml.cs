using MonitoringGUI.DB_info;
using System;
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
using System.Windows.Shapes;

namespace MonitoringGUI.View
{
    /// <summary>
    /// EditPopup.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class EditPopup : Window
    {
        public string query { get; set; }
        public string now_name { get; set; }
        public string now_temp { get; set; }
        public string now_hum { get; set; }
        public string now_id { get; set; }
        public EditPopup()
        {
            InitializeComponent();
            name.Text = now_name;
            is_temp_check.IsChecked = (now_temp.Equals("1")) ? true : false;
            is_hum_check.IsChecked = (now_hum.Equals("1")) ? true : false;
        }
        public EditPopup(string n,string t,string h,string i)
        {
            InitializeComponent();
            name.Text = n;
            is_temp_check.IsChecked = (t.Equals("1")) ? true : false;
            is_hum_check.IsChecked = (h.Equals("1")) ? true : false;
            now_id = i;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.query = "INSERT INTO  " + AWS.target_table + "(name,temp,hum) VALUES('" + name + "'," + temp + "," + hum + ");";
            this.DialogResult = true;
            this.Close();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var nameBorder = (Border)name.Template.FindName("border", name);

            //입력 유효성 검사
            if (name.Text == "")//둘다 입력 안함
            {
                nameError.Visibility = Visibility.Visible;
                nameBorder.BorderBrush = new SolidColorBrush(Colors.IndianRed);

            }
            else //로그인
            {
                this.now_name = name.Text;
                this.now_temp = (is_temp_check.IsChecked == true) ? "1" : "0";
                this.now_hum = (is_hum_check.IsChecked == true) ? "1" : "0";
                this.query = "UPDATE " + AWS.target_table + " SET name = '" + now_name + "', temp = " + now_temp + ", hum = " + now_hum + " WHERE id = "+now_id;
                this.DialogResult = true;
                this.Close();
            }

        }
        private void CancleButton_Click(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //재 입력시 경고 문구를 안보이게 하는 함수
        private void Error_Hidden(object sender, DependencyPropertyChangedEventArgs e)
        {
            var nameBorder = (Border)name.Template.FindName("border", name);
            nameBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF93DFFF"));
            nameError.Visibility = Visibility.Hidden;
        }
    }
}