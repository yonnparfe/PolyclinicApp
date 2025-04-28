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
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string roleName = (UserRole.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (string.IsNullOrEmpty(roleName))
                {
                    MessageBox.Show("Пожалуйста, выберите роль", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var existingRole = AppConnect.modelOdb.Roles
                    .FirstOrDefault(r => r.NameRole.Equals(roleName, StringComparison.OrdinalIgnoreCase));

                if (existingRole == null)
                {
                    MessageBox.Show("Выбранная роль не существует", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var newUser = new Users
                {
                    Login = Username.Text,
                    Password = Password.Password,
                    Role_Id = existingRole.Id,
                };

                AppConnect.modelOdb.Users.Add(newUser);
                AppConnect.modelOdb.SaveChanges();

                MessageBox.Show("Данные успешно добавлены", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }



            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            

        }
    }
}
