using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDWInterpolation
{
    public class GridPoints
    {
        private List<Point> gridPoints = new List<Point>();
        private float topLeft, topRight, bottomLeft, bottomRight;
        private float stepX, stepY;
        private int numSteps;

        public event EventHandler GridCreated;
        protected virtual void OnGridCreated(EventArgs e)
        {
            if (GridCreated != null)
                GridCreated(this, e);
        }

        public GridPoints(float topLeft, float topRight, float bottomLeft, float bottomRight, int numSteps)
        {
            this.topLeft = topLeft;
            this.topRight = topRight;
            this.bottomLeft = bottomLeft;
            this.bottomRight = bottomRight;
            this.numSteps = numSteps;
        }

        public void createGrid()
        {
            this.stepX = (topRight - topLeft) / numSteps;
            this.stepY = (bottomRight - bottomLeft) / numSteps;

            for (int i = 0; i<numSteps; i++)
            {
                for (int j = 0; j<numSteps; j++)
                {
                    float x = topLeft + i * stepX;
                    float y = bottomLeft + j * stepY;
                    gridPoints.Add(new Point(x, y, 0));
                }
            }

            OnGridCreated(new EventArgs());
        }
        public int getNumSteps()
        {
            return this.numSteps;
        }

        public List<Point> getGridPoints()
        {
            return gridPoints;
        }
    }
}
