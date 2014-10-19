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

namespace KinectFittingRoom
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        MainWindow m= new MainWindow();
        public Splash()
        {
            InitializeComponent();
        }

        private void label_tryon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m.Show();
            this.Close();
            
        }

        private void label_project_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grid_menu.Visibility = Visibility.Hidden;
            textblock_project.Visibility = Visibility.Visible;
            label_return.Visibility = Visibility.Visible;

        }

        private void label_authors_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grid_menu.Visibility = Visibility.Hidden;
            textblock_authors.Visibility = Visibility.Visible;
            label_return.Visibility = Visibility.Visible;
        }

        private void button_exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void button_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void label_return_MouseDown(object sender, MouseButtonEventArgs e)
        {
            grid_menu.Visibility = Visibility.Visible;
            textblock_authors.Visibility = Visibility.Hidden;
            textblock_project.Visibility = Visibility.Hidden;
            label_return.Visibility = Visibility.Hidden;
        }

    }
}
