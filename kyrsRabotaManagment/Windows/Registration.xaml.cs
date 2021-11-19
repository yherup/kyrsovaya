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
using System.Windows.Shapes;

namespace kyrsRabotaManagment
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        managmentEntities1 db = new managmentEntities1();
        public Registration()
        {
            InitializeComponent();
        }

        private void RegistrationClick_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text == "" || password.Password == "" || lastName.Text == "" || Name.Text == "" || middleName.Text == "" || DatePicker.Text == "")
            {
                MessageBox.Show("Пустые поля");
                return;
            }
            if (db.authorization.Select(item => item.login_account).Contains(Login.Text))
            {
                MessageBox.Show("Такой логин существует в системе");
                return;
            }

            authorization newUser = new authorization()
            {
                login_account = Login.Text,
                password_account = password.Password,
              
            };

            db.authorization.Add(newUser);
            db.SaveChanges();
            var NewAccount = db.authorization.Where(item => item.login_account==Login.Text).FirstOrDefault();

            employees newEmployee = new employees()
            {
                name_employee = Name.Text,
                surname_employee = lastName.Text,
                middlename_employee = middleName.Text,
                birthday_employee = (DateTime)DatePicker.SelectedDate,
                truemanager_employee = (bool)Manager.IsChecked ? true : false,
                id_account = NewAccount.id_account,
                adress_employee = Adress.Text

            };
            db.employees.Add(newEmployee);
            db.SaveChanges();
            MessageBox.Show("Вы успешно зарегистрировались");
            Adress.Clear();
            Login.Clear();
            password.Clear();
            DatePicker.Text = null;
            Name.Clear();
            lastName.Clear();
            middleName.Clear();
            Engineer.IsChecked = false;
            Manager.IsChecked = false;


        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Authentication au = new Authentication();
            au.Show();
            this.Close();
        }

        private void Engineer_Checked(object sender, RoutedEventArgs e)
        {
                Adress.Visibility = Visibility.Visible;
                L_adress.Visibility = Visibility.Visible;
        }

        private void Manager_Checked(object sender, RoutedEventArgs e)
        {
                Adress.Visibility = Visibility.Hidden;
                L_adress.Visibility = Visibility.Hidden;

        }
    }
}
