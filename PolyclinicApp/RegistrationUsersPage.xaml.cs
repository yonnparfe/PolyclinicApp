using PolyclinicApp.ApplicationData;
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

namespace PolyclinicApp
{
    /// <summary>
    /// Логика взаимодействия для RegistrationUsersPage.xaml
    /// </summary>
    public partial class RegistrationUsersPage : Page
    {
        public RegistrationUsersPage()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordBox.Password) || RoleComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var existingUser = AppConnect.modelOdb.Users.FirstOrDefault(x => x.Login == LoginTextBox.Text);
                if (existingUser != null)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string roleName = (RoleComboBox.SelectedItem as ComboBoxItem).Content.ToString();
                var existingRole = AppConnect.modelOdb.Roles.FirstOrDefault(x => x.NameRole == roleName);

                if (existingRole == null)
                {
                    // Добавить новую роль в базу данных
                    existingRole = new Roles() { NameRole = roleName };
                    AppConnect.modelOdb.Roles.Add(existingRole);
                    AppConnect.modelOdb.SaveChanges();
                }

                var newUser = new Users()
                {
                    Login = LoginTextBox.Text,
                    Password = PasswordBox.Password,
                    Role_Id = existingRole.Id
                };

                AppConnect.modelOdb.Users.Add(newUser);
                AppConnect.modelOdb.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
