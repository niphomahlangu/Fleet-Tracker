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
using System.Data;
using System.Data.SqlClient;

namespace Fleet_Tracker
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        //connection object
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FTS_DB.mdf;Integrated Security=True");
        public SignUp()
        {
            InitializeComponent();
            //populate employee type combo box
            empTypeComboBox();
        }

        private void empTypeComboBox()
        {
            string[] empType = { "Office manager", "Vehicle admin manager", "Trip/Usage manager", "Service manager", "Timesheet manager"};
            //array items to combo box
            for(int y=0; y<empType.Length; ++y)
            {
                cmbEmpType.Items.Add(empType[y]);
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //open the connection object
                connection.Open();
                //command object
                SqlCommand insertCmd = new SqlCommand("INSERT INTO tblEmployee(EmpId, FirstName, Surname, EmpType, Password) VALUES('"+txtEmpId.Text+ "','" + txtFName.Text + "','" + txtSurname.Text + "','" + cmbEmpType.Items[cmbEmpType.SelectedIndex].ToString() + "','" + txtPassword.Password + "')", connection);
                int rowCount = insertCmd.ExecuteNonQuery();
                if(rowCount == 1)
                {
                    MessageBox.Show("Account successfully added.");
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
            //close this window
            this.Close();
        }
    }
}
