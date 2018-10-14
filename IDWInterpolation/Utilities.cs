using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDWInterpolation
{
    public class Utilities
    {
        public static float normalizeValueBetweenRange(float value, float min, float max, float a, float b)
        {
            float normalized = (((b - a) * (value - min)) / (max - min)) + a;
            return normalized;
        }

        public static float getDistanceBetween2Points(Point p1, Point p2)
        {
            double xDiff = Math.Pow(Convert.ToDouble(p1.getX() - p2.getX()), 2);
            double yDiff = Math.Pow(Convert.ToDouble(p1.getY() - p2.getY()), 2);
            double distance = Math.Sqrt(xDiff + yDiff);
            return (float)distance;
        }

        public static int getIndexOfClosestValue(List<Point> array, float value) 
        {
            float[] differences = new float[array.Count];
            for (int i = 0; i < array.Count; i++)
            {
                differences[i] = Math.Abs(array.ElementAt(i).getHeight() - value);
            }
            int pointIndex = Array.IndexOf(differences, differences.Min());
            return pointIndex;
        }
    }
}
