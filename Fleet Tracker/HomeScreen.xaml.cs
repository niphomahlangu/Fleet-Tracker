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

namespace Fleet_Tracker
{
    /// <summary>
    /// Interaction logic for HomeScreen.xaml
    /// </summary>
    public partial class HomeScreen : Page
    {
        public HomeScreen()
        {
            InitializeComponent();
        }
        //opens the vehicle information manager
        private void btnVehManager_Click(object sender, RoutedEventArgs e)
        {
            //navigate to vehicle information manager page
            this.NavigationService.Navigate(new VehicleManager());
        }
        //opens the vehicle services manager
        private void btnVehServ_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new VehicleServices());
        }
        //opens the trip and resources manager
        private void btnTripManager_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TripManager());
        }
        //opens the timesheet manager
        private void btnTSheets_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Timesheets());
        }
    }
}
