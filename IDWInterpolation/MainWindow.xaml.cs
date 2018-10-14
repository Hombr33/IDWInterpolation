using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace IDWInterpolation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private PointReader pr;
        private GridPoints gp;
        private IDWAlgorithm algo;
        private string path = @"C:\Users\D1996e\Desktop\output2.txt";

        private List<Ellipse> gridPointsOnDisplay = new List<Ellipse>();
        private List<Ellipse> knownPointsOnDisplay = new List<Ellipse>();

        public CancellationTokenSource tokenSource = new CancellationTokenSource();

        public void clearScreen()
        {
            this.uiCanvas.Children.Clear();
        }

        public MainWindow()
        {
            InitializeComponent();
            HyperParams hyperParamsWindow = new HyperParams();
            hyperParamsWindow.setMainWindow(this);
            hyperParamsWindow.Show();
        }

        

        public void startAlogirthm(int density, int divisions, int distance, float radius)
        {
            pr = new PointReader(path);
            pr.PointsLoaded += OnPointsLoaded;
            pr.readText();
            float[] limits = pr.getGridLimits();
            gp = new GridPoints(limits[0], limits[1], limits[2], limits[3], density);
            gp.GridCreated += OnGridCreated;
            gp.createGrid();
            algo = new IDWAlgorithm(pr.getKnownPoints(), gp.getGridPoints());
            algo.CellDivision = divisions;
            algo.Equidistance = distance * 5;
            algo.InterpolationCompleted += OnInterpolationCompleted;
            algo.LineConnected += OnLineDraw; 
            var token = tokenSource.Token;
            Task.Run(() =>
            {
                algo.interpolatePoints(radius);
                algo.createCells(gp.getNumSteps());
                for (int i = 0; i < 50; i++)
                {
                    float height = 300 + distance * i;
                    if (height % algo.Equidistance == 0)
                    {
                        algo.drawDEMLine(height, Color.FromRgb(200, 50, 200));
                    }
                    else
                    {
                        algo.drawDEMLine(height, Color.FromRgb(200, 50, 0));
                    }

                }
            }, token);
        }

        public void exportFile(string path)
        {
            string absolutePath = AppDomain.CurrentDomain.BaseDirectory + path;
            if (!File.Exists(absolutePath))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(absolutePath))
                {
                    sw.WriteLine("Number of Points: " + gp.getGridPoints().Count);
                    sw.WriteLine("Cell Divisions: " + algo.CellDivision);
                    sw.WriteLine("Equidistance: " + algo.Equidistance);
                    sw.WriteLine();
                    foreach (Point p in gp.getGridPoints())
                    {
                        sw.WriteLine("X: " + p.getX());
                        sw.WriteLine("Y: " + p.getY());
                        sw.WriteLine("Height: " + p.getHeight());
                        sw.WriteLine();
                    }
                }
            }
        }

        private void OnLineDraw(object sender, LineToDrawEventArgs args)
        {
            this.Dispatcher.Invoke(() =>
            {
                void Line_MouseDown(object _sender, MouseButtonEventArgs e, float x1, float x2, float y1, float y2)
                {
                    Console.WriteLine("X1: " + x1.ToString() + " X2: " + x2.ToString() + " Y1: " + y1.ToString() + " Y2: " + y2.ToString());
                }

                if (args.P1 != null && args.P2 != null)
                {
                    float[] limits = pr.getGridLimits();
                    float xMin = limits[0];
                    float xMax = limits[1];
                    float yMin = limits[2];
                    float yMax = limits[3];
                    float uiWidth = Convert.ToSingle(uiCanvas.Width);
                    float uiHeight = Convert.ToSingle(uiCanvas.Height);

                    float x1 = Utilities.normalizeValueBetweenRange(args.P1.getX(), xMin, xMax, 0, uiWidth);
                    float x2 = Utilities.normalizeValueBetweenRange(args.P2.getX(), xMin, xMax, 0, uiWidth);
                    float y1 = Utilities.normalizeValueBetweenRange(args.P1.getY(), yMin, yMax, 0, uiHeight);
                    float y2 = Utilities.normalizeValueBetweenRange(args.P2.getY(), yMin, yMax, 0, uiHeight);

                    Line line = new Line();
                    
                    Brush brush = new SolidColorBrush(args.Color);
                    line.Stroke = brush;
                    line.X1 = x1;
                    line.X2 = x2;
                    line.Y1 = y1;
                    line.Y2 = y2;

                    line.MouseDown += (_sender, e) => Line_MouseDown(_sender, e, args.P1.getX(), args.P2.getX(), args.P1.getY(), args.P2.getY());

                    line.StrokeThickness = 2;
                    uiCanvas.Children.Add(line);
                }

                if (args.Alt_P1 != null && args.Alt_p2 != null)
                {
                    float[] limits = pr.getGridLimits();
                    float xMin = limits[0];
                    float xMax = limits[1];
                    float yMin = limits[2];
                    float yMax = limits[3];
                    float uiWidth = Convert.ToSingle(uiCanvas.Width);
                    float uiHeight = Convert.ToSingle(uiCanvas.Height);

                    float x1 = Utilities.normalizeValueBetweenRange(args.Alt_P1.getX(), xMin, xMax, 0, uiWidth);
                    float x2 = Utilities.normalizeValueBetweenRange(args.Alt_p2.getX(), xMin, xMax, 0, uiWidth);
                    float y1 = Utilities.normalizeValueBetweenRange(args.Alt_P1.getY(), yMin, yMax, 0, uiHeight);
                    float y2 = Utilities.normalizeValueBetweenRange(args.Alt_p2.getY(), yMin, yMax, 0, uiHeight);

                    Line line = new Line();
                    Brush brush = new SolidColorBrush(args.Color);
                    line.Stroke = brush;
                    line.X1 = x1;
                    line.X2 = x2;
                    line.Y1 = y1;
                    line.Y2 = y2;

                    line.MouseDown += (_sender, e) => Line_MouseDown(_sender, e, args.Alt_P1.getX(), args.Alt_p2.getX(), args.Alt_P1.getY(), args.Alt_p2.getY());

                    line.StrokeThickness = 2;
                    uiCanvas.Children.Add(line);
                }
            });
        }

   

        private void OnInterpolationCompleted(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                removeGridPoints();
                //removeKnownPoints();
                float[] limits = pr.getGridLimits();
                float xMin = limits[0];
                float xMax = limits[1];
                float yMin = limits[2];
                float yMax = limits[3];
                float uiWidth = Convert.ToSingle(uiCanvas.Width);
                float uiHeight = Convert.ToSingle(uiCanvas.Height);

                foreach (Point p in gp.getGridPoints())
                {
                    float xPos = Utilities.normalizeValueBetweenRange(p.getX(), xMin, xMax, 0, uiWidth);
                    float yPos = Utilities.normalizeValueBetweenRange(p.getY(), yMin, yMax, 0, uiHeight);
                    placeKnownGridPoint(p, xPos, yPos);
                }
            });
        }
        private void OnGridCreated(object sender, EventArgs e)
        {
            float[] limits = pr.getGridLimits();
            float xMin = limits[0];
            float xMax = limits[1];
            float yMin = limits[2];
            float yMax = limits[3];
            float uiWidth = Convert.ToSingle(uiCanvas.Width);
            float uiHeight = Convert.ToSingle(uiCanvas.Height);

            foreach (Point p in gp.getGridPoints())
            {
                float xPos = Utilities.normalizeValueBetweenRange(p.getX(), xMin, xMax, 0, uiWidth);
                float yPos = Utilities.normalizeValueBetweenRange(p.getY(), yMin, yMax, 0, uiHeight);
                placeKnownGridPoint(p, xPos, yPos);
            }
        }

        private void placeKnownPoint(Point point, float x, float y)
        {
            int range = (int)(point.getHeight() / 5f);
            Color color;
            switch (range)
            {
                case 60:
                    color = Color.FromRgb(0, 17, 229);
                    break;
                case 61:
                    color = Color.FromRgb(6, 16, 220);
                    break;
                case 62:
                    color = Color.FromRgb(13, 15, 212);
                    break;
                case 63:
                    color = Color.FromRgb(20, 15, 204);
                    break;
                case 64:
                    color = Color.FromRgb(27, 14, 196);
                    break;
                case 65:
                    color = Color.FromRgb(34, 14, 188);
                    break;
                case 66:
                    color = Color.FromRgb(40, 13, 179);
                    break;
                case 67:
                    color = Color.FromRgb(47, 13, 171);
                    break;
                case 68:
                    color = Color.FromRgb(54, 12, 163);
                    break;
                case 69:
                    color = Color.FromRgb(61, 12, 159);
                    break;
                case 70:
                    color = Color.FromRgb(68, 11, 147);
                    break;
                case 71:
                    color = Color.FromRgb(75, 11, 139);
                    break;
                case 72:
                    color = Color.FromRgb(81, 10, 130);
                    break;
                case 73:
                    color = Color.FromRgb(88, 10, 122);
                    break;
                case 74:
                    color = Color.FromRgb(95, 9, 114);
                    break;
                case 75:
                    color = Color.FromRgb(102, 8, 106);
                    break;
                case 76:
                    color = Color.FromRgb(109, 8, 98);
                    break;
                case 77:
                    color = Color.FromRgb(115, 7, 89);
                    break;
                case 78:
                    color = Color.FromRgb(122, 7, 81);
                    break;
                case 79:
                    color = Color.FromRgb(129, 6, 73);
                    break;
                case 80:
                    color = Color.FromRgb(136, 6, 65);
                    break;
                case 81:
                    color = Color.FromRgb(143, 5, 57);
                    break;
                case 82:
                    color = Color.FromRgb(150, 5, 49);
                    break;
                case 83:
                    color = Color.FromRgb(156, 4, 40);
                    break;
                case 84:
                    color = Color.FromRgb(163, 4, 32);
                    break;
                case 85:
                    color = Color.FromRgb(170, 3, 24);
                    break;
                case 86:
                    color = Color.FromRgb(177, 3, 16);
                    break;
                case 87:
                    color = Color.FromRgb(184, 2, 8);
                    break;
                case 88:
                    color = Color.FromRgb(191, 2, 0);
                    break;
                case 89:
                    color = Color.FromRgb(200, 1, 0);
                    break;
                case 90:
                    color = Color.FromRgb(213, 1, 0);
                    break;
                default:
                    color = Color.FromRgb(0, 0, 0);
                    break;

            }
            SolidColorBrush brush = new SolidColorBrush(color);
            Pen blackPen = new Pen(brush, 3);

            float width = 2f;
            float height = 2f;

            Ellipse myEllipse = new Ellipse();
            myEllipse.Fill = brush;
            myEllipse.StrokeThickness = 0.2f;
            myEllipse.Stroke = Brushes.Black;
            myEllipse.Width = width;
            myEllipse.Height = height;

            Canvas.SetLeft(myEllipse, x);
            Canvas.SetTop(myEllipse, y);
            uiCanvas.Children.Add(myEllipse);

            knownPointsOnDisplay.Add(myEllipse);
        }
        private void placeKnownGridPoint(Point point, float x, float y)
        {
            int range = (int)(point.getHeight() / 5f);
            Color color;
            switch (range)
            {
                case 60:
                    color = Color.FromRgb(0, 17, 229);
                    break;
                case 61:
                    color = Color.FromRgb(6, 16, 220);
                    break;
                case 62:
                    color = Color.FromRgb(13, 15, 212);
                    break;
                case 63:
                    color = Color.FromRgb(20, 15, 204);
                    break;
                case 64:
                    color = Color.FromRgb(27, 14, 196);
                    break;
                case 65:
                    color = Color.FromRgb(34, 14, 188);
                    break;
                case 66:
                    color = Color.FromRgb(40, 13, 179);
                    break;
                case 67:
                    color = Color.FromRgb(47, 13, 171);
                    break;
                case 68:
                    color = Color.FromRgb(54, 12, 163);
                    break;
                case 69:
                    color = Color.FromRgb(61, 12, 159);
                    break;
                case 70:
                    color = Color.FromRgb(68, 11, 147);
                    break;
                case 71:
                    color = Color.FromRgb(75, 11, 139);
                    break;
                case 72:
                    color = Color.FromRgb(81, 10, 130);
                    break;
                case 73:
                    color = Color.FromRgb(88, 10, 122);
                    break;
                case 74:
                    color = Color.FromRgb(95, 9, 114);
                    break;
                case 75:
                    color = Color.FromRgb(102, 8, 106);
                    break;
                case 76:
                    color = Color.FromRgb(109, 8, 98);
                    break;
                case 77:
                    color = Color.FromRgb(115, 7, 89);
                    break;
                case 78:
                    color = Color.FromRgb(122, 7, 81);
                    break;
                case 79:
                    color = Color.FromRgb(129, 6, 73);
                    break;
                case 80:
                    color = Color.FromRgb(136, 6, 65);
                    break;
                case 81:
                    color = Color.FromRgb(143, 5, 57);
                    break;
                case 82:
                    color = Color.FromRgb(150, 5, 49);
                    break;
                case 83:
                    color = Color.FromRgb(156, 4, 40);
                    break;
                case 84:
                    color = Color.FromRgb(163, 4, 32);
                    break;
                case 85:
                    color = Color.FromRgb(170, 3, 24);
                    break;
                case 86:
                    color = Color.FromRgb(177, 3, 16);
                    break;
                case 87:
                    color = Color.FromRgb(184, 2, 8);
                    break;
                case 88:
                    color = Color.FromRgb(191, 2, 0);
                    break;
                case 89:
                    color = Color.FromRgb(200, 1, 0);
                    break;
                case 90:
                    color = Color.FromRgb(213, 1, 0);
                    break;
                default:
                    color = Color.FromRgb(0, 0, 0);
                    break;

            }
            SolidColorBrush brush = new SolidColorBrush(color);
            Pen blackPen = new Pen(brush, 3);

            float width = 6f;
            float height = 6f;

            Ellipse myEllipse = new Ellipse();
            myEllipse.MouseDown += (sender, e) => OnMouseEnterEllipse(sender, e, point.getHeight().ToString(), point.getX().ToString(), point.getY().ToString());
            myEllipse.Fill = brush;
            myEllipse.StrokeThickness = 0.2f;
            myEllipse.Stroke = Brushes.Black;
            myEllipse.Width = width;
            myEllipse.Height = height;

            Canvas.SetLeft(myEllipse, x);
            Canvas.SetTop(myEllipse, y);
            uiCanvas.Children.Add(myEllipse);

            gridPointsOnDisplay.Add(myEllipse);
        }

        private void OnMouseEnterEllipse(object sender, MouseEventArgs e, string height, string X, string Y)
        {
            Console.WriteLine("X: " + X + " Y: " + Y + " Height: " + height);
        }

        private void removeGridPoints()
        {
            foreach (Ellipse ellipse in gridPointsOnDisplay)
            {
                uiCanvas.Children.Remove(ellipse);
            }
        } 
        private void removeKnownPoints()
        {
            foreach (Ellipse ellipse in knownPointsOnDisplay)
            {
                uiCanvas.Children.Remove(ellipse);
            }
        }

        private void OnPointsLoaded(object sender, EventArgs e)
        {
            float[] limits = pr.getGridLimits();
            float xMin = limits[0];
            float xMax = limits[1];
            float yMin = limits[2];
            float yMax = limits[3];
            float uiWidth = Convert.ToSingle(uiCanvas.Width);
            float uiHeight = Convert.ToSingle(uiCanvas.Height);
            foreach (Point p in pr.getKnownPoints())
            {
                float xPos = Utilities.normalizeValueBetweenRange(p.getX(), xMin, xMax, 0, uiWidth);
                float yPos = Utilities.normalizeValueBetweenRange(p.getY(), yMin, yMax, 0, uiHeight);
                placeKnownPoint(p, xPos, yPos);
            }
        }
    }
}
