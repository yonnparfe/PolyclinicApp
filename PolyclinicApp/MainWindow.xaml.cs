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
using System.Windows.Shapes;

namespace PolyclinicApp
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppConnect.modelOdb = new PolyclinicEntities();
            MainFrame.Navigate(new MainPage());
        }

        private void btnRegisterPatientsClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new PatientRegistrationPage());
        }

        private void btnMedicalCardsClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MedicalCardsPage());
        }

        private void btnAppointmentClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AppointmentPage());
        }

        private void btnSchedulesClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SchedulePage());
        }

        //private void btnTestsClick(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new MedicalTestsPage());
        //}

        //private void btnSettingsClick(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new SettingsPage());
        //}

      

    }
}
