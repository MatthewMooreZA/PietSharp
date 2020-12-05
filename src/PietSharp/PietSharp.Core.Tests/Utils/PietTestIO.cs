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

        public PietTestIO(int maxOutput = 100):this(new List<string>())
        {
            MaxOutput = maxOutput;
        }

        public PietTestIO(List<string> inputs, int maxOutput)
        {
            MaxOutput = maxOutput;
            OutputStream = new List<string>();
            InputStream = inputs;
        }

        public int MaxOutput { get; set; }
        public List<string> OutputStream { get; set; } 
        public List<string> InputStream { get; set; }
        public void Output(object value)
        {
            OutputStream.Add(value.ToString());
            if (MaxOutput > 0 && OutputStream.Count > MaxOutput)
            {
                throw new Exception("Exceeded allowed output");
            }
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

        public char? ReadChar()
        {
            return null;
        }

        public string OutputString()
        {
            return string.Concat(OutputStream);
        }
    }
}
