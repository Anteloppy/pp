using MySql.Data.MySqlClient;
using pp.entities;
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
    /// Логика взаимодействия для Personal_cards_add_page.xaml
    /// </summary>
    public partial class Personal_card_add_page : Page
    {
        public Personal_card_add_page()
        {
            InitializeComponent();
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<Address> addresses = new List<Address>();
            List<Bank> banks = new List<Bank>();
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd_a = new MySqlCommand("select a.id_address as id, c.city_name as city, s.street_name as street, house_number as house, flat_number as flat from addresses as a join cities as c on a.city_id = c.id_city join streets as s on a.street_id = s.id_street;", conn);
            using (MySqlDataReader reader = cmd_a.ExecuteReader())
            {
                while (reader.Read())
                {
                    Address record = new Address();
                    record.id_address = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                    record.fk_city_name = reader.IsDBNull(reader.GetOrdinal("city")) ? string.Empty : reader.GetString("city");
                    record.fk_street_name = reader.IsDBNull(reader.GetOrdinal("street")) ? string.Empty : reader.GetString("street");
                    record.house_number = reader.IsDBNull(reader.GetOrdinal("house")) ? string.Empty : reader.GetString("house");
                    record.flat_number = reader.IsDBNull(reader.GetOrdinal("flat")) ? 0 : reader.GetInt32("flat");

                    addresses.Add(record);
                }
            }
            CBaddress.ItemsSource = addresses;

            MySqlCommand cmd_b = new MySqlCommand("select id_bank as id, bank_name as name from banks;", conn);
            using (MySqlDataReader reader = cmd_b.ExecuteReader())
            {
                while (reader.Read())
                {
                    Bank record = new Bank();
                    record.id_bank = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                    record.bank_name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name");

                    banks.Add(record);
                }
            }
            CBbank.ItemsSource = banks;
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            string add = "";
            if (string.IsNullOrEmpty(TBlast_name.Text) || string.IsNullOrEmpty(TBname.Text) || string.IsNullOrEmpty(TBsurname.Text) ||
    string.IsNullOrEmpty(TBbirth_date.Text) || CBaddress.SelectedIndex == -1 || string.IsNullOrEmpty(TBbank_account.Text) ||
    string.IsNullOrEmpty(TBinn.Text) || string.IsNullOrEmpty(TBsnils.Text) || string.IsNullOrEmpty(TBemployment_date.Text) ||
    string.IsNullOrEmpty(TBdismissal_date.Text))
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите продолжить? Есть пустые поля.", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    string start = "", end = "";
                    start = "insert into personal_cards (";
                    if (!string.IsNullOrEmpty(TBlast_name.Text))
                    {
                        start += "last_name, ";
                        end += "'" + TBlast_name.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBname.Text))
                    {
                        start += "name, ";
                        end += "'" + TBname.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBsurname.Text))
                    {
                        start += "surname, ";
                        end += "'" + TBsurname.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBbirth_date.Text))
                    {
                        start += "birth_date, ";
                        end += "'" + TBbirth_date.Text + "', ";
                    }
                    if (CBaddress.SelectedIndex >= 0)
                    {
                        start += "address_id, ";
                        int a = CBaddress.SelectedIndex + 1;
                        end += a + ", ";
                    }
                    if (CBbank.SelectedIndex >= 0)
                    {
                        start += "bank_id, ";
                        int a = CBbank.SelectedIndex + 1;
                        end += a + ", ";
                    }
                    if (!string.IsNullOrEmpty(TBbank_account.Text))
                    {
                        start += "bank_account, ";
                        end += "'" + TBbank_account.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBinn.Text))
                    {
                        start += "INN, ";
                        end += "'" + TBinn.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBsnils.Text))
                    {
                        start += "SNILS, ";
                        end += "'" + TBsnils.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBemployment_date.Text))
                    {
                        start += "employment_date, ";
                        end += "'" + TBemployment_date.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBdismissal_date.Text))
                    {
                        start += "dismissal_date, ";
                        end += "'" + TBdismissal_date.Text + "', ";
                    }
                    start = start.Remove(start.Length - 2, 2);
                    end = end.Remove(end.Length - 2, 2);
                    add += start + ") values (" + end + ") ; commit;";
                }
                else return;
            }
            else { add = "insert into personal_cards(last_name, name, surname, birth_date, address_id, bank_id, bank_account, INN, SNILS, employment_date, dismissal_date) values('" + TBlast_name.Text + "', '" + TBname.Text + "', '" + TBsurname.Text + "', " + TBbirth_date.Text + ", " + CBaddress.SelectedIndex + ", " + CBaddress.SelectedIndex + ", '" + TBbank_account + "', '" + TBinn.Text + "', '" + TBsnils.Text + "', " + TBemployment_date.Text + ", " + TBdismissal_date.Text + ") ; commit;"; }
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(add, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("карточка сотрудника добавлена");
        }
    }
}
