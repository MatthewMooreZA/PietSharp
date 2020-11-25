using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.ExtensionMethods;

namespace PietSharp.Core
{
    public class PietStack
    {
        public void Push(int value)
        {
            _stack.Push(value);
        }

        public void Pop()
        {
            _stack.TryPop(out _ );
        }

        public void Add()
        {
            ApplyTernary((s1, s2) => s1 + s2);
        }

        public void Subtract()
        {
            ApplyTernary((s1, s2) => s2 - s1);
        }

        public void Multiply()
        {
            ApplyTernary((s1, s2) => s1 * s2);
        }

        public void Divide()
        {
            ApplyTernaryIf(
                (s1, s2) => s2 / s1, 
                (_, s2) => s2 != 0
            );
        }

        public void Mod()
        {
            ApplyTernaryIf(
                (s1, s2) => (s2 - s1)*(s2 / s1),
                (_, s2) => s2 != 0
            );
        }

        public void Not()
        {
            if (!_stack.TryPop(out var result)) return;

            this.Push(result == 0 ? 1 : 0);
        }

        public void Greater()
        {
            ApplyTernary((s1, s2) => s2 > s1 ? 1 : 0);
        }

        public void Duplicate()
        {
            if (!_stack.TryPop(out var result)) return;
            this.Push(result);
        }

        private void ApplyTernary(Func<int, int, int> operatorFunc)
        {
            if (!_stack.TryPop2(out var stackResults)) return;
            var (top, second) = stackResults;
            
            var result = operatorFunc.Invoke(top, second);
            this.Push(result);
        }

        private void ApplyTernaryIf(Func<int, int, int> operatorFunc, Func<int, int, bool> conditionalFunc)
        {
            if (!_stack.TryPop2(out var stackResults)) return;
            var (top, second) = stackResults;

            if (!conditionalFunc.Invoke(top, second)) return;

            var result = operatorFunc.Invoke(top, second);
            this.Push(result);

        }

        public void Roll()
        {
            if(!_stack.TryPop2(out var stackResults)) return;

            var (numberOfRolls, depthOfRoll) = stackResults;
            int absNumberOfRolls = Math.Abs(numberOfRolls);

        }

        private readonly Stack<int> _stack = new Stack<int>();

    }
}
