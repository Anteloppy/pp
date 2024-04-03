using MySql.Data.MySqlClient;
using pp.entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
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

namespace pp.wins
{
    /// <summary>
    /// Логика взаимодействия для Personal_cards_page.xaml
    /// </summary>
    public partial class Personal_cards_page : Page
    {
        public Personal_cards_page()
        {
            InitializeComponent();
            //LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<Personal_card> personal_cards = new List<Personal_card>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from clients", conn);


                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Personal_card record = new Personal_card();
                        record.id_person = reader.GetInt32("");
                        record.last_name = reader.GetString("");
                        record.name = reader.GetString("");
                        record.surname = reader.GetString("");
                        record.birht_date = reader.GetString("");
                        record.fk_address = reader.GetString("");
                        record.fk_bank = reader.GetString("");
                        record.bank_account = reader.GetString("");
                        record.INN = reader.GetString("");
                        record.SNILS = reader.GetString("");
                        record.employment_date = reader.GetString("");
                        record.dismissal_date = reader.GetString("");

                        personal_cards.Add(record);
                    }
                }
            }
            DGpersonal_cards.ItemsSource = personal_cards;
        }

        private void DGpersonal_cards_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
