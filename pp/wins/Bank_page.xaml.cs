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
    /// Логика взаимодействия для Bank_page.xaml
    /// </summary>
    public partial class Bank_page : Page
    {
        public Bank_page()
        {
            InitializeComponent();
            LoadData();
        }
        private void Bank_page_Activated(object sender, EventArgs e)
        {
            LoadData();
        }
        static string connectionString = "server=localhost; port=3306; database=employees_database; user=root; password=Nimda123;";
        private void LoadData()
        {
            List<Bank> banks = new List<Bank>();
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select id_bank as id, bank_name as name from banks", conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bank record = new Bank();
                        record.id_bank = reader.IsDBNull(reader.GetOrdinal("id")) ? 0 : reader.GetInt32("id");
                        record.bank_name = reader.IsDBNull(reader.GetOrdinal("name")) ? string.Empty : reader.GetString("name");

                        banks.Add(record);
                    }
                }
            }
            DGbank.ItemsSource = banks;
        }

        private void DGbank_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
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

        private void DGbank_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Delete_Click();
        }

        private void DGbank_MouseDoubleClick(object sender, MouseButtonEventArgs e)
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
            Personal_card si = (Personal_card)DGbank.SelectedItem;
            EditWindow ew = new EditWindow();
            ew.Show();
            //ew.frameM.Navigate(new Bank_edit_page(si.id_bank, si.bank_name));
        }

        private void Delete_Click()
        {
            Bank si = (Bank)DGbank.SelectedItem;
            MessageBoxResult result = MessageBox.Show("Удалить строку с id " + si.id_bank + "?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                string delete = "delete from banks where id_bank = @id; commit;";
                MySqlConnection conn = new MySqlConnection(connectionString);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(delete, conn);
                cmd.Parameters.AddWithValue("@id", si.id_bank);
                cmd.ExecuteNonQuery();
                conn.Close();
                MessageBox.Show("Bank с id " + si.id_bank + " удалено");
            }
        }
    }
}
