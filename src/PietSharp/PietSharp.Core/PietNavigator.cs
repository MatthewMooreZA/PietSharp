using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Models;

namespace PietSharp.Core
{
    public class PietNavigator
    {
        public PietNavigator(uint[,] data)
        {
            _data = data;
            _width = _data.GetLength(1);
            _height = _data.GetLength(0);
        }

        public (int x, int y) CurrentPoint { get; private set; } = (0, 0);

        public bool TryNavigate(PietBlock block, out (int x, int y) result)
        {
            int failureCount = 0;
            while (failureCount < 8)
            {
                (int x, int y) exitPoint = (_direction, _codelChooser) switch
                {
                    _ when block.Colour == White => CurrentPoint,
                    (Direction.Right, CodelChoice.Left)  => block.TopRight.x >= block.BottomRight.x ? block.TopRight : block.BottomRight,
                    (Direction.Right, CodelChoice.Right) => block.BottomRight.x >= block.TopRight.x ? block.BottomRight : block.TopRight,

                    (Direction.Down, CodelChoice.Left) => block.BottomRight.y >= block.BottomLeft.y ? block.BottomRight : block.BottomLeft,
                    (Direction.Down, CodelChoice.Right) => block.BottomLeft.y >= block.BottomRight.y ? block.BottomLeft : block.BottomRight,

                    (Direction.Left, CodelChoice.Left) => block.BottomLeft.x <= block.TopLeft.x ? block.BottomLeft : block.TopLeft,
                    (Direction.Left, CodelChoice.Right) => block.TopLeft.x <= block.BottomLeft.x ? block.TopLeft : block.BottomLeft,

                    (Direction.Up, CodelChoice.Left) => block.TopLeft.y <= block.TopRight.y ? block.TopLeft : block.TopRight,
                    (Direction.Up, CodelChoice.Right) => block.TopRight.y <= block.TopLeft.y ? block.TopRight : block.TopLeft,
                    _ => throw new NotImplementedException(),
                };

                if (block.Colour == White)
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
                        switch (_direction)
                        {
                            case Direction.Right:
                                exitPoint.x++;
                                break;
                            case Direction.Down:
                                exitPoint.y++;
                                break;
                            case Direction.Left:
                                exitPoint.x--;
                                break;
                            case Direction.Up:
                                exitPoint.y--;
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }

                    // we've crossed the boundary, one step back to be on the edge
                    exitPoint = prevStep;
                }

                (int x, int y) nextStep = _direction switch
                {
                    Direction.Right => (exitPoint.x + 1, exitPoint.y),
                    Direction.Down => (exitPoint.x, exitPoint.y + 1),
                    Direction.Left => (exitPoint.x - 1, exitPoint.y),
                    Direction.Up => (exitPoint.x, exitPoint.y - 1),
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
                    return true;
                }

                CurrentPoint = exitPoint;

                if (failureCount % 2 == 0)
                {
                    _codelChooser = _codelChooser == CodelChoice.Left ? CodelChoice.Right : CodelChoice.Left;
                }
                else
                {
                    _direction = (Direction)((int)(_direction + 1) % 4);
                }

                failureCount++;
            }

            result = default;
            return false;
        }


        private readonly uint[,] _data;
        private CodelChoice _codelChooser = CodelChoice.Left;
        private Direction _direction = Direction.Right;

        private const uint White = 0xFFFFFF;
        private const uint Black = 0x000000;

        private readonly int _width;
        private readonly int _height;
    }
}
