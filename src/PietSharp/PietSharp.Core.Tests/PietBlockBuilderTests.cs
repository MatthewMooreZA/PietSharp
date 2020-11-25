using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PietSharp.Core.Tests
{
    public class PietBlockBuilderTests
    {
        [Fact]
        public void BasicTest_FillEntireSpace()
        {
            uint[,] data = new uint[3, 3]
            {
                {0, 0, 0},
                {0, 0, 0},
                {0, 0, 0},
            };

            var blockBuilder = new PietBlockerBuilder(data);

            var block = blockBuilder.GetBlockAt(0, 0);

            Assert.Equal(0u, block.Colour);
            Assert.Equal(9, block.BlockCount);
        }

        [Fact]
        public void BasicTest_LShape()
        {
            uint[,] data = new uint[3, 3]
            {
                {0, 1, 1},
                {0, 1, 1},
                {0, 0, 0},
            };

            var blockBuilder = new PietBlockerBuilder(data);

            var block = blockBuilder.GetBlockAt(0, 0);

            Assert.Equal(0u, block.Colour);
            Assert.Equal(5, block.BlockCount);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(2, 2)]
        [InlineData(4, 4)]
        public void BasicTest_FigureEight(int startX, int startY)
        {
            uint[,] data = new uint[5, 5]
            {
                {0, 0, 0, 1, 1},
                {0, 1, 0, 1, 1},
                {0, 0, 0, 0, 0},
                {1, 1, 0, 1, 0},
                {1, 1, 0, 0, 0},
            };

            var blockBuilder = new PietBlockerBuilder(data);

            var block = blockBuilder.GetBlockAt(startX, startY);

            Assert.Equal(0u, block.Colour);
            Assert.Equal(15, block.BlockCount);
        }

        private uint[,] NestedShape1 => new uint[8, 8]
        {
            {6, 6, 6, 6, 6, 6, 6, 3},
            {6, 3, 3, 3, 6, 3, 6, 6},
            {6, 3, 6, 3, 6, 3, 3, 6},
            {6, 3, 6, 3, 3, 3, 3, 6},
            {6, 3, 6, 6, 6, 6, 3, 6},
            {6, 3, 3, 3, 3, 3, 3, 6},
            {6, 6, 6, 6, 6, 6, 6, 6},
            {6, 6, 6, 6, 6, 6, 6, 6}
        };

        [Theory]
        [InlineData(0, 0, 36)]
        [InlineData(2, 3, 6)]
        [InlineData(1, 1, 21)]
        [InlineData(7, 0, 1)]
        public void NestedShapeTest(int startX, int startY, int count)
        {
            uint[,] data = NestedShape1;

            var blockBuilder = new PietBlockerBuilder(data);

            var block = blockBuilder.GetBlockAt(startX, startY);

            Assert.Equal(count, block.BlockCount);
        }

    }
}
