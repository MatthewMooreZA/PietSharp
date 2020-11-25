using System;
using System.Collections.Generic;
using System.Linq;
using PietSharp.Core.ExtensionMethods;
using Xunit;

namespace PietSharp.Core.Tests
{
    public class StackExtensionTests
    {
        // a note on stack IEnumerable initialization
        // the order in the array is the order it gets PUSHED, not their position on the stack
        // i.e. flip them when you visualize the stack

        [Fact]
        public void BasicRotateRightTest()
        {
            Stack<int> stack = new Stack<int>(new [] {3, 2, 1});

            stack.RotateRight(3, 1);

            var expect = new[] {2, 3, 1};

            var results = stack.ToArray();

            Assert.Equal(expect, results);
        }

        [Fact]
        public void PartialRotateRightTest()
        {
            Stack<int> stack = new Stack<int>(new[] {4, 3, 2, 1 });

            stack.RotateRight(3, 1);

            var expect = new[] { 2, 3, 1, 4};

            var results = stack.ToArray();

            Assert.Equal(expect, results);
        }

        [Fact]
        public void FullRotateRightTest()
        {
            Stack<int> stack = new Stack<int>(new[] { 3, 2, 1 });

            stack.RotateRight(3, 3);

            // full shuffle should end up back in the order it started
            var expect = new[] { 1, 2, 3 };

            var results = stack.ToArray();

            Assert.Equal(expect, results);
        }

        [Fact]
        public void RotateRightInvalidDepthTest()
        {
            Stack<int> stack = new Stack<int>(new[] { 3, 2, 1 });

            int stackSize = stack.Count;

            var rotated = stack.RotateRight(4, 3);

            Assert.False(rotated);

            Assert.Equal(stackSize, stack.Count);
        }

        [Fact]
        public void BasicRotateLeftTest()
        {
            Stack<int> stack = new Stack<int>(new[] { 3, 2, 1 });

            stack.RotateLeft(3, 1);

            var expect = new[] { 3, 1, 2 };

            var results = stack.ToArray();

            Assert.Equal(expect, results);
        }

        [Fact]
        public void PartialRotateLeftTest()
        {
            Stack<int> stack = new Stack<int>(new[] { 4, 3, 2, 1 });

            stack.RotateLeft(3, 1);

            var expect = new[] { 3, 1, 2, 4 };

            var results = stack.ToArray();

            Assert.Equal(expect, results);
        }

        [Fact]
        public void FullRotateLeftTest()
        {
            Stack<int> stack = new Stack<int>(new[] { 3, 2, 1 });

            stack.RotateLeft(3, 3);

            // full shuffle should end up back in the order it started
            var expect = new[] { 1, 2, 3 };

            var results = stack.ToArray();

            Assert.Equal(expect, results);
        }

        [Fact]
        public void RotateLeftInvalidDepthTest()
        {
            Stack<int> stack = new Stack<int>(new[] { 3, 2, 1 });

            int stackSize = stack.Count;

            var rotated = stack.RotateLeft(4, 3);

            Assert.False(rotated);

            Assert.Equal(stackSize, stack.Count);
        }
    }
}
