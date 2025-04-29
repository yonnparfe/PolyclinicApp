using MaterialDesignThemes.Wpf;
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
    public partial class PatientRegistrationPage : Page
    {
        public PatientRegistrationPage()
        {
            InitializeComponent();
        }

        private void RegisterPatient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newMedicalCard = new Medical_Cards
                {
                    First_Name = FirstName.Text,
                    Last_Name = LastName.Text,
                    Patronymic = MiddleName.Text,
                    Date_Birth = BirthDate.SelectedDate.HasValue ? BirthDate.SelectedDate.Value : DateTime.MinValue,
                    Gender = (Gender.SelectedItem as ComboBoxItem)?.Content.ToString(),
                    Street = Street.Text,
                    House = int.Parse(HouseNumber.Text),
                    Entrance = int.Parse(Entrance.Text),
                    Flat = int.Parse(Apartment.Text),
                    Home_Phone_Number = HomePhone.Text,
                    Mobile_Phone = MobilePhone.Text,
                    Work_Phone = WorkPhone.Text,
                    Place_Work = WorkPlace.Text,
                    Work_Address = WorkAddress.Text,
                    Position = Position.Text,
                    Note = Notes.Text
                };


                AppConnect.modelOdb.Medical_Cards.Add(newMedicalCard);
                AppConnect.modelOdb.SaveChanges();

                MessageBox.Show("Данные успешно добавлены", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
    
}
