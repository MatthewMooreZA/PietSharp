using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PietSharp.Core.Contracts;

namespace PietSharp.Core.Tests.Utils
{
    public class PietTestIO : IPietIO
    {
        public PietTestIO(List<string> inputStream)
        {
            OutputStream = new List<string>();
            InputStream = inputStream;
        }

        public PietTestIO():this(new List<string>())
        {
        }

        public List<string> OutputStream { get; set; } 
        public List<string> InputStream { get; set; }
        public void Output(object value)
        {
            OutputStream.Add(value.ToString());
        }

        public int? ReadInt()
        {
            if (!InputStream.Any())
            {
                return null;
            }

            var head = InputStream[0];
            InputStream.RemoveAt(0);

            if (int.TryParse(head, out var result))
            {
                return result;
            }

            return null;
        }

        public char ReadChar()
        {
            throw new NotImplementedException();
        }
    }
}
