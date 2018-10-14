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

namespace IDWInterpolation
{
    /// <summary>
    /// Interaction logic for HyperParams.xaml
    /// </summary>
    public partial class HyperParams : Window
    {
        public HyperParams()
        {
            InitializeComponent();
        }

        private int distance;
        private int divisions;
        private int density;
        private float radius;
        private MainWindow mainWindow;

        private void uiStart_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.clearScreen();
            distance = Convert.ToInt32(this.uiEquidistance.Text);
            divisions = Convert.ToInt32(this.uiDivisions.Text);
            density = Convert.ToInt32(this.uiDensity.Text);
            radius = (float)Convert.ToDecimal(this.uiRadius.Text);
            //if (mainWindow.tokenSource != null)
            //{
            //    mainWindow.tokenSource.Cancel();
            //}

            mainWindow.startAlogirthm(density, divisions, distance, radius);
        }

        public void setMainWindow(Window mainWindow)
        {
            this.mainWindow = mainWindow as MainWindow;
        }

        private void uiExport_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.exportFile("/testFile.dem");
        }
    }
}
