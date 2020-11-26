using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Models;
using Xunit;

namespace PietSharp.Core.Tests
{
    public class PietNavigatorTests
    {
        [Fact]
        public void SingleBlockTest()
        {
            // super boring single block
            var data = new uint[,]
            {
                {0XFF}
            };

            var block = new PietBlock(0XFF);
            block.AddPixel(0, 0);

            var navigator = new PietNavigator(data);

            bool endNav = navigator.TryNavigate(block, out var result);

            // should run around in its single block 8 times and then quit
            Assert.False(endNav);
        }

        [Fact]
        public void BasicNavigationTest1()
        {
            var data = new uint[,]
            {
                {0XFF, 0X00FF, 0x0000FF}
            }; // R G B :)

            var blockBuilder = new PietBlockerBuilder(data);

            var initialBlock = blockBuilder.GetBlockAt(0, 0);

            var navigator = new PietNavigator(data);

            // R -> G

            Assert.True(navigator.TryNavigate(initialBlock, out var result1));
            Assert.Equal((1,0), result1);

            var blockTwo = blockBuilder.GetBlockAt(result1.x, result1.y);

            // G -> B

            Assert.True(navigator.TryNavigate(blockTwo, out var result2));
            Assert.Equal((2, 0), result2);


            var blockThree = blockBuilder.GetBlockAt(result2.x, result2.y);

            // B -> | <- G 

            Assert.True(navigator.TryNavigate(blockThree, out var result3));
            Assert.Equal((1, 0), result3);
        }

        [Fact]
        public void PickFurthestRightEdgeTest()
        {
            var data = new uint[,]
            {
                {0XFF0000, 0X00FF00, 0x0000FF},
                {0XFF0000, 0XFF0000, 0x0000FF}
            }; 

            var blockBuilder = new PietBlockerBuilder(data);

            var initialBlock = blockBuilder.GetBlockAt(0, 0);

            var navigator = new PietNavigator(data);

            Assert.True(navigator.TryNavigate(initialBlock, out var result));

            Assert.Equal((2,1), result);
        }
    }
}
