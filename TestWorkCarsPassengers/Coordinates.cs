using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWorkCarsPassengers
{

    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        public double Distance(Coordinates other)
        {
            return Math.Sqrt(Math.Pow(X - other.X, 2) + Math.Pow(Y - other.Y, 2));
        }
    }
}
