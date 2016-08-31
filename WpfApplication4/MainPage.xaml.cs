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

namespace WpfApplication4
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private int v;

        public MainPage()
        {
            InitializeComponent();
            
        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            Button temp = sender as Button;

            Station stationP = new Station(temp.Content.ToString());
            this.NavigationService.Navigate(stationP);
        }

        private void Addstation_Click(object sender, RoutedEventArgs e)
        {
            AddStation AddstationP = new AddStation();
            this.NavigationService.Navigate(AddstationP);
        }
    }
}
