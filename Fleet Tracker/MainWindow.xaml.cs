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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Frame.NavigationService.Navigate(new HomeScreen());
        }
        //event to close window
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();//close window
        }

        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
            new UserAccess().Show();
            //close current window
            this.Close();
        }
    }
}
