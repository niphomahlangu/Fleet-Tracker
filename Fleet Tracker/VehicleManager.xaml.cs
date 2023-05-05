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
using System.Data;
using System.Data.SqlClient;

namespace Fleet_Tracker
{
    /// <summary>
    /// Interaction logic for VehicleManager.xaml
    /// </summary>
    public partial class VehicleManager : Page
    {
        //connection object
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FTS_DB.mdf;Integrated Security=True");
        //holds the vehicle ID
        public string VehicleNum;
        //date of service
        public string RegistrationNum;
        //type of vehicle
        public string VehicleType;
        //vehicle manufacturer
        public string Manufacturer;
        //size of engine
        public string EngineSize;
        //odometer reading
        public int OdmReading;

        public VehicleManager()
        {
            InitializeComponent();
            //populate vehicle type combo box
            vechTypeCmb();
            //populate manufacturer combo box
            manufacturerCmb();
            //populate engine size combo box
            engineSizeCmb();
        }

        private void engineSizeCmb()
        {
            string[] sizes = { "1000kg", "1500kg", "2000kg" };
            for(int i=0; i<sizes.Length; ++i)
            {
                cmbEngineSize.Items.Add(sizes[i]);
            }
        }

        private void manufacturerCmb()
        {
            string[] manufactuers = { "MAN", "VOLVO", "MERCEDES", "Scania", "DAF" };
            for (int i = 0; i < manufactuers.Length; ++i)
            {
                cmbManufacturer.Items.Add(manufactuers[i]);
            }
        }

        private void vechTypeCmb()
        {
            string[] vechtTypes = { "Tractor-trailer", "Semi-trailer", "Flat bed","Refridgerator","Log carrier","Car carrier" };
            for (int i = 0; i < vechtTypes.Length; ++i)
            {
                cmbVechType.Items.Add(vechtTypes[i]);
            }
        }

        //event to return to home screen
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomeScreen());
        }
        //add content to a file or database
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            VehicleNum = txtVehicleNum.Text;
            RegistrationNum = txtRegNum.Text;
            VehicleType = cmbVechType.Items[cmbVechType.SelectedIndex].ToString();
            Manufacturer = cmbManufacturer.Items[cmbManufacturer.SelectedIndex].ToString();
            EngineSize = cmbEngineSize.Items[cmbEngineSize.SelectedIndex].ToString();
            OdmReading = int.Parse(txtOdReading.Text);
            //store vehicle data into the database
            storeVehicleData();
            
        }

        private void storeVehicleData()
        {
            try
            {
                //open the connection object
                connection.Open();
                //sql command string
                string commandString = "INSERT INTO tblVehicles (VehicleId, RegistrationNum, VehicleType, Manufacturer, EngineSize, OdometerReading)" +
                    "VALUES('"+VehicleNum+ "', '"+RegistrationNum+"', '"+VehicleType+"', '"+Manufacturer+"', '"+EngineSize+"','"+OdmReading+"')";
                //command object
                SqlCommand insertCmd = new SqlCommand(commandString, connection);
                //execute the command
                int rowCount = insertCmd.ExecuteNonQuery();
                //check if a row is affected
                if(rowCount == 1)
                {
                    MessageBox.Show("Vehicle Data successfully added.","Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close the connection object
                connection.Close();
            }
        }

        //display content from exiting file or database
        private void btnViewVech_Click(object sender, RoutedEventArgs e)
        {
            //select data from database
            getVehicleData();
        }

        private void getVehicleData()
        {
            try
            {
                //open connection object
                connection.Open();
                string query = "SELECT * FROM tblVehicles";
                //select command object
                SqlCommand selectCmd = new SqlCommand(query, connection);
                //data table
                DataTable table = new DataTable();
                //load query into 
                table.Load(selectCmd.ExecuteReader());
                //add items to grid
                dtGrid.DataContext = table;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //close connection object
                connection.Close();
            }
            
        }

    }
}
