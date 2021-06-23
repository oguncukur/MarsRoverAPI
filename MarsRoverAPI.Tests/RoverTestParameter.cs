using MarsRoverAPI.Models;
using System.Collections.Generic;

namespace MarsRoverAPI.Tests
{
    public class RoverTestParameter
    {
        public int PlateauBorderX { get; set; }
        public int PlateauBorderY { get; set; }
        public IEnumerable<RoverParameter> Rovers { get; set; }
        public IEnumerable<RoverResponseModel> Expected { get; set; }
    }
}