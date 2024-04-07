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
                case 0: Mframe.Navigate(new Personal_cards_page()); Title = "Персональные карты сотрудников"; break;
                case 1: Mframe.Navigate(new Department_page()); Title = "Список структурных подразделений"; break;
                case 2: Title = "Список должностей"; break;
                case 3: Title = "Структура"; break;
                case 4: Title = "Справочник графиков работы"; break;
                case 5: Title = "Контакт сотрудника"; break;
                case 6: Title = "Адреса"; break;
                case 7: Title = "Справочник регионов"; break;
                case 8: Title = "Справочник районов"; break;
                case 9: Title = "Справочник городов"; break;
                case 10: Title = "Справочник улиц"; break;
                case 11: Title = "Справочник курсов повышения квалификации"; break;
                case 12: Title = "Журнал прохождения курсов повышения квалификации"; break;
                case 13: Title = "Кадровый резерв"; break;
                case 14: Title = "Справочник банков"; break;
            }
        }
    }
}
