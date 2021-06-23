using Xunit;
using MarsRoverAPI.Models;
using System.Collections.Generic;

namespace MarsRoverAPI.Tests
{
    public class RoverTestTheoryData : TheoryData<RoverTestParameter>
    {
        public RoverTestTheoryData()
        {
            Add(new RoverTestParameter
            {
                PlateauBorderX = 5,
                PlateauBorderY = 5,
                Rovers = new List<RoverParameter>()
                {
                    new RoverParameter
                    {
                        Command = "LMLMLMLMM",
                        RoverStartPositionX = 1,
                        RoverStartPositionY = 2,
                        Direction = Direction.North
                    },
                    new RoverParameter
                    {
                        Command = "MMRMMRMRRM",
                        RoverStartPositionX = 3,
                        RoverStartPositionY = 3,
                        Direction = Direction.East,
                    }
                },
                Expected = new List<RoverResponseModel>()
                {
                    new RoverResponseModel
                    {
                        RoverPositionX = 1,
                        RoverPositionY= 3,
                        Direction = Direction.North
                    },
                    new RoverResponseModel
                    {
                        RoverPositionX = 5,
                        RoverPositionY= 1,
                        Direction = Direction.East
                    }
                }
            });
        }
    }
}