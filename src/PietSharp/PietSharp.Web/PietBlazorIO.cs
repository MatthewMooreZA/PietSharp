using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PietSharp.Core.Contracts;

namespace PietSharp.Web
{
    public class PietBlazorIO : IPietIO
    {
        public PietBlazorIO(Action<string> onOutput)
        {
            _pipeOutput = onOutput;
        }
        public void Output(object value)
        {
            _pipeOutput.Invoke(value.ToString());
        }

        public int? ReadInt()
        {
            if (!_inputs.Any())
            {
                return null;
            }
            var head = _inputs[0];
            _inputs.RemoveAt(0);

            if (int.TryParse(head, out var result))
            {
                return result;
            }

            return null;
        }

        public char? ReadChar()
        {
            if (!_inputs.Any())
            {
                return null;
            }
            var head = _inputs[0];

            char result = head.First();

            var rest = head.Skip(1);

            if (rest.Any())
            {
                _inputs[0] = string.Concat(head.Skip(1));
            }
            else
            {
                _inputs.RemoveAt(0);
            }

            return result;
        }

        public void SetInput(string input)
        {
            _inputs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private List<string> _inputs;
        private Action<string> _pipeOutput;
    }
}
