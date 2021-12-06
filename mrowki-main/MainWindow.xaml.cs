using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace mrowki
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public PassedData passedData;
        public MainWindow()
        {
            passedData = new PassedData();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = new MoreInfo();
            window.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Window window = new InitialSettings(passedData);
            window.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Window window = new ProgramWindow(passedData);
            window.Show();
        }
    }
}
