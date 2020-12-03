using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Contracts;

namespace PietSharp.Core
{
    public class PietConsoleIO: IPietIO
    {
        public void Output(object value)
        {
            Console.Write(value);
        }

        public int? ReadInt()
        {
            if (int.TryParse(Console.ReadLine(), out var result))
            {
                return result;
            }

            return null;
        }

        public char? ReadChar()
        {
            return (char)Console.Read();
        }
    }
}
