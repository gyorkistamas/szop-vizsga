using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace szop_vizsga_kliens.Windows;

/// <summary>
/// Interaction logic for ViewDrawing.xaml
/// </summary>
public partial class ViewDrawing : Window
{

    private User LoggedInUser { get; set; }

    private List<Label> cells = new List<Label>();

    private bool IsNew { get; set; }

    private szop_vizsga_kliens.Models.Drawing drawing { get; set; }


    public ViewDrawing(User loggedinUser, int DrawingId, bool newDrawing)
    {
        InitializeComponent();
        CreateButtons();
        IsNew = newDrawing;
        LoggedInUser = loggedinUser;

        if (newDrawing)
        {
            CreateButtons();
            buttonDelete.IsEnabled = false;
        }
        else
        {
            DrawingResponse response = RestCalls.GetSingleDrawing(DrawingId);

            if (response.Error == 1)
            {
                MessageBox.Show(response.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
            else
            {

                drawing = response.Data;

                DisplayDrawing();
            }
        }

    }

    public ViewDrawing(User loggedinUser, bool newDrawing) : this(loggedinUser, 0, newDrawing) { }


    private void CreateButtons()
    {
        CreateLabels("1111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111111");
    }

    private void CreateLabels(string colors)
    {
        cells.Clear();
        int x = 1, y = 2;

        for (int i = 0; i < 100; i++)
        {
            Label temp = new Label();

            temp.Background = GetBrushColor(i >= colors.Length ? '1' : colors[i]);
            temp.SetValue(Grid.RowProperty, x);
            temp.SetValue(Grid.ColumnProperty, y);
            temp.BorderThickness = new Thickness(1);
            if (temp.Background.ToString() == "#FFFFFFFF")
                temp.BorderBrush = Brushes.Black;
            else
                temp.BorderBrush = Brushes.White;
            temp.Margin = new Thickness(1, 1, 1, 1);

            temp.AddHandler(Label.MouseDownEvent, new RoutedEventHandler(ChangeColor));

            gridMain.Children.Add(temp);


            y++;
            cells.Add(temp);
            if ((i + 1) % 10 == 0)
            {
                y = 2;
                x++;
            }
        }
    }


    private void ChangeColor(object sender, RoutedEventArgs e)
    {

        if (!IsNew && drawing.Username != LoggedInUser.Username)
        {
            return;
        }

        Label label = sender as Label;

        label.Background = GetNextColor(label.Background);

        if (label.Background.ToString() == "#FFFFFFFF")
            label.BorderBrush = Brushes.Black;
        else
            label.BorderBrush = Brushes.White;
    }


    private Brush GetNextColor(Brush brush) => brush.ToString() switch 
    {
        "#FFFFFFFF" => Brushes.Black,
        "#FF000000" => Brushes.Red,
        "#FFFF0000" => Brushes.Green,
        "#FF008000" => Brushes.Blue,
        "#FF0000FF" => Brushes.Yellow,
        "#FFFFFF00" => Brushes.White,
        _ => Brushes.White
    };

    private Brush GetBrushColor(char number) => number switch
    {
        '1' => Brushes.White,
        '2' => Brushes.Black,
        '3' => Brushes.Red,
        '4' => Brushes.Green,
        '5' => Brushes.Blue,
        '6' => Brushes.Yellow,
        _ => Brushes.White
    };


    private void DisplayDrawing()
    {
        labelId.Content = "ID: " + drawing.Id;
        labelCreator.Content = "Creator: " + drawing.Username;
        textboxTitle.Text = drawing.Title;
        CreateLabels(drawing.Drawing_Data);

        if (LoggedInUser.Username != drawing.Username)
        {
            textboxTitle.IsReadOnly = true;
            buttonSave.IsEnabled = false;
            buttonDelete.IsEnabled = false;
            
        }
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void Save(object sender, RoutedEventArgs e)
    {

        SimpleResponse response = null;

        if (IsNew)
        {
            string drawing = GetDrawingData();
            response = RestCalls.NewDrawing(LoggedInUser.Token, textboxTitle.Text, drawing);
        }
        else
        {
            string drawing_data = GetDrawingData();
            response = RestCalls.UpdateDrawing(LoggedInUser.Token, textboxTitle.Text, drawing.Id, drawing_data);
        }


        if (response.Error == 1)
        {
            MessageBox.Show(response.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            MessageBox.Show(response.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

    }

    private string GetDrawingData()
    {
        string data = "";

        foreach (var item in cells)
        {
            data += GetNumberForColor(item.Background.ToString());
        }

        return data;
    }

    private string GetNumberForColor(string color) => color switch
    {
        "#FFFFFFFF" => "1",
        "#FF000000" => "2",
        "#FFFF0000" => "3",
        "#FF008000" => "4",
        "#FF0000FF" => "5",
        "#FFFFFF00" => "6",
        _ => "1"
    };

    private void Delete(object sender, RoutedEventArgs e)
    {



        SimpleResponse response = RestCalls.DeleteDrawing(LoggedInUser.Token, drawing.Id);

        if (response.Error == 1)
        {
            MessageBox.Show(response.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        else
        {
            MessageBox.Show(response.Message, "Message", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
