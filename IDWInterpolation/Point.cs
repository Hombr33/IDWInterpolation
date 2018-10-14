using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDWInterpolation
{
    public class Point
    {
        private float x;
        private float y;
        private float height;

        public Point(float x, float y, float height)
        {
            this.x = x;
            this.y = y;
            this.height = height;
        }

        public bool isPointWithinRadius(Point point, float radius)
        {
            double xDiff = Math.Pow(Convert.ToDouble(point.getX() - this.x), 2);
            double yDiff = Math.Pow(Convert.ToDouble(point.getY() - this.y), 2);
            double distance = Math.Sqrt(xDiff + yDiff);

            if (distance > radius)
            {
                return false;
            }

            return true;
        }

        public float getX()
        {
            return x;
        }
        public float getY()
        {
            return y;
        }
        public float getHeight()
        {
            return height;
        }
        public void setHeight(float height)
        {
            this.height = height;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Point;
            if (item == null)
            {
                return false;
            }

            if (item.getX() == this.x && item.getY() == this.y && item.getHeight() == this.height)
            {
                return true;
            } else
            {
                return false;
            }
        }
    }
}
