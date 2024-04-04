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
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<Personal_card> personal_cards = new List<Personal_card>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select pc.id_person, pc.last_name, pc.name, pc.surname, pc.birth_date, concat(c.city_name, ' ', s.street_name, ' ', a.house_number, ' ', a.flat_number) as address, b.bank_name as bank, pc.bank_account, pc.INN, pc.SNILS, pc.employment_date, pc.dismissal_date from personal_cards as pc join addresses as a on pc.address_id = a.id_address join cities as c on a.city_id = c.id_city join streets as s on a.street_id = s.id_street join banks as b on pc.bank_id = b.id_bank", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Personal_card record = new Personal_card();
                        record.id_person = reader.IsDBNull(reader.GetOrdinal("id_person")) ? 0 : reader.GetInt32("id_person");
                        record.last_name = reader.IsDBNull(reader.GetOrdinal("last_name")) ? string.Empty : reader.GetString("last_name");
                        record.name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name");
                        record.surname = reader.IsDBNull(reader.GetOrdinal("surname")) ? string.Empty : reader.GetString("surname");
                        record.birth_date = reader.IsDBNull(reader.GetOrdinal("birth_date")) ? string.Empty : reader.GetDateTime("birth_date").ToString("yyyy-MM-dd");
                        record.fk_address = reader.IsDBNull(reader.GetOrdinal("address")) ? string.Empty : reader.GetString("address");
                        record.fk_bank = reader.IsDBNull(reader.GetOrdinal("bank")) ? string.Empty : reader.GetString("bank");
                        record.bank_account = reader.IsDBNull(reader.GetOrdinal("bank_account")) ? string.Empty : reader.GetString("bank_account");
                        record.INN = reader.IsDBNull(reader.GetOrdinal("INN")) ? string.Empty : reader.GetString("INN");
                        record.SNILS = reader.IsDBNull(reader.GetOrdinal("SNILS")) ? string.Empty : reader.GetString("SNILS");
                        record.employment_date = reader.IsDBNull(reader.GetOrdinal("employment_date")) ? string.Empty : reader.GetDateTime("employment_date").ToString("yyyy-MM-dd");
                        record.dismissal_date = reader.IsDBNull(reader.GetOrdinal("dismissal_date")) ? string.Empty : reader.GetDateTime("dismissal_date").ToString("yyyy-MM-dd");

                        personal_cards.Add(record);
                    }
                }
            }
            DGpersonal_cards.ItemsSource = personal_cards;
        }

        private void DGpersonal_cards_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Personal_card si = (Personal_card)DGpersonal_cards.SelectedItem;
            Delete_Click(si);
        }

        private void Delete_Click(Personal_card si)
        {
            MessageBoxResult result = MessageBox.Show("удалить строку с id " + si.id_person + "?", "подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from personal_cards where id = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_person);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("personal card с id " + si.id_person + "  удалена");
            }
        }
        private void Edit_Click(Personal_card si)
        {
            MessageBox.Show(Convert.ToString(si.id_person));
        }

        private void DGpersonal_cards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Personal_card si = (Personal_card)DGpersonal_cards.SelectedItem;
            Edit_Click(si);
        }

        private void DGpersonal_cards_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var row = (DataGridRow)(sender as DataGrid).ItemContainerGenerator.ContainerFromItem(((FrameworkElement)e.OriginalSource).DataContext);
            if (row != null)
            {
                if (row.IsSelected)
                {
                    (sender as DataGrid).UnselectAll();
                    row.IsSelected = true;
                }
                else row.IsSelected = true;
            }
        }
    }
}
