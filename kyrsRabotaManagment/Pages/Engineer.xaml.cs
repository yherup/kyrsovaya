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

namespace kyrsRabotaManagment.Pages
{
    /// <summary>
    /// Логика взаимодействия для Engineer.xaml
    /// </summary>
    public partial class Engineer : Page
    {
        int _current_user;


        managmentEntities1 db;
        public Engineer(employees Employees)
        {
            InitializeComponent();
            db = new managmentEntities1();
            var MainTable = db.maintable.Where(item => item.id_employee == Employees.id_employee).ToList();
            for (int i = 0; i < MainTable.Count(); i++)
            {
                var Orders = db.orders.ToList().Where(item => item.id_order == MainTable[i].id_order).ToList();
                if(!MainTable[i].done)
                    orders.Items.Add(Orders[0]);
            }
            _current_user = Employees.id_employee;
            string str = "Инженер " + Employees.surname_employee + " " + Employees.name_employee;
            this.Title = str;
        }

        private void Success_btn_Click(object sender, RoutedEventArgs e)
        {
            var ord = (orders)orders.SelectedItem;
            var mtb = db.maintable.Where(item => item.id_order == ord.id_order).First().done = true;
            db.SaveChanges();
            MessageBox.Show("Заказ выполнин");
            orders.Items.Clear();

            var MainTable = db.maintable.Where(item => item.id_employee == _current_user).ToList();
            for (int i = 0; i < MainTable.Count(); i++)
            {
                var Orders = db.orders.ToList().Where(item => item.id_order == MainTable[i].id_order).ToList();
                if (!MainTable[i].done)
                    orders.Items.Add(Orders[0]);
            }
        }

        private void Exit_btn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
        }
    }
}
