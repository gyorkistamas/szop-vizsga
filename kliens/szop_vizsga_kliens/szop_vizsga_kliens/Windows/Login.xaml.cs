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
using szop_vizsga_kliens.Backend;
using szop_vizsga_kliens.Models;

namespace szop_vizsga_kliens.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void LoginAction(object sender, RoutedEventArgs e)
        {
            LoginResponse response = RestCalls.Login(textBoxUsername.Text, textBoxPassword.Password);

            if (response.Error == 1)
            {
                MessageBox.Show(response.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            User loggedInUser = new User(textBoxUsername.Text, response.Token);
            ListDrawings listDrawings = new ListDrawings(loggedInUser);
            listDrawings.Show();
            this.Close();

        }

        private void LoginOnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginAction(sender, e);
            }
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();

            reg.Show();

            this.Close();
        }

        private void VisitorLogin(object sender, RoutedEventArgs e)
        {
            User loggedInUser = new User("Visitor", "none");
            ListDrawings listDrawings = new ListDrawings(loggedInUser);
            listDrawings.Show();
            this.Close();
        }
    }
}
