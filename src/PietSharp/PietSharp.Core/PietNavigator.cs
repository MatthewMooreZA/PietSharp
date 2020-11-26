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

        public bool TryNavigate(PietBlock block, out (int x, int y) result)
        {
            int failureCount = 0;
            while (failureCount < 8)
            {
                var exitPoint = (_direction, _codelChooser) switch
                {
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

                // todo: implement white - it has different step logic
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

                // you're blocked if the target is a block codel or you're out of bounds
                bool isBlocked = isOutOfBounds || _data[exitPoint.y, exitPoint.x] == 0X000000;

                if (!isBlocked)
                {
                    result = nextStep;
                    return true;
                }

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

        private readonly int _width;
        private readonly int _height;
    }
}
