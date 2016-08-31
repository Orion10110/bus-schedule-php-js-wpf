using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для Station.xaml
    /// </summary>
    /// 

   

    public partial class Station : Page
    {
        private int count = 0;

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

        private string orient = "прямо";


        private List<string> day = new List<string>();

        public List<string> Day
        {
            get { return day; }
            set { day = value; }
        }
        private List<string> wday = new List<string>();

        public List<string> WDay
        {
            get { return wday; }
            set { wday = value; }
        }


        private string bus;

        public string Bus
        {
            get { return bus; }
            set { bus = value; }
        }

        private Busstop tbusStop;
        

        public Busstop TBusStop
        {
            get { return tbusStop; }
            set { tbusStop = value; }
        }



        public Station()
        {
            InitializeComponent();
            button.DataContext = this;
        }

        public Station(string numberBus)
        {
            InitializeComponent();
            button.DataContext = this;
            
            this.Bus = numberBus;
            M();
            

        }

        private void button_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }


         public void M()
        {




            LV.Items.Clear();
            string sql = @"SELECT ID,NAME_STOP
FROM  `MAINROUTE` inner join `STOPBUS` USING(ID_STOP_BUS) where TNUMBER ='" + Bus + "' and `ROUTE`='"+orient+"' "; // Строка запроса
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
                ListStation Station = new ListStation(int.Parse(idBus), nameBusstat);
                LV.Items.Add(Station);
            }

        }


        public void AddState1()
        {

            listView.Items.Clear();
            string sql = "SELECT * FROM STOPBUS ORDER BY NAME_STOP";  // Строка запроса
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
                listView.Items.Add(Station);
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddState1();
            panel0.Visibility = Visibility.Hidden;
            panel3.Visibility = Visibility.Hidden;
            panel2.Visibility = Visibility.Hidden;
            panel1.Visibility = Visibility.Visible;
        }

        private void step2_Click(object sender, RoutedEventArgs e)
        {

            if (listView.SelectedItem != null)
            {
                panel0.Visibility = Visibility.Visible;
                TBusStop = listView.SelectedItem as Busstop;


                panel1.Visibility = Visibility.Hidden;
                
                label.Content = TBusStop.Name +" будний";
                maskTb.Text = "";

            }
            

           
        }

        private void step3_Click(object sender, RoutedEventArgs e)
        {

            if (count%2 == 0 )
            {
                day.Clear();
                foreach (TextBox tx in chR.Children)
                {
                    
                    day.Add(tx.Text);
                    tx.Text = "";
                }
                count++;
                label.Content = TBusStop.Name + " выходной";
                for (int i = 1, j = 0; j < listw.Count; i++, j++)
                {
                    TextBox temp = chR.Children[i] as TextBox;
                    temp.Text = listw[j];
                }



            }
            else if(count%2 != 0)
            {
                

                wday.Clear();
                foreach (TextBox tx in chR.Children)
                {
                    wday.Add(tx.Text);
                    tx.Text = "";
                    
                   
                }
                count++;
                panel2.Visibility = Visibility.Hidden;
                AddState2();
            }
            
        }



        public void AddState2()
        {
            
            string query = @"INSERT INTO `TIMETRANSPORT`( `DESCRIPTION`, `T5`, `T6`, `T7`, `T8`, `T9`, `T10`, `T11`, `T12`, `T13`, `T14`, `T15`, `T16`, `T17`, `T18`, `T19`, `T20`, `T21`, `T22`, `T23`, `T0`, `T1`) VALUES";

            string qp1 = "('" + bus + " " + orient+ " " + TBusStop.Name + "',";
            foreach (string st in day)
            {
                qp1 += "'" + st + "',";
            }
            
            qp1 =qp1.Remove(qp1.Length - 1);
            qp1 += "),";
            string qp2 = "('" + bus + " "+orient+  " выходной " + TBusStop.Name + "',";
            foreach (string st in wday)
            {
                qp2 += "'" + st + "',";
            }

           qp2=  qp2.Remove(qp2.Length - 1);
            qp2 += ");";


            query += qp1+qp2;


             // Строка запроса
            MySqlConnection connection = new MySqlConnection(connStr);
            MySqlCommand sqlCom = new MySqlCommand(query, connection);
            connection.Open();
            sqlCom.ExecuteNonQuery();
          
            Addstate3();

        }


        string w = "";
        string d = "";
        public void Addstate3()
        {

            
            string sql = "SELECT ID_TIME FROM TIMETRANSPORT ORDER BY ID_TIME DESC LIMIT 2";  // Строка запроса
            MySqlConnection connection = new MySqlConnection(connStr);
            MySqlCommand sqlCom = new MySqlCommand(sql, connection);
            connection.Open();
            sqlCom.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            var myData = dt.Select();
           
                 w = myData[0].ItemArray[0].ToString();
                 d = myData[1].ItemArray[0].ToString();

            Addstate4();
        }

        public void Addstate4()
        {

            
            string sql = "INSERT INTO `MAINROUTE`( `TNUMBER`, `ROUTE`, `ROUTENAME`, `ID_STOP_BUS`, `WEEKDAYS`, `WEEKEND`) VALUES ('"+Bus+ "','"+orient+"','1 ','" +TBusStop.ID +"','" +d+ "','"+w +"')";  // Строка запроса
            MySqlConnection connection = new MySqlConnection(connStr);
            MySqlCommand sqlCom = new MySqlCommand(sql, connection);
            connection.Open();
            sqlCom.ExecuteNonQuery();

            M();
            

           


        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem temp = comboBox.SelectedItem as ComboBoxItem;
            orient = temp.Content.ToString();
            M();
        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            if (LV.SelectedIndex != -1)
            {
                MessageBoxResult answer = MessageBox.Show("Удалить", "1", MessageBoxButton.YesNo);
                if (answer== MessageBoxResult.Yes)
                {
                    ListStation temp = LV.SelectedItem as ListStation;
                    string sql = "SELECT `WEEKDAYS`,`WEEKEND` FROM `MAINROUTE` WHERE ID='" + temp.ID + "';";  // Строка запроса
                    MySqlConnection connection = new MySqlConnection(connStr);
                    MySqlCommand sqlCom = new MySqlCommand(sql, connection);
                    connection.Open();
                    sqlCom.ExecuteNonQuery();
                    MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    var myData = dt.Select();

                    string one = myData[0].ItemArray[0].ToString();
                    string second = myData[0].ItemArray[1].ToString();

                    string querydel = "DELETE FROM `TIMETRANSPORT` WHERE `ID_TIME`='" + one + "' or `ID_TIME`='" + second + "';";
                    sqlCom = new MySqlCommand(querydel, connection);

                    sqlCom.ExecuteNonQuery();


                    string querydel1 = "DELETE FROM `MAINROUTE` WHERE ID='" + temp.ID + "';";
                    sqlCom = new MySqlCommand(querydel1, connection);

                    sqlCom.ExecuteNonQuery();

                }
                M();
                }

               
        }
        string one;
        string second;
        ListStation update_station;
        private void button1_Copy1_Click(object sender, RoutedEventArgs e)
        {
            if (LV.SelectedIndex != -1)
            {
                panel2.Visibility = Visibility.Hidden;
                panel1.Visibility = Visibility.Hidden;
                panel3.Visibility = Visibility.Visible;
                update_station = LV.SelectedItem as ListStation;
                SetCb();
                label.Content = update_station.Name;
                string sql = "SELECT `WEEKDAYS`,`WEEKEND` FROM `MAINROUTE` WHERE ID='" + update_station.ID + "';";  // Строка запроса
                MySqlConnection connection = new MySqlConnection(connStr);
                MySqlCommand sqlCom = new MySqlCommand(sql, connection);
                connection.Open();
                sqlCom.ExecuteNonQuery();
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(sqlCom);
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                var myData = dt.Select();

                one = myData[0].ItemArray[0].ToString();
                second = myData[0].ItemArray[1].ToString();

                string querydel = "SELECT * FROM `TIMETRANSPORT` WHERE `ID_TIME`='" + one + "' or `ID_TIME`='" + second + "';";
                sqlCom = new MySqlCommand(querydel, connection);

                sqlCom.ExecuteNonQuery();
                dataAdapter = new MySqlDataAdapter(sqlCom);
                DataTable dt1 = new DataTable();
                dataAdapter.Fill(dt1);
                myData = dt1.Select();
                int k = 2;
                foreach (TextBox tx in chRd.Children)
                {
                    
                    tx.Text = myData[0].ItemArray[k].ToString();
                    k++;
                }
                k = 2;
                foreach (TextBox tx in chRw.Children)
                {

                    tx.Text = myData[1].ItemArray[k].ToString();
                    k++;
                }

               

            }
        }

        private void step4_Click(object sender, RoutedEventArgs e)
        {
            day.Clear();
            wday.Clear();
            foreach (TextBox tx in chRd.Children)
            {
                day.Add(tx.Text);
               


            }

            foreach (TextBox tx in chRw.Children)
            {
                wday.Add(tx.Text);
                


            }



            string sql = "UPDATE `TIMETRANSPORT` SET ";
            string qp1 = "";
            int k = 5;
            foreach (string st in day)
            {

                qp1 += "`T"+k+"`='" + st + "',";
                if (k < 23)
                {
                    k++;
                }
                else
                {
                    k = 0;
                }
               
            }

            qp1 = qp1.Remove(qp1.Length - 1);
            qp1 += " WHERE `ID_TIME`='"+one+"';";
            sql += qp1;



            MySqlConnection connection = new MySqlConnection(connStr);
            MySqlCommand sqlCom = new MySqlCommand(sql, connection);
            connection.Open();
            sqlCom.ExecuteNonQuery();

            sql = "UPDATE `TIMETRANSPORT` SET ";
            string qp2 = "";
             k = 5;
            foreach (string st in wday)
            {

                qp2 += "`T" + k + "`='" + st + "',";
                if (k < 23)
                {
                    k++;
                }
                else
                {
                    k = 0;
                }
            }

            qp2 = qp2.Remove(qp2.Length - 1);
            qp2 += " WHERE `ID_TIME`='" + second + "';";
            sql += qp2;
            sqlCom = new MySqlCommand(sql, connection);

            sqlCom.ExecuteNonQuery();
            Busstop temp = cb2.SelectedItem as Busstop;

            sql = "UPDATE `MAINROUTE` SET `ID_STOP_BUS`='"+ temp.ID+"' WHERE ID='" + update_station.ID + "';";
            sqlCom = new MySqlCommand(sql, connection);

            sqlCom.ExecuteNonQuery();
            panel3.Visibility = Visibility.Hidden;
            M();
        }

        private void SetCb()
        {
           
            cb2.Items.Clear();
            string sql = "SELECT * FROM STOPBUS ORDER BY NAME_STOP";  // Строка запроса
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
                cb2.Items.Add(Station);
            }
            foreach (Busstop cmi in cb2.Items)
            {
                if (cmi.Name == update_station.Name)
                {
                    cb2.SelectedItem = cmi;
                }
            }
        }
        List<string> listd;
        List<string> listw;
        private void button0_Click(object sender, RoutedEventArgs e)
        {
            string mainS = ui.Text;
            string mainSw = uiw.Text;
            listd = new List<string>(mainS.Split('\t'));
            listw = new List<string>(mainSw.Split('\t'));
            ui.Text = "";
            uiw.Text = "";
            for (int i = 1, j = 0; j < listd.Count; i++, j++)
            {
                TextBox temp = chR.Children[i] as TextBox;
                temp.Text = listd[j];
            }
            panel0.Visibility = Visibility.Hidden;
            panel2.Visibility = Visibility.Visible;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            listView.Items.Clear();
            TextBox temp = sender as TextBox;
            string sql = "SELECT * FROM STOPBUS WHERE NAME_STOP LIKE '%"+temp.Text+"%' ORDER BY NAME_STOP";  // Строка запроса
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
                listView.Items.Add(Station);
            }
        }
    }
}
