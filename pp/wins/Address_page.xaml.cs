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

namespace pp.wins
{
    /// <summary>
    /// Логика взаимодействия для Address_page.xaml
    /// </summary>
    public partial class Address_page : Page
    {
        public Address_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void Address_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<Address> addresses = new List<Address>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select a.id_address as id, c.city_name as city, s.street_name as street, house_number as house, flat_number as flat from addresses as a left join cities as c on a.city_id = c.id_city left join streets as s on a.street_id = s.id_street;", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
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
            }
            DGaddress.ItemsSource = addresses;
        }
        private void DGaddress_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private void DGaddress_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }
        private void DGaddress_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Edit_Click();
        }
        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            Edit_Click();
        }
        private void BDelete_Click(object sender, RoutedEventArgs e)
        {
            Delete_Click();
        }
        private void Edit_Click()
        {
            Address si = (Address)DGaddress.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            //ew.frameM.Navigate(new Address_edit_page(si.id_address, si.fk_city_name, si.fk_street_name, si.house_number, si.flat_number));
        }
        private void Delete_Click()
        {
            Address si = (Address)DGaddress.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_address + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from addresses where id_address = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_address);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Address с id " + si.id_address + " удалено");
            }
        }
    }
}
