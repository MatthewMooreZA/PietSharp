using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Tests.Utils;
using Xunit;

namespace PietSharp.Core.Tests
{
    public class PietSessionTests
    {
        [Fact]
        public void BasicSessionTest()
        {
            var data = new uint[,]
            {
                {0xC00000, 0XFF0000, 0XFF0000, 0xC00000, 0xFFC0FF, 0xFFC0FF},
                {0xFF0000, 0xFF0000, 0xC00000, 0xC00000, 0x000000, 0xFFC0FF},
                {0xFF0000, 0xFF0000, 0x000000, 0xFFC0FF, 0xFFC0FF, 0xFFC0FF},
                {0xFF0000, 0xFF0000, 0xC00000, 0x000000, 0x000000, 0x000000},
                {0xFF0000, 0xFF0000, 0x000000, 0xFF0000, 0x000000, 0x000000},
            };


            var io = new PietTestIO();
            var session = new PietSession(data, io);

            session.Run();

            Assert.False(session.Running);

            Assert.Equal("10", io.OutputStream[0]);
        }
    }
}
