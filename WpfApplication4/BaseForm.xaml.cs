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
    /// Логика взаимодействия для BaseForm.xaml
    /// </summary>
    public partial class BaseForm : Page
    {

   
      static string serverName = "localhost"; // Адрес сервера (для локальной базы пишите "localhost")
    static string userName = "root"; // Имя пользователя
    static string dbName = "TIMEBUS"; //Имя базы данных
    static string port = "3306"; // Порт для подключения
    static string password = ""; // Пароль для подключения
    static string connStr = "server=" + serverName +
        ";user=" + userName +
        ";database=" + dbName +
        ";port=" + port +
        ";password=" + password + ";";

    public BaseForm()
    {
        InitializeComponent();
        //LV.Items.Add(new Busstop(1,"Фолюш"));

        M();

    }

    public void M()
    {




        LV.Items.Clear();
        string sql = "SELECT * FROM STOPBUS"; // Строка запроса
        MySqlConnection connection = new MySqlConnection(connStr);
        MySqlCommand sqlCom = new MySqlCommand(sql, connection);
        connection.Open();
        sqlCom.ExecuteNonQuery();
        MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
        DataTable dt = new DataTable();
        dataAdapter.Fill(dt);

        var myData = dt.Select();
        for (int i = 0; i < myData.Length; i++)
        {

            //for (int j = 0; j < myData[i].ItemArray.Length; j++)
            String idBus = myData[i].ItemArray[0].ToString();
            String nameBusstat = myData[i].ItemArray[1].ToString();
            Busstop Station = new Busstop(int.Parse(idBus), nameBusstat);
            LV.Items.Add(Station);
        }

    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
        MySqlConnection conn = new MySqlConnection(connStr);
        conn.Open();
        string text = TextBoxNameStation.Text;
        string sql = "INSERT INTO `STOPBUS`(`NAME_STOP`) VALUES ('" + text + "');"; // Строка запроса
        MySqlConnection connection = new MySqlConnection(connStr);
        MySqlCommand sqlCom = new MySqlCommand(sql, connection);
        connection.Open();
        sqlCom.ExecuteNonQuery();
        M();
    }


}
}
