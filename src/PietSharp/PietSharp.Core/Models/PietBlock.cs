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
            return _pixels.Add((x, y));
        }

        public bool ContainsPixel(int x, int y)
        {
            return _pixels.Contains((x, y));
        }

        private readonly HashSet<(int x, int y)> _pixels;
    }
}
