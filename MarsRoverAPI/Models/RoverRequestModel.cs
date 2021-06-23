namespace MarsRoverAPI.Models
{
    public class RoverRequestModel
    {
        public int PlateauBorderX { get; set; }
        public int PlateauBorderY { get; set; }
        public string RoverCommand { get; set; }
        public Direction RoverDirection { get; set; }
        public int RoverStartPositionX { get; set; }
        public int RoverStartPositionY { get; set; }
    }
}