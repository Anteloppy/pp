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
    /// Логика взаимодействия для Structure_page.xaml
    /// </summary>
    public partial class Structure_page : Page
    {
        public Structure_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void Structure_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<Structure> structures = new List<Structure>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select s.id_structure as id, d.department_name as department, p.position_name as position, s.salary, s.bonus from structure as s left join departments as d on s.department_id = d.id_department left join positions as p on s.position_id = p.id_position", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Structure record = new Structure();
                        record.id_structure = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.fk_department_name = reader.IsDBNull(reader.GetOrdinal("department")) ? string.Empty : reader.GetString("department");
                        record.fk_position_name = reader.IsDBNull(reader.GetOrdinal("position")) ? string.Empty : reader.GetString("position");
                        record.salary = reader.IsDBNull(reader.GetOrdinal("salary")) ? 0 : reader.GetInt32("salary");
                        record.bonus = reader.IsDBNull(reader.GetOrdinal("bonus")) ? 0 : reader.GetInt32("bonus");

                        structures.Add(record);
                    }
                }
            }
            DGstructure.ItemsSource = structures;
        }
        private void DGstructure_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private void DGstructure_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }
        private void DGstructure_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            Structure si = (Structure)DGstructure.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            //ew.frameM.Navigate(new Structure_edit_page(si.id_structure, si.fk_department_name, si.fk_position_name, si.salary, si.bonus));
        }
        private void Delete_Click()
        {
            Structure si = (Structure)DGstructure.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_structure + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from structure where id_structure = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_structure);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Structure с id " + si.id_structure + " удалено");
            }
        }
    }
}
