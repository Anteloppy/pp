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
    /// Логика взаимодействия для Work_schedule_page.xaml
    /// </summary>
    public partial class Work_schedule_page : Page
    {
        public Work_schedule_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void Work_schedule_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        public void LoadData()
        {
            List<Work_schedule> work_schedules = new List<Work_schedule>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select id_work_schedule as id, schedule_name as name, working_days, days_off, working_hours_per_day from work_schedules", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Work_schedule record = new Work_schedule();
                        record.id_work_schedule = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.schedule_name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name");
                        record.working_days = reader.IsDBNull(reader.GetOrdinal("working_days")) ? 0 : reader.GetInt32("working_days");
                        record.days_off = reader.IsDBNull(reader.GetOrdinal("days_off")) ? 0 : reader.GetInt32("days_off");
                        record.working_hours_per_day = reader.IsDBNull(reader.GetOrdinal("working_hours_per_day")) ? 0 : reader.GetInt32("working_hours_per_day");

                        work_schedules.Add(record);
                    }
                }
            }
            DGwork_schedule.ItemsSource = work_schedules;
        }

        private void DGwork_schedule_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private void DGwork_schedule_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }
        private void DGwork_schedule_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            Work_schedule si = (Work_schedule)DGwork_schedule.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            //ew.frameM.Navigate(new Department_edit_page(si.id_work_schedule, si.schedule_name, si.working_days, si.days_off, si.working_hours_per_day));
        }
        private void Delete_Click()
        {
            Work_schedule si = (Work_schedule)DGwork_schedule.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_work_schedule + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from work_schedules where id_work_schedule = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_work_schedule);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Work schedule с id " + si.id_work_schedule + " удалено");
            }
        }
    }
}
