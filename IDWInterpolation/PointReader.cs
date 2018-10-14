using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDWInterpolation
{
    public class PointReader
    {
        private string path;
        private List<Point> puncteCunoscute = new List<Point>();

        public PointReader(string path)
        {
            this.path = path;
        }



        public event EventHandler PointsLoaded;
        protected virtual void OnPointsLoaded(EventArgs e)
        {
            if (PointsLoaded != null)
                PointsLoaded(this, e);
        }


        public List<Point> getKnownPoints()
        {
            return this.puncteCunoscute;
        }

        public float[] getGridLimits()
        {
            float[] xMinMax = getXMinMax(puncteCunoscute);
            float[] yMinMax = getYMinMax(puncteCunoscute);


            float[] limits = new float[4];
            limits[0] = xMinMax[0];
            limits[1] = xMinMax[1];

            limits[2] = yMinMax[0];
            limits[3] = yMinMax[1];

            return limits;
        }

        public void readText()
        {
            int counter = 0;
            string line;

            // Read the file and display it line by line.  
            System.IO.StreamReader file =
                new System.IO.StreamReader(path);
            while ((line = file.ReadLine()) != null)
            {
                string[] results = line.Split(null);
                Point p = new Point(float.Parse(results[0]), float.Parse(results[1]), float.Parse(results[2]));
                puncteCunoscute.Add(p);
                counter++;
            }

            file.Close();
            OnPointsLoaded(new EventArgs());
        }
        public float[] getXMinMax(List<Point> puncte)
        {
            float[] xCoords = new float[puncte.Count];

            float[] minMax = new float[2];

            for (int i = 0; i < puncte.Count; i++)
            {
                xCoords[i] = puncte.ElementAt(i).getX();
            }

            minMax[0] = xCoords.Min();
            minMax[1] = xCoords.Max();

            return minMax;
        }
        public float[] getYMinMax(List<Point> puncte)
        {
            float[] yCoords = new float[puncte.Count];

            float[] minMax = new float[2];

            for (int i = 0; i < puncte.Count; i++)
            {
                yCoords[i] = puncte.ElementAt(i).getY();
            }

            minMax[0] = yCoords.Min();
            minMax[1] = yCoords.Max();

            return minMax;
        }
    }
}
