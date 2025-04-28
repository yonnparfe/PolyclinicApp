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
    /// Логика взаимодействия для MedicalRecordsPage.xaml
    /// </summary>
    public partial class MedicalCardsPage : Page
    {
        public MedicalCardsPage()
        {
            InitializeComponent();
            LoadMedCard();
        }


        private void LoadMedCard()
        {
            MedicalCardsDataGrid.ItemsSource = AppConnect.modelOdb.Medical_Cards.ToList();

        }

        private void OpenMedCardClick(object sender, RoutedEventArgs e)
        {
            Frame mainFrame = Application.Current.MainWindow.FindName("MainFrame") as Frame;
            if (mainFrame != null)
            {
                mainFrame.Navigate(new PatientMedCardPage());
            }
        }
    }
}
