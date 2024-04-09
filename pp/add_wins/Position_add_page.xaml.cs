using MySql.Data.MySqlClient;
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

namespace pp.add_wins
{
    /// <summary>
    /// Логика взаимодействия для Position_add_page.xaml
    /// </summary>
    public partial class Position_add_page : Page
    {
        public Position_add_page()
        {
            InitializeComponent();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            string add = "";
            if (string.IsNullOrEmpty(TBname.Text))
            {
                MessageBox.Show("Заполните пустое поле.");
                return;
            }
            else
                add = "insert into positions(position_name) values('" + TBname.Text + "'); commit;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(add, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Position добавлено.");
        }
    }
}
