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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            Login login= new Login();

            login.Show();

            this.Close();
        }

        private void RegisterAction(object sender, RoutedEventArgs e)
        {
            if (textBoxPassword.Password != textBoxPasswordConfirm.Password)
            {
                MessageBox.Show("Passwords do not match", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RegisterResponse response = RestCalls.Register(textBoxUsername.Text,textBoxPassword.Password);

            MessageBox.Show(response.Message, response.Error == 1 ? "Error" : "Message", MessageBoxButton.OK, response.Error == 1 ? MessageBoxImage.Error: MessageBoxImage.Information);

            if (response.Error == 0)
            {
                Login(null, null);
            }
        }

        private void RegisterOnEnter(object sender, KeyEventArgs e)
        {
            if (e.Key== Key.Enter)
            {
                RegisterAction(sender, e);
            }
        }
    }
}
