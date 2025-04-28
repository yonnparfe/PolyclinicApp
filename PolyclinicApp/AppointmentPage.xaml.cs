using PolyclinicApp.ApplicationData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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
using System.Runtime.Remoting.Contexts;

namespace PolyclinicApp
{
    /// <summary>
    /// Логика взаимодействия для AppointmentPage.xaml
    /// </summary>
    public partial class AppointmentPage : Page
    {
        public AppointmentPage()
        {
            InitializeComponent();
            LoadTicket();

        }

        private void LoadTicket()
        {
            var tickets = AppConnect.modelOdb.Tickets.Include(t => t.Medical_Cards).Include(t => t.Doctors).ToList();

            var doctorNames = new List<string>();
            var patientNames = new List<string>();

            foreach (var ticket in tickets)
            {
                if (ticket.Doctors != null)
                {
                    string doctorFullName = $"{ticket.Doctors.First_Name} {ticket.Doctors.Last_Name}";
                    doctorNames.Add(doctorFullName);
                }

                if (ticket.Medical_Cards != null && ticket.Medical_Cards != null)
                {
                    string patientFullName = $"{ticket.Medical_Cards.First_Name} {ticket.Medical_Cards.Last_Name}";
                    patientNames.Add(patientFullName);
                }
            }

            DoctorComboBox.ItemsSource = doctorNames;
            PatientComboBox.ItemsSource = patientNames;
        }

        private void SubmitAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var appointmentDate = AppointmentDate.SelectedDate;

                var hour = HourComboBox.SelectedItem as ComboBoxItem;
                var minute = MinuteComboBox.SelectedItem as ComboBoxItem;

                var appointmentTime = new DateTime(appointmentDate.Value.Year, appointmentDate.Value.Month, appointmentDate.Value.Day,
                                                    int.Parse(hour.Content.ToString()), int.Parse(minute.Content.ToString()), 0);

                string selectedDoctor = (DoctorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                var existingDoctor = AppConnect.modelOdb.Doctors.FirstOrDefault(d =>
                    d.First_Name + " " + d.Last_Name + " " + d.Patronymic == selectedDoctor);

                string selectedPatient = (PatientComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                var existingPatient = AppConnect.modelOdb.Medical_Cards.FirstOrDefault(p =>
                    p.First_Name + " " + p.Last_Name + " " + p.Patronymic == selectedPatient);

                if (existingDoctor == null || existingPatient == null)
                {
                    MessageBox.Show("Выберите корректного врача и пациента.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }


                var newTicket = new Tickets
                {
                    Doctor_Id = existingDoctor.Id,
                    Med_Card_Id = existingPatient.Id,
                    Cabinet_Number = int.Parse(Room.Text),
                    Date_Time_Admission = appointmentTime
                };

                AppConnect.modelOdb.Tickets.Add(newTicket);
                AppConnect.modelOdb.SaveChanges();

                MessageBox.Show("Вы успешно записались на прием!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void History_Click(object sender, RoutedEventArgs e)
        {
            Frame mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
            if (mainFrame != null)
            {
                mainFrame.Navigate(new HistoryAppointmentPage());
            }

            
        }
    }
}
