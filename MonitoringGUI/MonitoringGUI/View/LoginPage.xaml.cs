using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// LoginPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class LoginPage : Page
    {
        String TrueId;
        String TruePassword;
        public LoginPage()
        {
            InitializeComponent();
            TrueId = "1234";
            TruePassword = "1234";
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var idBorder = (Border)id.Template.FindName("border", id);
            var passwordBorder = (Border)password.Template.FindName("border", password);

            //입력 유효성 검사
            if (id.Text == "" && password.Password == "")//둘다 입력 안함
            {
                idError.Visibility = Visibility.Visible;
                passwordError.Visibility = Visibility.Visible;
                idBorder.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                passwordBorder.BorderBrush = new SolidColorBrush(Colors.IndianRed);

            }
            else if (id.Text == "")
            {  //아이디 입력 안함
                idError.Visibility = Visibility.Visible;
                idBorder.BorderBrush = new SolidColorBrush(Colors.IndianRed);
            }
            else if (password.Password == "") //비번 입력안함
            {
                passwordError.Visibility = Visibility.Visible;
                passwordBorder.BorderBrush = new SolidColorBrush(Colors.IndianRed);
            }
            else if (id.Text != TrueId || password.Password != TruePassword)//틀린 유저 정보->경고 표시
            {
                idBorder.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                passwordBorder.BorderBrush = new SolidColorBrush(Colors.IndianRed);
                loginError.Visibility = Visibility.Visible;
            }
            else //로그인
            {
                MenuPage menuPage = new MenuPage();
                NavigationService.Navigate(menuPage);
            }

        }
        private void ExitButton_Click(Object sender, RoutedEventArgs e)
        {
            MessageBoxResult mbr = MessageBox.Show("종료하시겠습니까?", "프로그램 종료", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (mbr == MessageBoxResult.Yes)
            {
                System.Environment.Exit(0);
            }
            else
                return;
        }

        //재 입력시 id 경고 문구를 안보이게 하는 함수
        private void id_Error_Hidden(object sender, DependencyPropertyChangedEventArgs e)
        {
            var idBorder = (Border)id.Template.FindName("border", id);
            idBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF93DFFF"));
            idError.Visibility = Visibility.Hidden;
            loginError.Visibility = Visibility.Hidden;
        }
        //재 입력시 password 경고 문구를 안보이게 하는 함수
        private void password_Error_Hidden(object sender, DependencyPropertyChangedEventArgs e)
        {
            var passwordBorder = (Border)password.Template.FindName("border", password);
            passwordBorder.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF93DFFF"));
            passwordError.Visibility = Visibility.Hidden;
            loginError.Visibility = Visibility.Hidden;
        }

        //password의 placeholder를 안보이게 하는 함수(id는 trigger로 구현)
        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var passwordBox = sender as PasswordBox;
            if (passwordBox == null || passwordBox.Password.Equals(""))
            {
                password_Block.Visibility = Visibility.Visible;
            }
            else
            {
                password_Block.Visibility = Visibility.Hidden;
            }

        }
    }
}
