using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace kyrsRabotaManagment.Pages
{
    /// <summary>
    /// Логика взаимодействия для Manager2.xaml
    /// </summary>
    public partial class Manager2 : Page
    {

        public class Maintablelist
        {
            public string Name_order { get; set; }
            public string Surname { get; set; }
            public string Name { get; set; }
            public string Flag { get; set; }
            public Maintablelist(string name_order, string surName, string name, string flag)
            {
                this.Name_order = name_order;
                this.Surname = surName;
                this.Name = name;
                this.Flag = flag;
            }
        }

        managmentEntities1 db = new managmentEntities1();
        public Manager2(employees Employees)
        {
            InitializeComponent();
            string str = "Менеджер " + Employees.surname_employee + " " + Employees.name_employee;
            this.Title = str;
            var mtb = db.maintable.ToList();
            foreach(maintable item in mtb)
            {
                int emp_id = item.id_employee;
                int ord_id = item.id_order;
                var emp = db.employees.Find(emp_id);
                var ord = db.orders.Find(ord_id);

                Maintablelist list = new Maintablelist( ord.name_order, emp.surname_employee, emp.name_employee, item.done? "Выполнено": "Невыполнено");

                maintables.Items.Add(list);
            }

        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
