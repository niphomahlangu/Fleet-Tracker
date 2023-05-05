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
    /// Interaction logic for UserAccess.xaml
    /// </summary>
    public partial class UserAccess : Window
    {
        //connection object
        SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\FTS_DB.mdf;Integrated Security=True");
        public UserAccess()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string query = "SELECT COUNT(1) FROM tblEmployee WHERE FirstName=@Username AND Password=@Password";
                SqlCommand selectCmd = new SqlCommand(query, connection);
                selectCmd.CommandType = CommandType.Text;
                selectCmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                selectCmd.Parameters.AddWithValue("@Password", txtPassword.Password);
                int count = Convert.ToInt32(selectCmd.ExecuteScalar());
                if(count == 1)
                {
                    MessageBox.Show("Welcome "+txtUsername.Text,"Login Succesfull", MessageBoxButton.OK, MessageBoxImage.Information);
                    //open the Main window
                    new MainWindow().Show();
                    //close this window
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Incorrect Username or Password","Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            //open the Sign Up page
            new SignUp().Show();
        }
    }
}
