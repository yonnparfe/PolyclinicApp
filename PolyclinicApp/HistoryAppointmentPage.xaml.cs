using PolyclinicApp.ApplicationData;
using System;
using System.Data.Entity;
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
    /// Логика взаимодействия для HistoryAppointmentPage.xaml
    /// </summary>
    public partial class HistoryAppointmentPage : Page
    {
        public HistoryAppointmentPage()
        {
            InitializeComponent();
            LoadTicket();
        }

        private void LoadTicket()
        {
            var tickets = AppConnect.modelOdb.Tickets.Include(t => t.Medical_Cards).Include(t => t.Doctors).ToList();
            TicketDataGrid.ItemsSource = tickets;

        }
    }


}
