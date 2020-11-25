using System;
using System.Collections.Generic;
using System.Text;

namespace PietSharp.Core.ExtensionMethods
{
    public static class StackExt
    {

        public static bool TryPop<T>(this Stack<T> stack, out T result)
        {
            result = default;
            if (stack.Count < 1) return false;

            result = stack.Pop();

            return true;
        }

        public static bool TryPeek<T>(this Stack<T> stack, out T result)
        {
            result = default;
            if (stack.Count < 1) return false;

            result = stack.Peek();

            return true;
        }

        public static bool TryPop2<T>(this Stack<T> stack, out (T arg1, T arg2) results)
        {
            results = default;
            if (stack.Count < 2) return false;

            results.arg1 = stack.Pop();
            results.arg2 = stack.Pop();

            return true;
        }

        public static bool RotateRight<T>(this Stack<T> stack, int depth, int iterations)
        {
            if (depth > stack.Count) return false;
            // if we need to rotate 3 items 7 items, then we can skip the full cycles and just the the 1
            int absoluteIterations = iterations % depth; 

            Stack<T> stack1 = new Stack<T>(absoluteIterations);
            Stack<T> stack2 = new Stack<T>(depth - absoluteIterations);
            for (var i = 0; i < depth; i++)
            {
                if (i < absoluteIterations)
                {
                    stack1.Push(stack.Pop());
                }
                else
                {
                    stack2.Push(stack.Pop());
                }
            }

            while (stack1.Count > 0)
            {
                stack.Push(stack1.Pop());
            }

            while (stack2.Count > 0)
            {
                stack.Push(stack2.Pop());
            }

            return true;
        }

        public static bool RotateLeft<T>(this Stack<T> stack, int depth, int iterations)
        {
            if (depth > stack.Count) return false;
            // if we need to rotate 3 items 7 items, then we can skip the full cycles and just the the 1
            int absoluteIterations = iterations % depth;

            Stack<T> stack1 = new Stack<T>(absoluteIterations);
            Stack<T> stack2 = new Stack<T>(depth - absoluteIterations);
            for (var i = depth; i > 0; i--)
            {
                if (i <= absoluteIterations)
                {
                    stack1.Push(stack.Pop());
                }
                else
                {
                    stack2.Push(stack.Pop());
                }
            }

            while (stack2.Count > 0)
            {
                stack.Push(stack2.Pop());
            }

            while (stack1.Count > 0)
            {
                stack.Push(stack1.Pop());
            }

            return true;
        }
    }
}
