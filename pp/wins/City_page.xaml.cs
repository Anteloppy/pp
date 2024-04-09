using MySql.Data.MySqlClient;
using pp.edit_wins;
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
    /// Логика взаимодействия для City_page.xaml
    /// </summary>
    public partial class City_page : Page
    {
        public City_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void City_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<City> cities = new List<City>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select c.id_city as id, c.city_name as name, d.district_name as district from cities as c left join districts as d on c.district_id = d.id_district", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        City record = new City();
                        record.id_city = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.city_name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name");
                        record.fk_district_name = reader.IsDBNull(reader.GetOrdinal("district")) ? string.Empty : reader.GetString("district");

                        cities.Add(record);
                    }
                }
            }
            DGcity.ItemsSource = cities;
        }
        private void DGcity_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private void DGcity_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }
        private void DGcity_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            City si = (City)DGcity.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            //ew.frameM.Navigate(new City_edit_page(si.id_city, si.city_name, si.fk_district_name));
        }
        private void Delete_Click()
        {
            City si = (City)DGcity.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_city + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from cities where id_city = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_city);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("City с id " + si.id_city + " удалено");
            }
        }
    }
}
