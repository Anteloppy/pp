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
    /// Логика взаимодействия для Contact_info_page.xaml
    /// </summary>
    public partial class Contact_info_page : Page
    {
        public Contact_info_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void Contact_info_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<Contact_info> contact_infos = new List<Contact_info>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select c.id_contact_info as id, concat(e.last_name, ' ', e.name, ' ', e.surname) as person, d.department_name as department, p.position_name as position, c.mobile_phone, c.landline_phone, c.email, concat(s.last_name, ' ', s.name, ' ', s.name) as supervisor from contact_info as c left join personal_cards as e on c.person_id = e.id_person left join personal_cards as s on c.supervisor_id = s.id_person left join departments as d on c.department_id = d.id_department left join positions as p on c.position_id = p.id_position", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Contact_info record = new Contact_info();
                        record.id_contact_info = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.fk_person = reader.IsDBNull(reader.GetOrdinal("person")) ? string.Empty : reader.GetString("person");
                        record.fk_department_name = reader.IsDBNull(reader.GetOrdinal("department")) ? string.Empty : reader.GetString("department");
                        record.fk_position_name = reader.IsDBNull(reader.GetOrdinal("position")) ? string.Empty : reader.GetString("position");
                        record.mobile_phone = reader.IsDBNull(reader.GetOrdinal("mobile_phone")) ? string.Empty : reader.GetString("mobile_phone");
                        record.landline_phone = reader.IsDBNull(reader.GetOrdinal("landline_phone")) ? string.Empty : reader.GetString("landline_phone");
                        record.email = reader.IsDBNull(reader.GetOrdinal("email")) ? string.Empty : reader.GetString("email");
                        record.fk_supervisor_person = reader.IsDBNull(reader.GetOrdinal("supervisor")) ? string.Empty : reader.GetString("supervisor");

                        contact_infos.Add(record);
                    }
                }
            }
            DGcontact_info.ItemsSource = contact_infos;
        }

        private void DGcontact_info_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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

        private void DGcontact_info_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }

        private void DGcontact_info_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            Contact_info si = (Contact_info)DGcontact_info.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            //ew.frameM.Navigate(new Contact_info_edit_page(si.id_contact_info, si.fk_person, si.fk_department_name, si.fk_position_name, si.mobile_phone, si.landline_phone, si.email, si.fk_supervisor_person));
        }

        private void Delete_Click()
        {
            Contact_info si = (Contact_info)DGcontact_info.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_contact_info + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from contact_info where id_contact_info = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_contact_info);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Contact info с id " + si.id_contact_info + " удалено");
            }
        }
    }
}
