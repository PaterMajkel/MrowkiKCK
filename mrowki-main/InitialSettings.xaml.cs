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
    /// Logika interakcji dla klasy InitialSettings.xaml
    /// </summary>
    public partial class InitialSettings : Window
    {
        public PassedData passedData;

        public InitialSettings(PassedData passedData)
        {
            this.passedData = passedData;
            InitializeComponent();
            PopSize.Text = passedData.popSize.ToString();
            MutChance.Text = passedData.mutChance.ToString();
            LifeLength.Text = passedData.lifeLength.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int example = 0;
            if(MutChance.Text!="" && LifeLength.Text !="" && PopSize.Text!="")
            {
                var x = int.Parse(MutChance.Text);
                if (x < 0 || x.GetType() != example.GetType())
                {
                    example++;

                }
                var y = int.Parse(LifeLength.Text);

                if (y < 0 || y.GetType() != example.GetType())
                    example++;
                var z = int.Parse(PopSize.Text);
                if (z < 0 || z.GetType() != example.GetType())
                    example++;
                if (example > 0)
                {
                    MessageBox.Show("Złe dane!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                passedData.mutChance = x;
                passedData.lifeLength = y;
                passedData.popSize = z;

                this.Close();
            }
            else
            {
                MessageBox.Show("Złe dane!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
           


        }

        private void PopSize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Length<2)
            {
                PopSize.Text = "";
                MessageBox.Show("Złe dane!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void MutChance_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Length < 2)
            {
                MutChance.Text = "";
                MessageBox.Show("Złe dane!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void LifeLength_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString().Length < 2)
            {
                LifeLength.Text = "";
                MessageBox.Show("Złe dane!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
