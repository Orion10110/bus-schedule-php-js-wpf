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
using MySql.Data.MySqlClient;
using System.Data;

namespace WpfApplication4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

       

        public MainWindow()
        {
            InitializeComponent();
            //LV.Items.Add(new Busstop(1,"Фолюш"));

            //M();
            MainPage mainP = new MainPage();
            frame.NavigationService.Navigate(mainP);
        }

        private void Canvas_TouchDown(object sender, TouchEventArgs e)
        {
            frame.NavigationService.Navigate(new Uri("BaseForm.xaml", UriKind.Relative));
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            frame.NavigationService.Navigate(new Uri("BaseForm.xaml", UriKind.Relative));
        }

        


    }
}
