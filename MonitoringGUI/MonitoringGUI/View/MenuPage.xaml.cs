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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MonitoringGUI.View
{
    /// <summary>
    /// MenuPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MenuPage : Page
    {
        public MenuPage()
        {
            InitializeComponent();
        }
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            var menu_name = ((Button)sender).Name;
            switch (menu_name)
            {
                case "self":
                    TestMenuPage test_menu_page = new TestMenuPage();
                    NavigationService.Navigate(test_menu_page);
                    break;
                case "monitoring":
                    MonitoringMenuPage monitoring_menu_page = new MonitoringMenuPage();
                    //MonitoringDetailPage monitoring_menu_page = new MonitoringDetailPage();
                    NavigationService.Navigate(monitoring_menu_page);
                    break;
                default:
                    break;
            }
        }
        private void LogoutButton_Click(Object sender, RoutedEventArgs e)
        {
            LoginPage login_page = new LoginPage();
            NavigationService.Navigate(login_page);
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
    }
}
