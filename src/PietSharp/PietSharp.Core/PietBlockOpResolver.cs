using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Models;

namespace PietSharp.Core
{
    public class PietBlockOpResolver
    {
        /// <summary>
        /// Calculates the operation signified by the transition from block1 to block2
        /// </summary>
        /// <param name="block1">The egress block</param>
        /// <param name="block2">The ingress block</param>
        /// <returns>An operation</returns>
        public PietOps Resolve(PietBlock block1, PietBlock block2)
        {
            if (TryResolveColour(block1.Colour, out var colour1) && TryResolveColour(block2.Colour, out var colour2))
            {
                int lightShift = colour2.darkness - colour1.darkness;
                if (lightShift < 0)
                {
                    lightShift += 3;
                }
                int colourShift = colour2.colour - colour1.colour;
                if (colourShift < 0)
                {
                    colourShift += 6;
                }

                return (colourShift, lightShift) switch
                {
                    (0, 0) => PietOps.Noop,
                    (0, 1) => PietOps.Push,
                    (0, 2) => PietOps.Pop,
                    (1, 0) => PietOps.Add,
                    (1, 1) => PietOps.Subtract,
                    (1, 2) => PietOps.Multiply,
                    (2, 0) => PietOps.Divide,
                    (2, 1) => PietOps.Mod,
                    (2, 2) => PietOps.Not,
                    (3, 0) => PietOps.Greater,
                    (3, 1) => PietOps.Pointer,
                    (3, 2) => PietOps.Switch,
                    (4, 0) => PietOps.Duplicate,
                    (4, 1) => PietOps.Roll,
                    (4, 2) => PietOps.InputNumber,
                    (5, 0) => PietOps.InputChar,
                    (5, 1) => PietOps.OutputNumber,
                    (5, 2) => PietOps.OutputChar,
                    _ => throw new NotImplementedException()
                };
            }

            throw new NotImplementedException();
        }

        private bool TryResolveColour(uint colour, out (HueColour colour, Darkness darkness) result)
        {
            switch (colour)
            {
                // red
                case 0xFFC0C0:
                    result.colour = HueColour.Red;
                    result.darkness = Darkness.Light;
                    return true;
                case 0xFF0000:
                    result.colour = HueColour.Red;
                    result.darkness = Darkness.Normal;
                    return true;
                case 0xC00000:
                    result.colour = HueColour.Red;
                    result.darkness = Darkness.Dark;
                    return true;
                // yellow
                case 0xFFFFC0:
                    result.colour = HueColour.Yellow;
                    result.darkness = Darkness.Light;
                    return true;
                case 0xFFFF00:
                    result.colour = HueColour.Yellow;
                    result.darkness = Darkness.Normal;
                    return true;
                case 0xC0C000:
                    result.colour = HueColour.Yellow;
                    result.darkness = Darkness.Dark;
                    return true;
                // green
                case 0xC0FFC0:
                    result.colour = HueColour.Green;
                    result.darkness = Darkness.Light;
                    return true;
                case 0x00FF00:
                    result.colour = HueColour.Green;
                    result.darkness = Darkness.Normal;
                    return true;
                case 0x00C000:
                    result.colour = HueColour.Green;
                    result.darkness = Darkness.Dark;
                    return true;
                //cyan
                case 0xC0FFFF:
                    result.colour = HueColour.Cyan;
                    result.darkness = Darkness.Light;
                    return true;
                case 0x00FFFF:
                    result.colour = HueColour.Cyan;
                    result.darkness = Darkness.Normal;
                    return true;
                case 0x00C0C0:
                    result.colour = HueColour.Cyan;
                    result.darkness = Darkness.Dark;
                    return true;
                //blue
                case 0xC0C0FF:
                    result.colour = HueColour.Blue;
                    result.darkness = Darkness.Light;
                    return true;
                case 0x0000FF:
                    result.colour = HueColour.Blue;
                    result.darkness = Darkness.Normal;
                    return true;
                case 0x0000C0:
                    result.colour = HueColour.Blue;
                    result.darkness = Darkness.Dark;
                    return true;
                //magenta
                case 0xFFC0FF:
                    result.colour = HueColour.Magenta;
                    result.darkness = Darkness.Light;
                    return true;
                case 0xFF00FF:
                    result.colour = HueColour.Magenta;
                    result.darkness = Darkness.Normal;
                    return true;
                case 0xC000C0:
                    result.colour = HueColour.Magenta;
                    result.darkness = Darkness.Dark;
                    return true;
                default:
                    result = default;
                    return false;
            }
        }
    }
}
