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
    /// Логика взаимодействия для Department_page.xaml
    /// </summary>
    public partial class Department_page : Page
    {
        public Department_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void Department_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        public void LoadData()
        {
            List<Department> departments = new List<Department>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select id_department as id, department_name as name from departments", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Department record = new Department();
                        record.id_department = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.department_name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name");

                        departments.Add(record);
                    }
                }
            }
            DGdepartment.ItemsSource = departments;
        }
        private void DGdepartment_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private void DGdepartment_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }
        private void DGdepartment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            Department si = (Department)DGdepartment.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            ew.frameM.Navigate(new Department_edit_page(si.id_department, si.department_name));
        }
        private void Delete_Click()
        {
            Department si = (Department)DGdepartment.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_department + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from departments where id_department = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_department);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Department с id " + si.id_department + " удалено");
            }
        }
    }
}
