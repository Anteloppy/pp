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

namespace pp.add_wins
{
    /// <summary>
    /// Логика взаимодействия для Work_schedule_add_page.xaml
    /// </summary>
    public partial class Work_schedule_add_page : Page
    {
        public Work_schedule_add_page()
        {
            InitializeComponent();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            string add = "";
            if (string.IsNullOrEmpty(TBname.Text) || string.IsNullOrEmpty(TBworking_days.Text) || string.IsNullOrEmpty(TBdays_of.Text) || string.IsNullOrEmpty(TBworking_hours_per_day.Text))
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите продолжить? Есть пустые поля.", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    string start = "", end = "";
                    start = "insert into personal_cards (";
                    if (!string.IsNullOrEmpty(TBname.Text))
                    {
                        start += "name, ";
                        end += "'" + TBname.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBworking_days.Text))
                    {
                        start += "working_days, ";
                        end += "'" + TBworking_days.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBdays_of.Text))
                    {
                        start += "days_of, ";
                        end += "'" + TBdays_of.Text + "', ";
                    }
                    if (!string.IsNullOrEmpty(TBworking_hours_per_day.Text))
                    {
                        start += "working_hours_per_day, ";
                        end += "'" + TBworking_hours_per_day.Text + "', ";
                    }
                    start = start.Remove(start.Length - 2, 2);
                    end = end.Remove(end.Length - 2, 2);
                    add += start + ") values (" + end + ") ; commit;";
                }
                else return;
            }
            else
            add = "insert into work_schedules(schedule_name, working_days, days_of, working_hours_per_day) values('" + TBname.Text + "'," + Convert.ToInt32(TBworking_days.Text) + ", " + Convert.ToInt32(TBdays_of.Text) + ", " + Convert.ToInt32(TBworking_hours_per_day.Text) + "); commit;";
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(add, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Work schedule добавлено.");
        }
    }
}
