using Xunit;
using Newtonsoft.Json;
using MarsRoverAPI.Models;
using System.Threading.Tasks;
using Pathoschild.Http.Client;
using System.Collections.Generic;

namespace MarsRoverAPI.Tests
{
    public class RoverTests
    {
        [Theory, ClassData(typeof(RoverTestTheoryData))]
        public async Task GetRoverPosition_ShouldAssertTrue_WhenPassedCorrectCommand(RoverTestParameter parameter)
        {
            var result = new List<RoverResponseModel>();
            using var client = new FluentClient("https://localhost:44319/");
            foreach (var rover in parameter.Rovers)
            {
                var response = await client
                    .GetAsync("rovers/v1")
                    .WithArgument("PlateauBorderX", parameter.PlateauBorderX)
                    .WithArgument("PlateauBorderY", parameter.PlateauBorderY)
                    .WithArgument("RoverCommand", rover.Command)
                    .WithArgument("RoverDirection", rover.Direction)
                    .WithArgument("RoverStartPositionX", rover.RoverStartPositionX)
                    .WithArgument("RoverStartPositiony", rover.RoverStartPositionY)
                    .As<RoverResponseModel>();
                result.Add(response);
            }

            string actual = JsonConvert.SerializeObject(result),
                expected = JsonConvert.SerializeObject(parameter.Expected);

            Assert.Equal(expected, actual);
        }
    }
}