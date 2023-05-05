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
    /// Interaction logic for Timesheets.xaml
    /// </summary>
    public partial class Timesheets : Page
    {
        //connection object
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FTS_DB.mdf;Integrated Security=True");
        public Timesheets()
        {
            InitializeComponent();
            //populate hours of work
            workHoursComboBox();
        }

        private void workHoursComboBox()
        {
            for(int i=0; i<13; ++i)
            {
                cmbWorkHours.Items.Add(i);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HomeScreen());
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            insertTimesheetData();
        }

        private void insertTimesheetData()
        {
            try
            {
                //open connection object
                connection.Open();
                //command string
                string cmdString = "INSERT INTO tblTimesheets (HoursWorked, EmpId, VehicleId)" +
                    "VALUES('" + cmbWorkHours.Items[cmbWorkHours.SelectedIndex].ToString() + "', '" + txtEmpId.Text + "', '" + txtVehicleId.Text + "')";
                SqlCommand insertCmd = new SqlCommand(cmdString, connection);
                //execute command and return a row count
                int rowCount = insertCmd.ExecuteNonQuery();
                //test if command has executed
                if (rowCount == 1)
                {
                    MessageBox.Show("Timesheet data successfully added.","Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                //open connection object
                connection.Close();
            }
        }

        private void btnViewSheet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //open connection object
                connection.Open();
                //query command string
                string query = "SELECT * FROM tblTimesheets";
                //command object
                SqlCommand selectCmd = new SqlCommand(query, connection);
                //object of DataTable
                DataTable table = new DataTable();
                //load query into table
                table.Load(selectCmd.ExecuteReader());
                //add items to grid
                dtGrid.DataContext = table;
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
