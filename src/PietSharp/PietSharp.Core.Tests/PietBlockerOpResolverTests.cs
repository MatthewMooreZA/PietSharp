using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Models;
using Xunit;

namespace PietSharp.Core.Tests
{
    public class PietBlockerOpResolverTests
    {
        [Theory]
        [InlineData(0xFFC0C0, 0xFFC0C0, PietOps.Noop)]
        [InlineData(0x00C0C0, 0x00C0C0, PietOps.Noop)]
        [InlineData(0xFFFF00, 0xC0C000, PietOps.Push)]
        [InlineData(0xC0C000, 0xFFFF00, PietOps.Pop)]
        [InlineData(0xC000C0, 0xC00000, PietOps.Add)]
        public void BasicColourResolver(uint colour1, uint colour2, PietOps expectedOperation)
        {
            PietBlockOpResolver opsResolver = new PietBlockOpResolver();

            var block1 = new PietBlock(colour1, true);
            var block2 = new PietBlock(colour2, true);

            var operation = opsResolver.Resolve(block1, block2);

            Assert.Equal(expectedOperation, operation);
        }
    }
}
