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
    /// Interaction logic for VehicleServices.xaml
    /// </summary>
    public partial class VehicleServices : Page
    {
        //connection object
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FTS_DB.mdf;Integrated Security=True");
        //field for the vehicle ID
        public string VehicleID;
        //field for the date of the service
        public DateTime ServiceDate;
        //field for the service code
        public string ServiceCode;
        //get Date and time
        //DateTime date = DateTime.Today;

        public int ServiceNum;
        public VehicleServices()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomeScreen());
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            VehicleID = txtVehicleID.Text;
            ServiceDate = DateTime.Parse(txtServiceDate.Text);
            ServiceCode = txtServiceCode.Text;
            insertServices();
        }

        private void insertServices()
        {
            try
            {
                //open connection object
                connection.Open();
                string cmdString = "INSERT INTO tblServices(VehicleId, ServiceProcedure, ServiceDate, CompletionStatus) " +
                    "VALUES('" + VehicleID + "', '" + ServiceCode + "', '" + ServiceDate + "','Pending')";
                //command object
                SqlCommand insertCmd = new SqlCommand(cmdString, connection);
                //execute command and return a row count
                int rowCount = insertCmd.ExecuteNonQuery();
                //test if command has executed
                if(rowCount == 1)
                {
                    MessageBox.Show("Service successfully scheduled.","Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
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

        private void btnViewAppointment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //open connection object
                connection.Open();
                //query command string
                string query = "SELECT ServiceNum, VehicleId, ServiceProcedure, ServiceDate FROM tblServices";
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

        
        private void showUpdate()
        {
            try
            {
                //open connection object
                connection.Open();
                //query command string
                string query = "SELECT * FROM tblServices";
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

        private void updateRecord()
        {
            try
            {
                connection.Open();
                //command object
                SqlCommand updateCmd = new SqlCommand("UPDATE tblServices SET CompletionStatus ='Complete' WHERE VehicleId = '"+txtVehId.Text+"' ", connection);
                //execute command and return a row count
                int rowCount = updateCmd.ExecuteNonQuery();
                //test if command has executed
                if (rowCount == 1)
                {
                    MessageBox.Show("Record successfully updated.","Record Update", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void radYes_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            if (radYes.IsChecked == true)
            {
                //mark status as complete
                updateRecord();
            }
            else if (radNo.IsChecked == true)
            {
                updateRecord1();
            }
            
        }

        private void updateRecord1()
        {
            try
            {
                connection.Open();
                //command object
                SqlCommand updateCmd = new SqlCommand("UPDATE tblServices SET CompletionStatus ='Incomplete' WHERE VehicleId = '" + txtVehId.Text + "' ", connection);
                //execute command and return a row count
                int rowCount = updateCmd.ExecuteNonQuery();
                //test if command has executed
                if (rowCount == 1)
                {
                    MessageBox.Show("Record successfully updated.", "Record Update", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void btnServReport_Click_1(object sender, RoutedEventArgs e)
        {
            //show the updated data
            showUpdate();
        }
    }
}
