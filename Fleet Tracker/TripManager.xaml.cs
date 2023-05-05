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
    /// Interaction logic for TripManager.xaml
    /// </summary>
    public partial class TripManager : Page
    {
        //connection object
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FTS_DB.mdf;Integrated Security=True");
        //field for trip destination
        public string Destination;
        //field for vehicle ID
        public string VehicleID;
        //field for the Date of the Trip
        public DateTime TripDate;
        //trip completion status
        public string TripStatus;
        public TripManager()
        {
            InitializeComponent();
            //populate the destination combo box
            destinationCmb();
        }

        private void destinationCmb()
        {
            string[] cities = {"Johannesburg", "Pretoria", "Durban", "Cape Town", "Bloemfontein", "Port Elizabeth", "Polokwane", "Pietermaritzburg", "Kimberly"};
            for(int i=0; i<cities.Length; ++i)
            {
                cmbDestination.Items.Add(cities[i]);
            }
        }

        //returns to the home screen
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomeScreen());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Destination = cmbDestination.Items[cmbDestination.SelectedIndex].ToString();
            VehicleID = txtVehicleID.Text;
            TripDate = DateTime.Parse(txtTripDate.Text);
            //insert data to database
            insertTripData();
        }

        private void insertTripData()
        {
            try
            {
                //open connection object
                connection.Open();
                //command string
                string cmdString = "INSERT INTO tblTripDetails (VehicleId, Destination, TripDate)" +
                    "VALUES('"+VehicleID+ "', '" + Destination + "', '" + TripDate + "')";
                SqlCommand insertCmd = new SqlCommand(cmdString, connection);
                //execute command and return a row count
                int rowCount = insertCmd.ExecuteNonQuery();
                //test if command has executed
                if (rowCount == 1)
                {
                    MessageBox.Show("Trip successfully scheduled.","Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //open connection object
                connection.Close();
            }
        }

        private void radYes_Checked(object sender, RoutedEventArgs e)
        {
            TripStatus = "Complete";
        }

        private void radNo_Checked(object sender, RoutedEventArgs e)
        {
            TripStatus = "Incomplete";
        }

        private void btnViewSchedule_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //open connection object
                connection.Open();
                //query command string
                string query = "SELECT TripId, VehicleId, Destination, TripDate FROM tblTripDetails";
                //command object
                SqlCommand selectCmd = new SqlCommand(query, connection);
                //object of DataTable
                DataTable table = new DataTable();
                //load query into table
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

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                //command object
                SqlCommand updateCmd = new SqlCommand("UPDATE tblTripDetails SET CompletionStatus ='"+TripStatus+"' WHERE VehicleId = '" + txtVehId.Text + "' ", connection);
                SqlCommand updateCmd2 = new SqlCommand("UPDATE tblTripDetails SET FeulUsed ='" + txtFuelUsed.Text + "' WHERE VehicleId = '" + txtVehId.Text + "' ", connection);
                SqlCommand updateCmd3 = new SqlCommand("UPDATE tblTripDetails SET Incident ='" + txtIncident.Text + "' WHERE VehicleId = '" + txtVehId.Text + "' ", connection);
                SqlCommand updateCmd4 = new SqlCommand("UPDATE tblTripDetails SET [DistanceTravelled (km)] ='" + txtDistance.Text + "' WHERE VehicleId = '" + txtVehId.Text + "' ", connection);
                //execute command and return a row count
                int rowCount = updateCmd.ExecuteNonQuery();
                int rowCount2 = updateCmd2.ExecuteNonQuery();
                int rowCount3 = updateCmd3.ExecuteNonQuery();
                int rowCount4 = updateCmd4.ExecuteNonQuery();
                //test if command has executed
                if (rowCount == 1)
                {
                    if (rowCount2 == 1) 
                    {
                        if (rowCount3 == 1)
                        {
                            if(rowCount4 == 1)
                            {
                                MessageBox.Show("Record successfully updated.", "Record Update", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnTripReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //open connection object
                connection.Open();
                //query command string
                string query = "SELECT * FROM tblTripDetails";
                //command object
                SqlCommand selectCmd = new SqlCommand(query, connection);
                //object of DataTable
                DataTable table = new DataTable();
                //load query into table
                table.Load(selectCmd.ExecuteReader());
                //add items to grid
                dtGrid2.DataContext = table;
            }
            catch (Exception ex)
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
