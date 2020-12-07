using System;
using System.Collections.Generic;
using System.Text;

namespace PietSharp.Core.ExtensionMethods
{
    public static class MathExt
    {
        /// <summary>
        /// Computes a proper modulo rather than a remainder.
        /// </summary>
        /// <param name="a">the dividend</param>
        /// <param name="n">the divisor</param>
        /// <returns>the modulus</returns>
        public static int Mod(int a, int n)
        {
            // kudos to Erdal G of Stackoverflow - https://stackoverflow.com/a/61524484

            return (((a %= n) < 0) && n > 0) || (a > 0 && n < 0) ? a + n : a;
        }
    }
}
