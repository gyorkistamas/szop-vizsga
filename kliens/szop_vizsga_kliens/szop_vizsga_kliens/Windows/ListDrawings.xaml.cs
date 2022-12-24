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
using szop_vizsga_kliens.Models;

namespace szop_vizsga_kliens.Windows
{
    /// <summary>
    /// Interaction logic for ListDrawings.xaml
    /// </summary>
    public partial class ListDrawings : Window
    {
        User LoggedInUser { get; set; }
        public ListDrawings(User user)
        {
            InitializeComponent();
            LoggedInUser = user;
        }
    }
}
