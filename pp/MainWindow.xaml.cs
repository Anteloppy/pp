using pp.add_wins;
using pp.edit_wins;
using pp.entities;
using pp.wins;
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

namespace pp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string[] lb_values = new string[15]
        {
            "Персональная карточка сотрудника",
            "Список структурных подразделений", 
            "Список должностей",
            "Структура",
            "Справочник графиков работы",
            "Контакт сотрудника",
            "Адреса",
            "Справочник регионов",
            "Справочник районов",
            "Справочник городов",
            "Справочник улиц",
            "Справочник курсов повышения квалификации",
            "Журнал прохождения курсов повышения квалификации",
            "Кадровый резерв",
            "Справочник банков"
        };
        public MainWindow()
        {
            InitializeComponent();
            LBOption.ItemsSource = lb_values;
        }

        private void LBOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var si = LBOption.SelectedIndex;
            switch (si) {
                case 0: Mframe.Navigate(new Personal_card_page()); Title = "Персональные карты сотрудников"; break;
                case 1: Mframe.Navigate(new Department_page()); Title = "Список структурных подразделений"; break;
                case 2: Mframe.Navigate(new Position_page()); Title = "Список должностей"; break;
                case 3: Mframe.Navigate(new Structure_page()); Title = "Структура"; break;
                case 4: Mframe.Navigate(new Work_schedule_page()); Title = "Справочник графиков работы"; break;
                case 5: Mframe.Navigate(new Contact_info_page()); Title = "Контакт сотрудника"; break;
                case 6: Mframe.Navigate(new Address_page()); Title = "Адреса"; break;
                case 7: Mframe.Navigate(new Region_page()); Title = "Справочник регионов"; break;
                case 8: Mframe.Navigate(new District_page()); Title = "Справочник районов"; break;
                case 9: Mframe.Navigate(new City_page()); Title = "Справочник городов"; break;
                case 10: Mframe.Navigate(new Street_page()); Title = "Справочник улиц"; break;
                case 11: Mframe.Navigate(new Course_page()); Title = "Справочник курсов повышения квалификации"; break;
                case 12: Mframe.Navigate(new Course_journal_page()); Title = "Журнал прохождения курсов повышения квалификации"; break;
                case 13: Mframe.Navigate(new Personnel_reserve_page()); Title = "Кадровый резерв"; break;
                case 14: Mframe.Navigate(new Bank_page()); Title = "Справочник банков"; break;
            }
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            AddWindow aw = new AddWindow();
            var si = LBOption.SelectedIndex;
            switch (si)
            {
                case 0: aw.Show(); aw.frameM.Navigate(new Personal_card_add_page()); break;
                case 1: aw.Show(); aw.frameM.Navigate(new Department_add_page()); break;
                case 2: aw.Show(); aw.frameM.Navigate(new Position_add_page()); break;
                //case 3: aw.Show(); aw.frameM.Navigate(new Structure_add_page()); break;
                case 4: aw.Show(); aw.frameM.Navigate(new Work_schedule_add_page()); break;
                //case 5: aw.Show(); aw.frameM.Navigate(new Contact_info_add_page()); break;
                //case 6: aw.Show(); aw.frameM.Navigate(new Address_add_page()); break;
                case 7: aw.Show(); aw.frameM.Navigate(new Region_add_page()); break;
                //case 8: aw.Show(); aw.frameM.Navigate(new District_add_page()); break;
                //case 9: aw.Show(); aw.frameM.Navigate(new City_add_page()); break;
                case 10: aw.Show(); aw.frameM.Navigate(new Street_add_page()); break;
                case 11: aw.Show(); aw.frameM.Navigate(new Course_add_page()); break;
                //case 12: aw.Show(); aw.frameM.Navigate(new Course_journal_add_page()); break;
                //case 13: aw.Show(); aw.frameM.Navigate(new Personnel_reserve_add_page()); break;
                case 14: aw.Show(); aw.frameM.Navigate(new Bank_add_page()); break;
            }
        }
    }
}
