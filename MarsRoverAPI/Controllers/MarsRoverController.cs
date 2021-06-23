using MarsRoverAPI.Extensions;
using MarsRoverAPI.Models;
using MarsRoverAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Drawing;

namespace MarsRoverAPI.Controllers
{
    [ApiController]
    [Route("rovers/v1")]
    public class MarsRoverController : ControllerBase
    {
        private readonly IRoverService _rover;
        private readonly ILogger<MarsRoverController> _logger;

        public MarsRoverController(ILogger<MarsRoverController> logger, IRoverService rover)
        {
            _rover = rover;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] RoverRequestModel model)
        {
            _logger.LogInformation("Request received for Mars Rover");

            if (model == null)
            {
                _logger.LogError("Parameter cannot be null");
                return BadRequest("Parameter cannot be null");
            }

            if (!model.RoverCommand.CommandValidator())
            {
                _logger.LogError("Incorrect rover command: {0}", model.RoverCommand);
                return BadRequest($"Incorrect rover command : {model.RoverCommand}");
            }
            try
            {
                _logger.LogTrace("Rover Start Position X: {0} Y: {1} Direction: {2}", model.RoverStartPositionX, model.RoverStartPositionY, model.RoverDirection);
                _rover.StartEngine(new Point(model.RoverStartPositionX, model.RoverStartPositionY), new Point(model.PlateauBorderX, model.PlateauBorderY), model.RoverDirection, model.RoverCommand);
                _logger.LogTrace("The rover has completed its movement");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred in the Mars Rover operation. Error message: {0}", ex.Message);
                return StatusCode(500, ex.Message);
            }

            var response = _rover.GetCurrentPosition();
            _logger.LogTrace("Rover Finish Position X: {0} Y: {1} Direction: {2}", response.RoverPositionX, response.RoverPositionY, response.Direction);
            _logger.LogInformation("Request completed for Mars Rover");
            return Ok(response);
        }
    }
}