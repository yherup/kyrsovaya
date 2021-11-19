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
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    ///
    public partial class Manager : Page
    {

        int _current_user;

        managmentEntities1 db;
        public Manager(employees Employees)
        {
            _current_user = Employees.id_employee;
            InitializeComponent();
            db = new managmentEntities1();
            engineers.ItemsSource = db.employees.Where(item => item.truemanager_employee == false).ToList();
            orders.ItemsSource = db.orders.Where(item =>item.inprocess_order==false).ToList();
            string str = "Менеджер " + Employees.surname_employee + " " + Employees.name_employee;
            this.Title = str;
        }

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            if (engineers.SelectedItem == null || orders.SelectedItem == null)
            {
                MessageBox.Show("Выберите инженера");
                return;
            }

            var employee = (employees)engineers.SelectedItem;
            var order = (orders)orders.SelectedItem;

            maintable newMaintable = new maintable()
            {
                id_order = order.id_order,
                id_employee = employee.id_employee,
                done = false
            };

            db.orders.Find(order.id_order).inprocess_order = true;

            db.maintable.Add(newMaintable);
            db.SaveChanges();
            MessageBox.Show("Заказ обработан");
            orders.ItemsSource = db.orders.Where(item => item.inprocess_order == false).ToList();
        }

        private void orders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (engineers.ItemsSource == null)
                engineers.Items.Clear();
            else engineers.ItemsSource=null;

            var order = (orders)orders.SelectedItem;

            if (order == null)
            {
                engineers.ItemsSource = db.employees.Where(item => item.truemanager_employee == false).ToList();
                orders.ItemsSource = db.orders.Where(item => item.inprocess_order == false).ToList();
                return;
            }

            var engin = db.employees.Where(item => item.truemanager_employee == false).ToList();
            try
            {
                foreach (employees item in engin)
                {
                    int hour = hoursCalculate(item);
                    if (order.adress_order.Split(',')[0] == item.adress_employee.Split(',')[0] && hour<3)
                        engineers.Items.Add(item);
                }

                var count = engineers.Items.Count;

                if (count == 0)
                {
                    foreach (employees item in engin)
                    {
                        int hour = hoursCalculate(item);
                            if ( hour < 3)
                                engineers.Items.Add(item);
                    }
                    foreach (employees item in engin)
                    {
                        int hour = hoursCalculate(item);
                        if (hour > 3)
                            engineers.Items.Add(item);
                    }
                }
                else
                {
                    foreach (employees item in engin)
                    {
                        int hour = hoursCalculate(item);
                        for (int i = 0; i <= count - 1; i++)
                            if ((employees)engineers.Items[i] != item && hour < 3)
                                engineers.Items.Add(item);
                    }
                    foreach (employees item in engin)
                    {
                        int hour = hoursCalculate(item);
                        if (hour > 3)
                            engineers.Items.Add(item);
                    }
                }
            }
            catch (Exception exp)
            {
                
            }

        }

        private int hoursCalculate(employees Emp)
        {
            int _hours = 0;
            var MainTable = db.maintable.Where(item=>item.id_employee==Emp.id_employee).ToList();

            for (int i = 0; i < MainTable.Count(); i++)
            {
                var Orders = db.orders.ToList().Where(item => item.id_order == MainTable[i].id_order).ToList();
                if(!MainTable[i].done)
                    _hours += Orders[0].time_order;
            }

            return _hours;
        }

        private void Static_btn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Manager2(db.employees.Find(_current_user)));
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
        }
    }
}
