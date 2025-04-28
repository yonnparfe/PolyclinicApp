using MaterialDesignColors;
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
    
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            AppConnect.modelOdb = new PolyclinicEntities();


        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LoginTextBox.Text) || string.IsNullOrEmpty(PasswordBox.Password))
                {
                    MessageBox.Show("Пожалуйста, заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                var userObj = AppConnect.modelOdb.Users.FirstOrDefault(x => x.Login == LoginTextBox.Text && x.Password == PasswordBox.Password);
                if (userObj == null)
                {
                    MessageBox.Show("Такого пользователя нет!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Application.Current.MainWindow = new MainWindow();
                    //MainWindow mainWindow = new MainWindow();
                    //mainWindow.Show();
                    Application.Current.MainWindow.Show();
                    this.Close(); 
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ошибка" + ex.Message.ToString(), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void RegistrationButton_Click(object sender, RoutedEventArgs e)
        {
            LoginFrame.Navigate(new RegistrationUsersPage());
        }

       

        
    }
}
