using System;
using System.Collections.Generic;
using System.Text;

namespace PietSharp.Core.Contracts
{
    public interface IPietIO
    {
        /// <summary>
        /// Write out the provided value
        /// </summary>
        /// <param name="value">The value to be written</param>
        public void Output(object value);
        /// <summary>
        /// Reads the next integer input.
        /// </summary>
        /// <returns>User input</returns>
        public int? ReadInt();
        /// <summary>
        /// Reads the next char input.
        /// </summary>
        /// <returns>User input</returns>
        public char? ReadChar();
    }
}
