using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PietSharp.Core.Models
{
    public class PietBlock
    {
        public PietBlock(uint colour, bool knownColour)
        {
            Colour = colour;
            KnownColour = knownColour;
            _pixels = new HashSet<(int x, int y)>();
        }
        public int BlockCount => _pixels.Count;
        public uint Colour { get; }
        public bool KnownColour { get; }
        public bool AddPixel(int x, int y)
        {
            if (_pixels.Add((x, y)))
            {
                return true;
            }

            return false;
        }

        public bool ContainsPixel(int x, int y)
        {
            return _pixels.Contains((x, y));
        }

        public (int x, int y) NorthLeft
        {
            get
            {
                return _pixels.OrderBy(p => p.y).ThenBy(p => p.x).First();
            }
        }

        public (int x, int y) NorthRight
        {
            get
            {
                return _pixels.OrderBy(p => p.y).ThenByDescending(p => p.x).First();
            }
        }

        public (int x, int y) EastLeft
        {
            get
            {
                return _pixels.OrderByDescending(p => p.x).ThenBy(p => p.y).First();
            }
        }

        public (int x, int y) EastRight
        {
            get
            {
                return _pixels.OrderByDescending(p => p.x).ThenByDescending(p => p.y).First();
            }
        }

        public (int x, int y) SouthLeft
        {
            get
            {
                return _pixels.OrderByDescending(p => p.y).ThenByDescending(p => p.x).First();
            }
        }

        public (int x, int y) SouthRight
        {
            get
            {
                return _pixels.OrderByDescending(p => p.y).ThenBy(p => p.x).First();
            }
        }

        public (int x, int y) WestLeft
        {
            get
            {
                return _pixels.OrderBy(p => p.x).ThenByDescending(p => p.y).First();
            }
        }

        public (int x, int y) WestRight
        {
            get
            {
                return _pixels.OrderBy(p => p.x).ThenBy(p => p.y).First();
            }
        }

        private readonly HashSet<(int x, int y)> _pixels;
    }
}
