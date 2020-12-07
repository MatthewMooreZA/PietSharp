using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PietSharp.Core.Tests
{
    public class PietStackTests
    {
        [Theory]
        [InlineData(5, 3, 2)]
        [InlineData(2, 3, 2)]
        [InlineData(-1, 3, 2)]
        [InlineData(-4, 3, 2)]
        [InlineData(7, 42, 7)]
        public void StackModOpTest(int value1, int value2, int expectation)
        {
            // compute value 2 mod value 1

            PietStack stack = new PietStack();

            stack.Push(value1);
            stack.Push(value2);

            stack.Mod();

            var result = stack.Pop();

            Assert.Equal(expectation, result);
        }
    }
}
