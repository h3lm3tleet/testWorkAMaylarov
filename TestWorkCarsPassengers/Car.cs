using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWorkCarsPassengers
{
    public class Car
    {
        public string Name { get; set; }
        public Coordinates Coordinate { get; set; }
        public Player Driver { get; set; }
        public List<Player> Passengers { get; set; } = new List<Player>();
    }
}
