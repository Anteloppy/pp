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
    /// Логика взаимодействия для Region_page.xaml
    /// </summary>
    public partial class Region_page : Page
    {
        public Region_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void Region_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        public void LoadData()
        {
            List<Region> regions = new List<Region>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select id_region as id, region_name as name from regions", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Region record = new Region();
                        record.id_region = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.region_name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name");

                        regions.Add(record);
                    }
                }
            }
            DGregion.ItemsSource = regions;
        }
        private void DGregion_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private void DGregion_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }
        private void DGregion_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            Region si = (Region)DGregion.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            ew.frameM.Navigate(new Region_edit_page(si.id_region, si.region_name));
        }
        private void Delete_Click()
        {
            Region si = (Region)DGregion.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_region + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from regions where id_region = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_region);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Region с id " + si.id_region + " удалено");
            }
        }
    }
}
