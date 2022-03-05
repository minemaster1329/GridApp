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
using System.Reflection;

namespace GridApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cmbColors.ItemsSource = typeof(Brushes).GetProperties();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            string rowCountString = rowCountTextBox.Text;
            string columnCountString = columnCountTextBox.Text;

            if (int.TryParse(rowCountString, out int rows)&& int.TryParse(columnCountString, out int cols) && rows > 0 && cols >0)
            {
                squaresGrid.Children.Clear();
                squaresGrid.RowDefinitions.Clear();
                squaresGrid.ColumnDefinitions.Clear();

                for (int i = 0; i < rows; i++)
                {
                    squaresGrid.RowDefinitions.Add(new RowDefinition());
                }

                for (int i = 0; i < cols; i++)
                {
                    squaresGrid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        Rectangle rect = new Rectangle();
                        rect.MouseLeftButtonDown += Rect_MouseLeftButtonDown;
                        rect.MouseRightButtonDown += Rect_MouseRightButtonDown;
                        rect.Fill = Brushes.Red;
                        rect.Margin = new Thickness(1);
                        rect.SetValue(Grid.RowProperty, i);
                        rect.SetValue(Grid.ColumnProperty, j);
                        squaresGrid.Children.Add(rect);
                    }
                }
            }
        }

        private void Rect_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is null) return;
            Rectangle r = (Rectangle)sender;
            r.Fill = Brushes.Red;
        }

        private void Rect_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is null || cmbColors.SelectedItem is null) return;
            Rectangle r = (Rectangle)sender;
            r.Fill = (Brush) (cmbColors.SelectedItem as PropertyInfo).GetValue(null, null);
        }
    }
}
