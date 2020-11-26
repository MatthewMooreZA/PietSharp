using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Models;

namespace PietSharp.Core
{
    public class PietSession
    {
        public PietSession(uint[,] data)
        {
            _data = data;
            _builder = new PietBlockerBuilder(_data);
        }


        private readonly uint[,] _data;
        private readonly PietBlockerBuilder _builder;
    }
}
