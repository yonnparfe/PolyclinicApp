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
    /// Логика взаимодействия для PatientMedCardPage.xaml
    /// </summary>
    public partial class PatientMedCardPage : Page
    {
        public PatientMedCardPage(Medical_Cards patient)
        {
            InitializeComponent();
            
            DataContext = patient;
        }


        private void DeleteCard_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить карту?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                var selectedPatient = (Medical_Cards)DataContext;
                if (selectedPatient != null)
                {
                    AppConnect.modelOdb.Medical_Cards.Remove(selectedPatient);
                    AppConnect.modelOdb.SaveChanges();
                    MessageBox.Show("Карта успешно удалена!", "Title", MessageBoxButton.OK, MessageBoxImage.Information);

                    Frame mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
                    if (mainFrame != null)
                    {
                        mainFrame.Navigate(new MedicalCardsPage());
                    }
                }
                else
                {
                    MessageBox.Show("Пациент не выбран.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void SaveChangesPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var patientToUpdate = (Medical_Cards)DataContext;
                if (patientToUpdate != null)
                {
                    
                    patientToUpdate.First_Name = FirstName.Text;
                    patientToUpdate.Last_Name = LastName.Text;
                    patientToUpdate.Patronymic = MiddleName.Text;
                    patientToUpdate.Date_Birth = BirthDate.SelectedDate.HasValue ? BirthDate.SelectedDate.Value : DateTime.MinValue;
                    patientToUpdate.Gender = (Gender.SelectedItem as ComboBoxItem)?.Content.ToString();
                    patientToUpdate.Street = Street.Text;
                    patientToUpdate.House = int.Parse(HouseNumber.Text);
                    patientToUpdate.Entrance = int.Parse(Entrance.Text);
                    patientToUpdate.Flat = int.Parse(Apartment.Text);
                    patientToUpdate.Home_Phone_Number = HomePhone.Text;
                    patientToUpdate.Mobile_Phone = MobilePhone.Text;
                    patientToUpdate.Work_Phone = WorkPhone.Text;
                    patientToUpdate.Place_Work = WorkPlace.Text;
                    patientToUpdate.Work_Address = WorkAddress.Text;
                    patientToUpdate.Position = Position.Text;
                    patientToUpdate.Note = Notes.Text;

                    AppConnect.modelOdb.SaveChanges();

                    MessageBox.Show("Карта успешно обновлена!", "Caption", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Пациент не найден для редактирования.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
