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
    /// Логика взаимодействия для Course_edit_page.xaml
    /// </summary>
    public partial class Course_edit_page : Page
    {
        public Course_edit_page()
        {
            InitializeComponent();
        }
        public Course_edit_page(int id, string name)
        {
            InitializeComponent();
            TBid.Text = Convert.ToString(id);
            TBname.Text = name;
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            string edit = "";
            if (string.IsNullOrEmpty(TBname.Text))
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите продолжить? Есть пустые поля.", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    edit = "update qualification_courses set course_name = null where id_course = @id; commit;";
                }
                else return;
            }
            else
            edit = "update qualification_courses set course_name = '" + TBname.Text + "' where id_course = @id; commit;";
            int id = Convert.ToInt32(TBid.Text);
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(edit, conn);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Course с id " + id + " изменено");
        }
    }
}
