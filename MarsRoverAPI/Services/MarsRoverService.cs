using System;
using System.Drawing;
using MarsRoverAPI.Models;
using System.Collections.Generic;

namespace MarsRoverAPI.Services
{
    public interface IRoverService
    {
        RoverResponseModel GetCurrentPosition();
        void StartEngine(Point startPosition, Point plateau, Direction direction, string commands);
    }

    public class MarsRoverService : IRoverService
    {
        private Point _plateau;
        public Direction _direction;
        private Point _roverPosition;

        private readonly IDictionary<string, Action> movementMethodDictionary;
        private readonly IDictionary<Direction, Action> forwardMoveDictionary;

        public MarsRoverService()
        {
            movementMethodDictionary = new Dictionary<string, Action>()
            {
                { "L", () => TurnLeft() },
                { "R", () => TurnRight() },
                { "M", () => Move() }
            };

            forwardMoveDictionary = new Dictionary<Direction, Action>
            {
                { Direction.North, () => {_roverPosition = new Point(_roverPosition.X, _roverPosition.Y + 1);} },
                { Direction.East, () => {_roverPosition = new Point(_roverPosition.X + 1, _roverPosition.Y);} },
                { Direction.South, () => {_roverPosition = new Point(_roverPosition.X, _roverPosition.Y - 1);} },
                { Direction.West, () => {_roverPosition = new Point(_roverPosition.X - 1, _roverPosition.Y);} }
            };
        }

        public void StartEngine(Point startPosition, Point plateau, Direction direction, string commands)
        {
            _plateau = plateau;
            _direction = direction;
            _roverPosition = startPosition;

            foreach (var command in commands)
            {
                movementMethodDictionary[command.ToString()].Invoke();

                if (_roverPosition.X > _plateau.X || _roverPosition.Y > _plateau.Y)
                {
                    throw new Exception("Rover crossed planetary boundary!");
                }
            }
        }

        private void Move()
        {
            forwardMoveDictionary[_direction].Invoke();
        }

        private void TurnLeft()
        {
            var newDirection = (int)_direction + 90;
            _direction = newDirection.Equals(360) ? 0 : (Direction)newDirection;
        }

        private void TurnRight()
        {
            var newDirection = (int)_direction - 90;
            _direction = newDirection.Equals(-90) ? (Direction)270 : (Direction)newDirection;
        }

        public RoverResponseModel GetCurrentPosition()
        {
            return new RoverResponseModel() { RoverPositionX = _roverPosition.X, RoverPositionY = _roverPosition.Y, Direction = _direction };
        }
    }
}