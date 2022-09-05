using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataVforBlazor
{
    public class DvPoint
    {
        public double X;
        public double Y;
        public DvPoint(double x, double y)
        {
            X = Math.Round(x, 5);
            Y = Math.Round(y, 5);
        }
    }
}
