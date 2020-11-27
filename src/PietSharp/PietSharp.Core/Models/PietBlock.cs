using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PietSharp.Core.Models
{
    public class PietBlock
    {
        public PietBlock(uint colour)
        {
            Colour = colour;
            _pixels = new HashSet<(int x, int y)>();
        }
        public int BlockCount => _pixels.Count;
        public uint Colour { get; }

        public bool AddPixel(int x, int y)
        {
            if (_pixels.Add((x, y)))
            {
                UpdateBoundaries(x, y);
                return true;
            }

            return false;
        }

        private void UpdateBoundaries(int x, int y)
        {
            if (x <= TopLeft.x && y <= TopLeft.y)
            {
                TopLeft = (x, y);
            }

            if (x >= TopRight.x && y <= TopRight.y)
            {
                TopRight = (x, y);
            }

            if (x <= BottomLeft.x && y >= BottomLeft.y)
            {
                BottomLeft = (x, y);
            }

            if (x >= BottomRight.x && y >= BottomRight.y)
            {
                BottomRight = (x, y);
            }
        }

        public bool ContainsPixel(int x, int y)
        {
            return _pixels.Contains((x, y));
        }

        public (int x, int y) TopLeft { get; private set; } = (int.MaxValue, int.MaxValue);
        public (int x, int y) TopRight { get; private set; } = (int.MinValue, int.MaxValue);
        public (int x, int y) BottomLeft { get; private set; } = (int.MaxValue, int.MinValue);
        public (int x, int y) BottomRight { get; private set; } = (int.MinValue, int.MinValue);

        private readonly HashSet<(int x, int y)> _pixels;
    }
}
