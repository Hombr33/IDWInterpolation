using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IDWInterpolation
{
    public class Cell
    {

        private Point p1, p2, p3, p4;
        private int number;
        private int steps;
        public enum sides { LEFT, TOP, RIGHT, BOTTOM };
        private sides input_side;
        private sides output_side;
        private sides input_side_alt;
        private sides output_side_alt;

        private Point input;
        private Point output;
        private Point alt_input;
        private Point alt_output;


        private Cell leftCell;
        private Cell rightCell;
        private Cell topCell;
        private Cell bottomCell;
        public Cell LeftCell { get { return leftCell; } set { leftCell = value; } }
        public Cell RightCell { get { return rightCell; } set { rightCell = value; } }
        public Cell TopCell { get { return topCell; } set { topCell = value; } }
        public Cell BottomCell { get { return bottomCell; } set { bottomCell = value; } }

        private List<Point> marginPoints_left = new List<Point>();
        private List<Point> marginPoints_right = new List<Point>();
        private List<Point> marginPoints_top = new List<Point>();
        private List<Point> marginPoints_bottom = new List<Point>();
        

        public Cell(Point p1, Point p2, Point p3, Point p4, int steps)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
            this.p4 = p4;
            this.steps = steps;
            divideCell();
        }

        public void assignCellNumber(int number)
        {
            this.number = number;
        }
        public int getCellNumber() { return this.number; }

        private void divideCell()
        {
            float stepX = Math.Abs(p4.getX() - p1.getX()) / steps;
            float stepY = Math.Abs(p4.getY() - p1.getY()) / steps;
            float stepZ; 
            //topLeft - topRight
            stepZ = Math.Abs(p2.getHeight() - p1.getHeight()) / steps;
            if (p1.getHeight() >= p2.getHeight())
            {
                for (int i = 1; i < steps; i++)
                {
                    Point newPoint = new Point(p1.getX() + stepX*i, p1.getY(), p1.getHeight() - stepZ*i);
                    marginPoints_top.Add(newPoint);
                }
            }
            else
            {
                for (int i = 1; i < steps; i++)
                {
                    Point newPoint = new Point(p1.getX() + stepX*i, p1.getY(), p1.getHeight() + stepZ*i);
                    marginPoints_top.Add(newPoint);
                }
            }
            //topLeft - bottomLeft
            stepZ = Math.Abs(p3.getHeight() - p1.getHeight()) / steps;
            if (p1.getHeight() >= p3.getHeight())
            {
                for (int i = 1; i < steps; i++)
                {
                    Point newPoint = new Point(p1.getX(), p1.getY() + stepY*i, p1.getHeight() - stepZ*i);
                    marginPoints_left.Add(newPoint);
                }
            }
            else
            {
                for (int i = 1; i < steps; i++)
                {
                    Point newPoint = new Point(p1.getX(), p1.getY() + stepY*i, p1.getHeight() + stepZ*i);
                    marginPoints_left.Add(newPoint);
                }
            }
            //topRight - bottomRight
            stepZ = Math.Abs(p2.getHeight() - p4.getHeight()) / steps;
            if (p2.getHeight() >= p4.getHeight())
            {
                for (int i = 1; i < steps; i++)
                {
                    Point newPoint = new Point(p2.getX(), p2.getY() + stepY*i, p2.getHeight() - stepZ*i);
                    marginPoints_right.Add(newPoint);
                }
            }
            else
            {
                for (int i = 1; i < steps; i++)
                {
                    Point newPoint = new Point(p2.getX(), p2.getY() + stepY*i, p2.getHeight() + stepZ*i);
                    marginPoints_right.Add(newPoint);
                }
            }
            //bottomLeft - bottomRight
            stepZ = Math.Abs(p3.getHeight() - p4.getHeight()) / steps;
            if (p3.getHeight() >= p4.getHeight())
            {
                for (int i = 1; i < steps; i++)
                {
                    Point newPoint = new Point(p3.getX() + stepX*i, p3.getY(), p3.getHeight() - stepZ*i);
                    marginPoints_bottom.Add(newPoint);
                }
            }
            else
            {
                for (int i = 1; i < steps; i++)
                {
                    Point newPoint = new Point(p3.getX() + stepX*i, p3.getY(), p3.getHeight() + stepZ*i);
                    marginPoints_bottom.Add(newPoint);
                }
            }
        }

        internal void reset()
        {
            this.input = null;
            this.output = null;
        }

        private void trySetOutputSide(sides side, float height, bool alternative)
        {
            List<Point> margins;
            Point _p1, _p2;
            Point higher;
            Point lower;
            switch (side)
            {
                case sides.LEFT:
                    margins = marginPoints_left;
                    _p1 = p1;
                    _p2 = p3;
                    break;
                case sides.BOTTOM:
                    margins = marginPoints_bottom;
                    _p1 = p3;
                    _p2 = p4;
                    break;
                case sides.RIGHT:
                    margins = marginPoints_right;
                    _p1 = p2;
                    _p2 = p4;
                    break;
                case sides.TOP:
                    margins = marginPoints_top;
                    _p1 = p1;
                    _p2 = p2;
                    break;
                default:
                    margins = marginPoints_left;
                    _p1 = p1;
                    _p2 = p3;
                    break;

            }
            if (_p1.getHeight() > _p2.getHeight())
            {
                higher = _p1;
                lower = _p2;
            }
            else
            {
                higher = _p2;
                lower = _p1;
            }

            if (alternative)
            {
                if (lower.getHeight() < height && higher.getHeight() >= height && input_side_alt != side && input_side != side && output_side != side)
                {
                    output_side_alt = side;
                    int pointIndex = Utilities.getIndexOfClosestValue(margins, height);
                    alt_output = margins.ElementAt(pointIndex);
                }
            } else
            {
                if (lower.getHeight() < height && higher.getHeight() >= height && input_side != side)
                {
                    output_side = side;
                    int pointIndex = Utilities.getIndexOfClosestValue(margins, height);
                    output = margins.ElementAt(pointIndex);
                }
            }
        }
        private void tryInputSide(sides side, float height, bool alternative)
        {
            List<Point> margins;
            Point _p1, _p2;
            Point higher;
            Point lower;
            switch (side)
            {
                case sides.LEFT:
                    margins = marginPoints_left;
                    _p1 = p1;
                    _p2 = p3;
                    break;
                case sides.BOTTOM:
                    margins = marginPoints_bottom;
                    _p1 = p3;
                    _p2 = p4;
                    break;
                case sides.RIGHT:
                    margins = marginPoints_right;
                    _p1 = p2;
                    _p2 = p4;
                    break;
                case sides.TOP:
                    margins = marginPoints_top;
                    _p1 = p1;
                    _p2 = p2;
                    break;
                default:
                    margins = marginPoints_left;
                    _p1 = p1;
                    _p2 = p3;
                    break;
            }
            if (_p1.getHeight() > _p2.getHeight())
            {
                higher = _p1;
                lower = _p2;
            }
            else
            {
                higher = _p2;
                lower = _p1;
            }

            if (alternative)
            {
                if (lower.getHeight() < height && higher.getHeight() >= height && input_side_alt != side && input_side != side && output_side != side)
                {
                    int pointIndex = Utilities.getIndexOfClosestValue(margins, height);
                    input_side_alt = side;
                    alt_input = margins.ElementAt(pointIndex);
                    setOutput(height, true);
                }
            } else
            {
                if (lower.getHeight() < height && higher.getHeight() >= height && input_side != side)
                {
                    int pointIndex = Utilities.getIndexOfClosestValue(margins, height);
                    input_side = side;
                    input = margins.ElementAt(pointIndex);
                    setOutput(height, false);
                }
            }

            
        }

        public void traverseDEMLine(float height)
        {
            tryInputSide(sides.LEFT, height, false);

            if (input != null && output != null)
            {
                switch (output_side)
                {
                    case sides.BOTTOM:
                        tryInputSide(sides.RIGHT, height, true);
                        tryInputSide(sides.TOP, height, true);
                        break;
                    case sides.RIGHT:
                        tryInputSide(sides.BOTTOM, height, true);
                        tryInputSide(sides.TOP, height, true);
                        break;
                    case sides.TOP:
                        tryInputSide(sides.BOTTOM, height, true);
                        tryInputSide(sides.RIGHT, height, true);
                        break;
                }
            } 

            if (alt_input != null && alt_output != null)
            {
                return;
            }

            tryInputSide(sides.BOTTOM, height, false);

            if (input != null && output != null)
            {
                switch (output_side)
                {
                    case sides.LEFT:
                        tryInputSide(sides.TOP, height, true);
                        tryInputSide(sides.RIGHT, height, true);
                        break;
                    case sides.TOP:
                        tryInputSide(sides.LEFT, height, true);
                        tryInputSide(sides.RIGHT, height, true);
                        break;
                    case sides.RIGHT:
                        tryInputSide(sides.LEFT, height, true);
                        tryInputSide(sides.TOP, height, true);
                        break;
                }
            }

            if (alt_input != null && alt_output != null)
            {
                return;
            }

            tryInputSide(sides.RIGHT, height, false);

            if (input != null && output != null)
            {
                switch (output_side)
                {
                    case sides.BOTTOM:
                        tryInputSide(sides.LEFT, height, true);
                        tryInputSide(sides.TOP, height, true);
                        break;
                    case sides.LEFT:
                        tryInputSide(sides.BOTTOM, height, true);
                        tryInputSide(sides.TOP, height, true);
                        break;
                    case sides.TOP:
                        tryInputSide(sides.BOTTOM, height, true);
                        tryInputSide(sides.LEFT, height, true);
                        break;
                }
            }

            if (alt_input != null && alt_output != null)
            {
                return;
            }


            tryInputSide(sides.TOP, height, false);


            if (input != null && output != null)
            {
                switch (output_side)
                {
                    case sides.RIGHT:
                        tryInputSide(sides.BOTTOM, height, true);
                        tryInputSide(sides.LEFT, height, true);
                        break;
                    case sides.BOTTOM:
                        tryInputSide(sides.RIGHT, height, true);
                        tryInputSide(sides.LEFT, height, true);
                        break;
                    case sides.LEFT:
                        tryInputSide(sides.RIGHT, height, true);
                        tryInputSide(sides.BOTTOM, height, true);
                        break;
                }
            }

            if (alt_input != null && alt_output != null)
            {
                return;
            }
        }
        private void setOutput(float height, bool alternative)
        {
            if (alternative)
            {
                switch (input_side_alt)
                {
                    case sides.LEFT:
                        trySetOutputSide(sides.TOP, height, alternative);
                        trySetOutputSide(sides.RIGHT, height, alternative);
                        trySetOutputSide(sides.BOTTOM, height, alternative);
                        break;
                    case sides.RIGHT:
                        trySetOutputSide(sides.BOTTOM, height, alternative);
                        trySetOutputSide(sides.LEFT, height, alternative);
                        trySetOutputSide(sides.TOP, height, alternative);
                        break;
                    case sides.BOTTOM:
                        trySetOutputSide(sides.LEFT, height, alternative);
                        trySetOutputSide(sides.TOP, height, alternative);
                        trySetOutputSide(sides.RIGHT, height, alternative);
                        break;
                    case sides.TOP:
                        trySetOutputSide(sides.RIGHT, height, alternative);
                        trySetOutputSide(sides.BOTTOM, height, alternative);
                        trySetOutputSide(sides.LEFT, height, alternative);
                        break;
                }
            } else
            {
                switch (input_side)
                {
                    case sides.LEFT:
                        trySetOutputSide(sides.TOP, height, alternative);
                        trySetOutputSide(sides.RIGHT, height, alternative);
                        trySetOutputSide(sides.BOTTOM, height, alternative);
                        break;
                    case sides.RIGHT:
                        trySetOutputSide(sides.BOTTOM, height, alternative);
                        trySetOutputSide(sides.LEFT, height, alternative);
                        trySetOutputSide(sides.TOP, height, alternative);
                        break;
                    case sides.BOTTOM:
                        trySetOutputSide(sides.LEFT, height, alternative);
                        trySetOutputSide(sides.TOP, height, alternative);
                        trySetOutputSide(sides.RIGHT, height, alternative);
                        break;
                    case sides.TOP:
                        trySetOutputSide(sides.RIGHT, height, alternative);
                        trySetOutputSide(sides.BOTTOM, height, alternative);
                        trySetOutputSide(sides.LEFT, height, alternative);
                        break;
                }
            }
        }

        public List<Point> getInputAndOutput()
        {
            return new List<Point>() { input, output };
        }
        public List<Point> getAltInputAndOutput()
        {
            return new List<Point>() { alt_input, alt_output };
        }
    }
}
