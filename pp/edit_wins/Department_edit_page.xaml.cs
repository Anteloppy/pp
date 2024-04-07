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

namespace pp.edit_wins
{
    /// <summary>
    /// Логика взаимодействия для Department_edit_page.xaml
    /// </summary>
    public partial class Department_edit_page : Page
    {
        public Department_edit_page()
        {
            InitializeComponent();
        }
        public Department_edit_page(int id_department, string name)
        {
            InitializeComponent();
            TBid_department.Text = Convert.ToString(id_department);
            TBname.Text = name;
        }

        private void BEdit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("edited");
        }
    }
}
