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

namespace Homework3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (mybutton.Content.ToString() != "Maximize!")
            {
                mybutton.Content = "Maximize!";
                this.WindowState = System.Windows.WindowState.Normal;
            }
            else
            {
                mybutton.Content = "Unmaximize!";
                this.WindowState = System.Windows.WindowState.Maximized;
            }
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            NameGrid.Width = this.ActualWidth + 168;
            NameGrid.Width = this.ActualWidth + 168;
            text.Width = this.ActualWidth + 168;
            combo.Width = this.ActualWidth + 168;
        }
    }
}
