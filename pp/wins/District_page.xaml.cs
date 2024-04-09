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
    /// Логика взаимодействия для District_page.xaml
    /// </summary>
    public partial class District_page : Page
    {
        public District_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void District_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<District> districts = new List<District>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select d.id_district as id, d.district_name as name, r.region_name as region from districts as d left join regions as r on d.region_id = r.id_region", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        District record = new District();
                        record.id_district = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.district_name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name");
                        record.fk_region_name = reader.IsDBNull(reader.GetOrdinal("region")) ? string.Empty : reader.GetString("region");

                        districts.Add(record);
                    }
                }
            }
            DGdistrict.ItemsSource = districts;
        }
        private void DGdistrict_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private void DGdistrict_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }
        private void DGdistrict_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            District si = (District)DGdistrict.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            //ew.frameM.Navigate(new District_edit_page(si.id_district, si.district_name));
        }
        private void Delete_Click()
        {
            District si = (District)DGdistrict.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_district + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from districts where id_district = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_district);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("District с id " + si.id_district + " удалено");
            }
        }
    }
}
