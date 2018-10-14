using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace IDWInterpolation
{
    public class LineToDrawEventArgs : EventArgs
    {
        private Point p1, p2, alt_p1, alt_p2;
        public Point P1 { get { return this.p1; } }
        public Point P2 { get { return this.p2; } }
        public Point Alt_P1 { get { return this.alt_p1; } }
        public Point Alt_p2 { get { return this.alt_p2; } }
        private Color color;
        public Color Color { get { return this.color; } }

        public LineToDrawEventArgs(Point p1, Point p2, Point alt_p1, Point alt_p2, Color color)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.alt_p1 = alt_p1;
            this.alt_p2 = alt_p2;
            this.color = color;
        }
    }
}
