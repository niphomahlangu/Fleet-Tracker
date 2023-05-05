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

namespace Fleet_Tracker
{
    /// <summary>
    /// Interaction logic for ServiceUpdate.xaml
    /// </summary>
    public partial class ServiceUpdate : Window
    {
        //property for vehicle Id
        public string VehicleId { get; set; }
        public ServiceUpdate()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            VehicleId = txtVehicleId.Text;
            //close window
            this.Close();
        }
    }
}
