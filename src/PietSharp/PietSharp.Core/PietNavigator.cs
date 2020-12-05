using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Models;

namespace PietSharp.Core
{
    public class PietNavigator
    {
        public PietNavigator(uint[,] data, int maxSteps = 500000)
        {
            _data = data;
            _width = _data.GetLength(1);
            _height = _data.GetLength(0);
            _maxSteps = maxSteps;
        }

        private readonly int _maxSteps;
        public int StepCount { get; private set; } = 0;

        public (int x, int y) CurrentPoint { get; private set; } = (0, 0);

        public bool TryNavigate(PietBlock block, out (int x, int y) result)
        {
            if (StepCount > _maxSteps)
            {
                result = default;
                // todo: log warning
                return false;
            }
            int failureCount = 0;

            bool moveStraight = block.Colour == White || !block.KnownColour;

            while (failureCount < 8)
            {
                (int x, int y) exitPoint = (Direction, CodelChooser) switch
                {
                    _ when moveStraight => CurrentPoint,
                    (Direction.East, CodelChoice.Left)  => block.EastLeft,
                    (Direction.East, CodelChoice.Right) => block.EastRight,

                    (Direction.South, CodelChoice.Left) => block.SouthLeft,
                    (Direction.South, CodelChoice.Right) => block.SouthRight,

                    (Direction.West, CodelChoice.Left) => block.WestLeft,
                    (Direction.West, CodelChoice.Right) => block.WestRight,

                    (Direction.North, CodelChoice.Left) => block.NorthLeft,
                    (Direction.North, CodelChoice.Right) => block.NorthRight,
                    _ => throw new NotImplementedException(),
                };

                if (moveStraight)
                {
                    bool StillInBlock()
                    {
                        return exitPoint.x >= 0 &&
                               exitPoint.y >= 0 &&
                               exitPoint.x < _width &&
                               exitPoint.y < _height &&
                               block.ContainsPixel(exitPoint.x, exitPoint.y);
                    }


                    (int x, int y) prevStep = exitPoint;
                    while (StillInBlock())
                    {
                        prevStep = exitPoint;
                        switch (Direction)
                        {
                            case Direction.East:
                                exitPoint.x++;
                                break;
                            case Direction.South:
                                exitPoint.y++;
                                break;
                            case Direction.West:
                                exitPoint.x--;
                                break;
                            case Direction.North:
                                exitPoint.y--;
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }

                    // we've crossed the boundary, one step back to be on the edge
                    exitPoint = prevStep;
                }

                (int x, int y) nextStep = Direction switch
                {
                    Direction.East => (exitPoint.x + 1, exitPoint.y),
                    Direction.South => (exitPoint.x, exitPoint.y + 1),
                    Direction.West => (exitPoint.x - 1, exitPoint.y),
                    Direction.North => (exitPoint.x, exitPoint.y - 1),
                    _ => throw new ArgumentOutOfRangeException()
                };

                bool isOutOfBounds = nextStep.x < 0 ||
                                     nextStep.y < 0 ||
                                     nextStep.x >= _width ||
                                     nextStep.y >= _height;

                // you're blocked if the target is a black codel or you're out of bounds
                bool isBlocked = isOutOfBounds || _data[nextStep.y, nextStep.x] == Black;

                if (!isBlocked)
                {
                    CurrentPoint = nextStep;
                    result = nextStep;
                    StepCount++;
                    return true;
                }

                CurrentPoint = exitPoint;

                if (failureCount % 2 == 0)
                {
                    ToggleCodelChooser(1);
                }
                else
                {
                    RotateDirectionPointer(1);
                }

                failureCount++;
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Rotates abs(turns) times. In turns is positive rotates clockwise otherwise counter clockwise
        /// </summary>
        /// <param name="turns">I</param>
        public void RotateDirectionPointer(int turns)
        {
            Direction = (Direction)((int)(Direction + turns) % 4);
        }

        public void ToggleCodelChooser(int times)
        {
            CodelChooser = (CodelChoice)((int)(CodelChooser + Math.Abs(times)) % 2);
        }

        public Direction Direction { get; private set; } = Direction.East;
        public CodelChoice CodelChooser { get; private set; } = CodelChoice.Left;

        private readonly uint[,] _data;

        private const uint White = 0xFFFFFF;
        private const uint Black = 0x000000;

        private readonly int _width;
        private readonly int _height;
    }
}
