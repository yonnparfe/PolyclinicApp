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

namespace PolyclinicApp
{
    /// <summary>
    /// Логика взаимодействия для SchedulePage.xaml
    /// </summary>
    public partial class SchedulePage : Page
    {
        public SchedulePage()
        {
            InitializeComponent();
            LoadSchedule();
        }

        private void LoadSchedule()
        {
            var schedule = AppConnect.modelOdb.Work_Schedule.Include(t => t.Doctors).Include(t => t.Doctors.Specializations).ToList();

            DoctorScheduleDataGrid.ItemsSource = schedule;

        }
    }
}
