using MySql.Data.MySqlClient;
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

namespace pp.edit_wins
{
    /// <summary>
    /// Логика взаимодействия для Work_schedule_edit_page.xaml
    /// </summary>
    public partial class Work_schedule_edit_page : Page
    {
        public Work_schedule_edit_page()
        {
            InitializeComponent();
        }
        public Work_schedule_edit_page(int id, string name, int working_days, int days_off, int working_hours_per_day)
        {
            InitializeComponent();
            TBid.Text = Convert.ToString(id);
            TBname.Text = name;
            TBworking_days.Text = working_days.ToString();
            TBdays_of.Text = days_off.ToString();
            TBworking_hours_per_day.Text = working_hours_per_day.ToString();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            string edit = "";
            if (string.IsNullOrEmpty(TBname.Text) || string.IsNullOrEmpty(TBworking_days.Text) || string.IsNullOrEmpty(TBdays_of.Text) || string.IsNullOrEmpty(TBworking_hours_per_day.Text))
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите продолжить? Есть пустые поля.", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    edit = "update personal_cards set ";
                    if (!string.IsNullOrEmpty(TBname.Text))
                        edit += "schedule_name = '" + TBname.Text + "', ";
                    else
                        edit += "schedule_name = null, ";
                    if (!string.IsNullOrEmpty(TBworking_days.Text))
                        edit += "working_days = '" + Convert.ToInt32(TBworking_days.Text) + "', ";
                    else
                        edit += "working_days = null, ";
                    if (!string.IsNullOrEmpty(TBdays_of.Text))
                        edit += "days_off = '" + Convert.ToInt32(TBdays_of.Text) + "', ";
                    else
                        edit += "days_off = null, ";
                    if (!string.IsNullOrEmpty(TBworking_hours_per_day.Text))
                        edit += "working_hours_per_day = '" + Convert.ToInt32(TBworking_hours_per_day.Text) + "', ";
                    else
                        edit += "working_hours_per_day =  null, ";
                    edit += "where id_work_schedule = @id; commit;";
                }
                else return;
            }
            else
            edit = "update work_schedules set schedule_name = '" + TBname.Text + "', working_days = " + Convert.ToInt32(TBworking_days.Text) + ", days_off = " + Convert.ToInt32(TBdays_of.Text) + ", working_hours_per_day = " + Convert.ToInt32(TBworking_hours_per_day.Text) + " where id_department = @id; commit;";
            int id = Convert.ToInt32(TBid.Text);
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(edit, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Work schedule с id " + id + " изменено");
        }
    }
}
