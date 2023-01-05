using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
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

namespace szop_vizsga_kliens.Windows;

/// <summary>
/// Interaction logic for ListDrawings.xaml
/// </summary>
public partial class ListDrawings : Window
{
    User LoggedInUser { get; set; }

    private DataTable Drawings = new DataTable();

    private int SelectedDrawingId = -1;
    public ListDrawings(User user)
    {
        InitializeComponent();
        LoggedInUser = user;
        labelWelcome.Content = "Hi, " + LoggedInUser.Username + "!";

        BuildDataGrid();
        Refresh(null, null);

        if (LoggedInUser.Token == "none")
        {
            buttonNewDrawing.IsEnabled= false;
        }
    }

    private void BuildDataGrid()
    {
        Drawings.Columns.Add("Id", typeof(int));
        Drawings.Columns.Add("Creator name", typeof(string));
        Drawings.Columns.Add("Title", typeof(string));
    }


    private void Refresh(object sender, EventArgs e)
    {
        Drawings.Rows.Clear();

        ListOfDrawingsResponse drawings = RestCalls.GetAllDrawings();

        if (drawings.Error == 1)
        {
            MessageBox.Show(drawings.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        foreach (var item in drawings.Data)
        {
            string[] tempLine = new string[3] { Convert.ToString(item.Id), item.Username, item.Title};
            Drawings.Rows.Add(tempLine);
        }

        datagridDrawings.ItemsSource = Drawings.DefaultView;
    }

    private void SelectionChanged(object sender, SelectedCellsChangedEventArgs e)
    {
        DataRowView selected = (DataRowView)datagridDrawings.SelectedItem;

        if (selected != null)
            SelectedDrawingId = (int)selected.Row[0];
        else
            SelectedDrawingId = -1;
    }

    private void Logout(object sender, RoutedEventArgs e)
    {
        LoggedInUser = null;
        Login login = new Login();
        login.Show();

        this.Close();
    }

    private void Exit(object sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    private void NewDrawing(object sender, RoutedEventArgs e)
    {
        ViewDrawing drawingWindow = new ViewDrawing(LoggedInUser, true);
        drawingWindow.ShowDialog();

        Refresh(null, null);
    }

    private void ViewEditDrawing(object sender, RoutedEventArgs e)
    {
        if (SelectedDrawingId == -1)
        {
            MessageBox.Show("No drawing selected, please highlight one!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        ViewDrawing drawingWindow = new ViewDrawing(LoggedInUser, SelectedDrawingId, false);
        try
        {
            drawingWindow.ShowDialog();
        }
        catch(Exception)
        {
            // Error already handled in other windows
        }

        Refresh(null, null);
    }

    private void AccessPHPAPI(object sender, RoutedEventArgs e)
    {
        Grades gradesWindow = new Grades();
        gradesWindow.ShowDialog();
    }
}
