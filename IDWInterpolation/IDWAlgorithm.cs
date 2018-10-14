using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static IDWInterpolation.Cell;

namespace IDWInterpolation
{
    public class IDWAlgorithm
    {
        private List<Point> knownPoints, gridPoints;
        private List<Cell> cells = new List<Cell>();
        private int cellDivision;
        private int equidistance;
        public int CellDivision { get { return cellDivision; } set { cellDivision = value; } }
        public int Equidistance { get { return equidistance; } set { equidistance = value; } }

        public event EventHandler InterpolationCompleted;
        protected virtual void OnInterpolationCompleted(EventArgs e)
        {
            if (InterpolationCompleted != null)
                InterpolationCompleted(this, e);
        }

        public delegate void LineConnectedEventHandler(object sender, LineToDrawEventArgs args);
        public event LineConnectedEventHandler LineConnected;
        protected virtual void OnLineConnected(LineToDrawEventArgs e)
        {
            if (LineConnected != null)
                LineConnected(this, e);
        }

        public IDWAlgorithm(List<Point> knownPoints, List<Point> gridPoints)
        {
            this.knownPoints = knownPoints;
            this.gridPoints = gridPoints;
        }

        public void interpolatePoints(float radius)
        {
            foreach (Point gridPoint in gridPoints)
            {
                Dictionary<Point, float> pointsInsideCircle = new Dictionary<Point, float>();
                float sum = 0;
                float weight = 0;

                foreach (Point knownPoint in knownPoints)
                {
                    if (gridPoint.isPointWithinRadius(knownPoint, radius))
                    {
                        float distance = Utilities.getDistanceBetween2Points(gridPoint, knownPoint);
                        pointsInsideCircle.Add(knownPoint, distance);
                    }
                }

                foreach (var entry in pointsInsideCircle)
                {
                    sum += 1 / entry.Value;
                    weight += entry.Key.getHeight() / entry.Value;
                }

                if (sum == 0)
                {
                    sum = 1;
                }

                float interp_height = weight / sum;

                gridPoint.setHeight(interp_height);
            }

            OnInterpolationCompleted(new EventArgs());
        }

        public void createCells(int range)
        {
            int cellNumber = 0;
            for (int i = 0; i<range - 1; i++)
            {
                for (int j = 0; j<range - 1; j++)
                {
                    Point topLeft = gridPoints[j + i * range];
                    Point bottomLeft = gridPoints[(j + 1) + i*range]; 
                    Point topRight = gridPoints[j + (i + 1) * range]; 
                    Point bottomRight = gridPoints[(j + 1) + (i + 1) * range];
                    Cell cell = new Cell(topLeft, topRight, bottomLeft, bottomRight, cellDivision);
                    cell.assignCellNumber(cellNumber);
                    cells.Add(cell);
                    cellNumber++;
                }
            }

            //foreach (Cell cell in cells)
            //{
            //    int index = cells.IndexOf(cell);
            //    if (index % (range - 1) != 0)
            //    {
            //        cell.TopCell = cells.ElementAt(index - 1);
            //    }
            //    if (index % (range - 1) != (range - 2))
            //    {
            //        cell.BottomCell = cells.ElementAt(index + 1);
            //    }
            //    if (index / (range - 1) != 0)
            //    {
            //        cell.LeftCell = cells.ElementAt(index - (range - 1));
            //    }
            //    if (index / (range - 1) != (range - 2))
            //    {
            //        cell.RightCell = cells.ElementAt(index + (range - 1));
            //    }
            //}
        }

        public void drawDEMLine(float value, Color color)
        {   
            foreach (Cell cell in cells)
            {
                cell.traverseDEMLine(value);
                List<Point> points = cell.getInputAndOutput();
                List<Point> alt_points = cell.getAltInputAndOutput();
                OnLineConnected(new LineToDrawEventArgs(points[0], points[1], alt_points[0], alt_points[1], color));
                cell.reset();
            } 
        }
    }
}
