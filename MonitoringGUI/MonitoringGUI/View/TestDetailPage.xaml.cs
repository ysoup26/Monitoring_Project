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
    /// TestDetailPage.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TestDetailPage : Page
    {
        List<InspectionInfo> _list;
        public TestDetailPage()
        {
            InitializeComponent();
            _list = new List<InspectionInfo>
                {
                    new InspectionInfo {Name ="중앙제어용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="전원용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="외부용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="경보용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="개별용 회로카드 조립체 #1",Img="./" },
                    new InspectionInfo {Name ="개별용 회로카드 조립체 #2",Img="./" },
                    new InspectionInfo {Name ="통신용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="전면표기용 회로카드 조립체",Img="./" },
                };
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            TestMenuPage test_menu_page = new  TestMenuPage();
            NavigationService.Navigate(test_menu_page);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InspectionList.ItemsSource = _list;
        }

        private void ListViewItem_Selected(object sender, RoutedEventArgs e)
        {
            var item = (ListViewItem)sender;
            item.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF25275A"));
            if (item.Name.Equals("Self"))
            {
                _list = new List<InspectionInfo>
                {
                    new InspectionInfo {Name ="중앙제어용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="전원용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="외부용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="경보용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="개별용 회로카드 조립체 #1",Img="./" },
                    new InspectionInfo {Name ="개별용 회로카드 조립체 #2",Img="./" },
                    new InspectionInfo {Name ="통신용 회로카드 조립체",Img="./" },
                    new InspectionInfo {Name ="전면표기용 회로카드 조립체",Img="./" },
                };
            }
            else
            {
                _list = new List<InspectionInfo>
                {
                    new InspectionInfo {Name ="스피커",Img="./" },
                    new InspectionInfo {Name ="FCU LED",Img="./" },
                    new InspectionInfo {Name ="Radio LED",Img="./" },
                    new InspectionInfo {Name ="MSB R LED",Img="./" },
                    new InspectionInfo {Name ="MSB L LED",Img="./" },
                    new InspectionInfo {Name ="MPSU R LED",Img="./" },
                    new InspectionInfo {Name ="MPSU L LED",Img="./" },
                };
            }

            InspectionList.ItemsSource = _list;
        }
    }
    public class InspectionInfo
    {
        public string Name { get; set; }
        public string Img { get; set; }
    }
}
