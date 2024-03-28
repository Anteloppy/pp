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
        public string[] lb_values = new string[15] { "Персональная карточка сотрудника", "Список структурных подразделений", "Список должностей", "Структура", "Справочник графиков работы", "Контакт сотрудника", "Адреса", "Справочник регионов", "Справочник районов", "Справочник городов", "Справочник улиц", "Справочник курсов повышения квалификации", "Журнал прохождения курсов повышения квалификации", "Кадровый резерв", "Справочник банков" };
        public MainWindow()
        {
            InitializeComponent();
            LBOption.ItemsSource = lb_values;
        }

        private void LBOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
