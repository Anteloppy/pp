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
    /// Логика взаимодействия для Course_journal.xaml
    /// </summary>
    public partial class Course_journal_page : Page
    {
        public Course_journal_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void Course_journal_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<Course_journal> course_journals = new List<Course_journal>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select j.id_course_journal as id, c.course_name as course, concat(e.last_name, ' ', e.name, ' ', e.surname) as person, j.start_date, j.end_date, j.result, j.status, j.notes, j.certificate_number, j.certificate_date from qualification_course_journal as j left join qualification_courses as c on j.course_id = c.id_course left join personal_cards as e on j.person_id = e.id_person", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Course_journal record = new Course_journal();
                        record.id_course_journal = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.fk_course_name = reader.IsDBNull(reader.GetOrdinal("course")) ? string.Empty : reader.GetString("course");
                        record.fk_person = reader.IsDBNull(reader.GetOrdinal("person")) ? string.Empty : reader.GetString("person");
                        record.start_date = reader.IsDBNull(reader.GetOrdinal("start_date")) ? string.Empty : reader.GetDateTime("start_date").ToString("yyyy-MM-dd");
                        record.end_date = reader.IsDBNull(reader.GetOrdinal("end_date")) ? string.Empty : reader.GetDateTime("end_date").ToString("yyyy-MM-dd");
                        record.result = reader.IsDBNull(reader.GetOrdinal("result")) ? string.Empty : reader.GetString("result");
                        record.status = reader.IsDBNull(reader.GetOrdinal("status")) ? string.Empty : reader.GetString("status");
                        record.notes = reader.IsDBNull(reader.GetOrdinal("notes")) ? string.Empty : reader.GetString("notes");
                        record.certificate_number = reader.IsDBNull(reader.GetOrdinal("certificate_number")) ? string.Empty : reader.GetString("certificate_number");
                        record.certificate_date = reader.IsDBNull(reader.GetOrdinal("certificate_date")) ? string.Empty : reader.GetDateTime("certificate_date").ToString("yyyy-MM-dd");

                        course_journals.Add(record);
                    }
                }
            }
            DGcourse_journal.ItemsSource = course_journals;
        }
        private void DGcourse_journal_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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
        private void DGcourse_journal_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }
        private void DGcourse_journal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            Course_journal si = (Course_journal)DGcourse_journal.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            //ew.frameM.Navigate(new Course_journal_edit_page(si.id_course_journal, si.fk_course_name, si.fk_person, si.start_date, si.end_date, si.result, si.status, si.notes, si.certificate_number, si.certificate_date));
        }
        private void Delete_Click()
        {
            Course_journal si = (Course_journal)DGcourse_journal.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_course_journal + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from course_journal where id_course_journal = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_course_journal);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Course journal с id " + si.id_course_journal + " удалено");
            }
        }
    }
}
