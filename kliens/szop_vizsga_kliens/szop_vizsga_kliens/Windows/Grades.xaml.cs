using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Grades.xaml
    /// </summary>
    public partial class Grades : Window
    {

        private DataTable Data = new DataTable();

        public Grades()
        {
            InitializeComponent();
            BuildTable();
        }


        private void BuildTable()
        {
            Data.Columns.Add("Id");
            Data.Columns.Add("Grade");
            Data.Columns.Add("Date");
            Data.Columns.Add("Description");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GetData(object sender, RoutedEventArgs e)
        {
            PHPResponse response = RestCalls.AccessPHP(textboxUsername.Text, textboxPassword.Password);

            if (response.Error == 1)
            {
                MessageBox.Show(response.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Data.Rows.Clear();

            foreach (var item in response.Grades)
            {
                string[] tempData = new string[4] { Convert.ToString(item.Id), Convert.ToString(item.Grade), item.Date, item.Description };
                Data.Rows.Add(tempData);
            }

            datagridGrades.ItemsSource = Data.DefaultView;

        }
    }
}
