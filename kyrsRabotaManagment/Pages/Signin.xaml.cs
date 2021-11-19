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
    /// Логика взаимодействия для Signin.xaml
    /// </summary>
    public partial class Signin : Page
    {
        managmentEntities1 db;
        public Signin()
        {
            InitializeComponent();
           db= new managmentEntities1();
        }

        private void Signin_Click(object sender, RoutedEventArgs e)
        {
            if (login.Text == "" || password.Password == "")
            {
                MessageBox.Show("Ошибка: пустые поля");
                return;
            }
            if (db.authorization.Select(item => item.login_account + " " + item.password_account).Contains(login.Text + " " + password.Password))
            {
                var NewAccount = db.authorization.Where(item => item.login_account == login.Text).FirstOrDefault();
                employees Employees = db.employees.Where(item => item.id_account==NewAccount.id_account).FirstOrDefault();
                MessageBox.Show("Вы авторизованы");
               if(Employees.truemanager_employee)
                    NavigationService.Navigate(new Manager(Employees));
                else NavigationService.Navigate(new Engineer(Employees));

            }
            else
            {
                MessageBox.Show("Ошибка логина/парола");
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Registration re = new Registration();
            re.Show();
            Application.Current.Windows[0].Close();
        }
    }
}
