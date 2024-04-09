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
    /// Логика взаимодействия для Personel_reserve_page.xaml
    /// </summary>
    public partial class Personnel_reserve_page : Page
    {
        public Personnel_reserve_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void Personnel_reserve_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<Personnel_reserve> personel_reserves = new List<Personnel_reserve>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select r.id_personnel_reserve as id, concat(e.last_name, ' ', e.name, ' ', e.surname) as person, concat(d.department_name, ' ', p.position_name, ' ', s.salary) as structure, r.reserve_entry_date, r.reserve_exit_date, r.reserve_status, concat(dn.department_name, ' ', pn.position_name, ' ', sn.salary) as new_structure, w.schedule_name as work_schedule from personnel_reserve as r left join personal_cards as e on r.person_id = e.id_person left join structure as s on r.structure_id = s.id_structure left join departments as d on s.department_id = d.id_department left join positions as p on s.position_id = p.id_position left join structure as sn on r.structure_id = sn.id_structure left join departments as dn on sn.department_id = dn.id_department left join positions as pn on sn.position_id = pn.id_position left join work_schedules as w on r.work_schedule_id = w.id_work_schedule", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Personnel_reserve record = new Personnel_reserve();
                        record.id_personnel_reserve = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.fk_person = reader.IsDBNull(reader.GetOrdinal("person")) ? string.Empty : reader.GetString("person");
                        record.fk_structure = reader.IsDBNull(reader.GetOrdinal("structure")) ? string.Empty : reader.GetString("structure");
                        record.reserve_entry_date = reader.IsDBNull(reader.GetOrdinal("reserve_entry_date")) ? string.Empty : reader.GetDateTime("reserve_entry_date").ToString("yyyy-MM-dd");
                        record.reserve_exit_date = reader.IsDBNull(reader.GetOrdinal("reserve_exit_date")) ? string.Empty : reader.GetDateTime("reserve_exit_date").ToString("yyyy-MM-dd");
                        record.reserve_status = reader.IsDBNull(reader.GetOrdinal("reserve_status")) ? string.Empty : reader.GetString("reserve_status");
                        record.fk_new_structure = reader.IsDBNull(reader.GetOrdinal("new_structure")) ? string.Empty : reader.GetString("new_structure");
                        record.fk_work_shedule_name = reader.IsDBNull(reader.GetOrdinal("work_schedule")) ? string.Empty : reader.GetString("work_schedule");

                        personel_reserves.Add(record);
                    }
                }
            }
            DGpersonel_reserve.ItemsSource = personel_reserves;
        }
        private void DGpersonel_reserve_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private void DGpersonel_reserve_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }
        private void DGpersonel_reserve_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            Personnel_reserve si = (Personnel_reserve)DGpersonel_reserve.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            //ew.frameM.Navigate(new Personel_reserve_edit_page(si.id_personnel_reserve, si.fk_person, si.fk_structure, si.reserve_entry_date, si.reserve_exit_date, si.reserve_status, si.fk_new_structure, si.fk_work_shedule_name));
        }
        private void Delete_Click()
        {
            Personnel_reserve si = (Personnel_reserve)DGpersonel_reserve.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_personnel_reserve + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from  where id_personel_reserve = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_personnel_reserve);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Personnel reserve с id " + si.id_personnel_reserve + " удалено");
            }
        }
    }
}
