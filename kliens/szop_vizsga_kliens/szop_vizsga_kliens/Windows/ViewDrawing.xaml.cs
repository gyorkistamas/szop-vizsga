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
    /// Interaction logic for ViewDrawing.xaml
    /// </summary>
    public partial class ViewDrawing : Window
    {

        private User LoggedInUser { get; set; }


        public ViewDrawing(User loggedinUser, int DrawingId, bool newDrawing)
        {
            InitializeComponent();
        }

        public ViewDrawing(User loggedinUser, bool newDrawing) : this(loggedinUser, 0, newDrawing) { }
    }
}
