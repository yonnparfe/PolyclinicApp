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
            LoadDoctors();
            LoadPatients();
        }

        private void LoadDoctors()
        {
            var doctors = AppConnect.modelOdb.Doctors.ToList();
            DoctorComboBox.ItemsSource = doctors.Select(d => new
            {
                Id = d.Id,
                FullName = $"{d.Last_Name} {d.First_Name} {d.Patronymic}"
            }).ToList();
            DoctorComboBox.DisplayMemberPath = "FullName";
            DoctorComboBox.SelectedValuePath = "Id";
        }

        private void LoadPatients()
        {
            var patients = AppConnect.modelOdb.Medical_Cards.ToList();
            PatientComboBox.ItemsSource = patients.Select(p => new
            {
                Id = p.Id,
                FullName = $"{p.Last_Name} {p.First_Name} {p.Patronymic}"
            }).ToList();
            PatientComboBox.DisplayMemberPath = "FullName";
            PatientComboBox.SelectedValuePath = "Id";
        }

        private void SubmitAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var appointmentDate = AppointmentDate.SelectedDate;

                if (!appointmentDate.HasValue || HourComboBox.SelectedItem == null || MinuteComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, убедитесь, что вы выбрали дату и время!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var hour = int.Parse((HourComboBox.SelectedItem as ComboBoxItem)?.Content.ToString());
                var minute = int.Parse((MinuteComboBox.SelectedItem as ComboBoxItem)?.Content.ToString());

                var appointmentTime = new DateTime(appointmentDate.Value.Year, appointmentDate.Value.Month, appointmentDate.Value.Day, hour, minute, 0);

                var selectedDoctorId = (int)(DoctorComboBox.SelectedValue ?? 0);
                var selectedPatientId = (int)(PatientComboBox.SelectedValue ?? 0);

                var existingDoctor = AppConnect.modelOdb.Doctors.Find(selectedDoctorId);
                var existingPatient = AppConnect.modelOdb.Medical_Cards.Find(selectedPatientId);

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
