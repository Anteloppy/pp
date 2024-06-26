﻿using MySql.Data.MySqlClient;
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
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace pp.edit_wins
{
    /// <summary>
    /// Логика взаимодействия для Personal_cards_edit_page.xaml
    /// </summary>
    public partial class Personal_cards_edit_page : Page
    {
        public Personal_cards_edit_page()
        {
            InitializeComponent();
        }
        public Personal_cards_edit_page(int id_person, string last_name, string name, string surname, string birth_date, string fk_address, string fk_bank, string bank_account, string INN, string SNILS, string employment_date, string dismissal_date)
        {
            InitializeComponent();

            TBid.Text = Convert.ToString(id_person);
            TBlast_name.Text = last_name;
            TBname.Text = name;
            TBsurname.Text = surname;
            TBbirth_date.Text = birth_date;
            LoadData();
            TBbank_account.Text = bank_account;
            TBinn.Text = INN;
            TBsnils.Text = SNILS;
            TBemployment_date.Text = employment_date;
            TBdismissal_date.Text = dismissal_date;
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

            MySqlCommand address_ = new MySqlCommand("select address_id as address from personal_cards;", conn);
            using (MySqlDataReader reader = address_.ExecuteReader())
            {
                if (reader.Read())
                {
                    int address_id = reader.IsDBNull(reader.GetOrdinal("address")) ? 0 : reader.GetInt32("address");
                    CBaddress.SelectedIndex = address_id;
                }
            }

            MySqlCommand bank_ = new MySqlCommand("select bank_id as bank from personal_cards;", conn);
            using (MySqlDataReader reader = bank_.ExecuteReader())
            {
                if (reader.Read())
                {
                    int bank_id = reader.IsDBNull(reader.GetOrdinal("bank")) ? 0 : reader.GetInt32("bank");
                    CBbank.SelectedIndex = bank_id;
                }
            }
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("editing " + TBid.Text);
            string edit = "";
            if (string.IsNullOrEmpty(TBlast_name.Text) || string.IsNullOrEmpty(TBname.Text) || string.IsNullOrEmpty(TBsurname.Text) || string.IsNullOrEmpty(TBbirth_date.Text) || string.IsNullOrEmpty(TBbank_account.Text) || string.IsNullOrEmpty(TBinn.Text) || string.IsNullOrEmpty(TBsnils.Text) || string.IsNullOrEmpty(TBemployment_date.Text) || string.IsNullOrEmpty(TBdismissal_date.Text))
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите продолжить? Есть пустые поля.", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    edit = "update personal_cards set ";
                    if (!string.IsNullOrEmpty(TBlast_name.Text))
                        edit += "last_name = '" + TBlast_name.Text + "', ";
                    else
                        edit += "last_name = null, ";
                    if (!string.IsNullOrEmpty(TBname.Text))
                        edit += "name = '" + TBname.Text + "', ";
                    else
                        edit += "name = null, ";
                    if (!string.IsNullOrEmpty(TBsurname.Text))
                        edit += "surname = '" + TBsurname.Text + "', ";
                    else
                        edit += "surname = null, ";
                    if (!string.IsNullOrEmpty(TBbirth_date.Text))
                        edit += "birth_date = '" + TBbirth_date.Text + "', ";
                    else
                        edit += "birth_date = null, ";
                    if (!string.IsNullOrEmpty(CBaddress.Text))
                        edit += "address_id = " + CBaddress.SelectedIndex + ", ";
                    else
                        edit += "address_id = null, ";
                    if (!string.IsNullOrEmpty(CBbank.Text))
                        edit += "bank_id = " + CBbank.SelectedIndex + ", ";
                    else
                        edit += "bank_id = null, ";
                    if (!string.IsNullOrEmpty(TBbank_account.Text))
                        edit += "bank_account = " + TBbank_account.Text + "', ";
                    else
                        edit += "bank_account = null, ";
                    if (!string.IsNullOrEmpty(TBinn.Text))
                        edit += "INN = " + TBinn.Text + "', ";
                    else
                        edit += "INN = null, ";
                    if (!string.IsNullOrEmpty(TBsnils.Text))
                        edit += "SNILS = '" + TBsnils.Text + "', ";
                    else
                        edit += "SNILS = null, ";
                    if (!string.IsNullOrEmpty(TBbank_account.Text))
                        edit += "employment_date = '" + TBemployment_date.Text + "', ";
                    else
                        edit += "employment_date = null, ";
                    if (!string.IsNullOrEmpty(TBbank_account.Text))
                        edit += "dismissal_date = '" + TBdismissal_date.Text + "', ";
                    else
                        edit += "dismissal_date =  null, ";
                    edit += "where id_person = @id; commit;";
                }
                else return;
            }
            else
            edit = "update personal_cards set last_name= '" + TBlast_name.Text + "', name = '" + TBname.Text + "', surname = '" + TBsurname.Text + "', birth_date = '" + TBbirth_date.Text + "', address_id = '" + CBaddress.SelectedIndex + "', bank_id = '" + CBbank.SelectedIndex + "', bank_account = '" + TBbank_account.Text + "', INN = '" + TBinn.Text + "', SNILS = '" + TBsnils.Text + "', employment_date = '" + TBemployment_date.Text + "', dismissal_date = '" + TBdismissal_date.Text + "' where id_person = @id; commit;";

            int id = Convert.ToInt32(TBid.Text);
            
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(edit, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Personal card с id " + id + " изменено.");
        }
    }
}
