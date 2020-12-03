using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Contracts;
using PietSharp.Core.Models;

namespace PietSharp.Core
{
    public class BaseOperations
    {
        private PietStack _stack;

        private Func<PietBlock> _getExitedBlock;

        private readonly IPietIO _io;

        public BaseOperations(PietStack stack, IPietIO io, Func<PietBlock> getExitedBlock)
        {
            _stack = stack;
            _io = io;
            _getExitedBlock = getExitedBlock;
        }

        /// <summary>
        /// Pushes the value of the colour block just exited on to the stack
        /// </summary>
        public virtual void Push()
        {
            var exitedBlock = _getExitedBlock.Invoke();
            if (exitedBlock == null) return;

            _stack.Push(exitedBlock.BlockCount);
        }

        /// <summary>
        /// Pops the top value off the stack and discards it
        /// </summary>
        public virtual void Pop()
        {
            _stack.Pop();
        }

        public virtual void Add()
        {
            _stack.Add();
        }

        public virtual void Subtract()
        {
            _stack.Subtract();
        }

        public virtual void Multiply()
        {
            _stack.Multiply();
        }

        public virtual void Divide()
        {
            _stack.Divide();
        }

        public virtual void Mod()
        {
            _stack.Mod();
        }

        public virtual void Not()
        {
            _stack.Not();
        }

        public virtual void Greater()
        {
            _stack.Greater();
        }

        public virtual void Pointer()
        {
            throw new NotImplementedException();
        }

        public virtual void Switch()
        {
            throw new NotImplementedException();
        }

        public virtual void Duplicate()
        {
            _stack.Duplicate();
        }

        public virtual void Roll()
        {
            _stack.Roll();
        }

        public virtual void InNumber()
        {
            var val = _io.ReadInt();
            if (val.HasValue)
            {
                _stack.Push(val.Value);
            }
        }

        public virtual void InChar()
        {
            var val = _io.ReadChar();
            if (val.HasValue)
            {
                _stack.Push(val.Value);
            }
        }

        public virtual void OutNumeric()
        {
            var result = _stack.Pop();
            if (result.HasValue)
            {
                _io.Output(result.Value);
            }
        }

        public virtual void OutChar()
        {
            var result = _stack.Pop();
            if (result.HasValue)
            {
                _io.Output((char)result.Value);
            }
        }

        public virtual Dictionary<PietOps, Action> GetMap()
        {
            return new Dictionary<PietOps, Action>
            {
                [PietOps.Push] = this.Push,
                [PietOps.Pop] = this.Pop,
                [PietOps.Add] = this.Add,
                [PietOps.Subtract] = this.Subtract,
                [PietOps.Multiply] = this.Multiply,
                [PietOps.Divide] = this.Divide,
                [PietOps.Mod] = this.Mod,
                [PietOps.Not] = this.Not,
                [PietOps.Greater] = this.Greater,
                [PietOps.Pointer] = this.Pointer,
                [PietOps.Switch] = this.Switch,
                [PietOps.Duplicate] = this.Duplicate,
                [PietOps.Roll] = this.Roll,
                [PietOps.InputChar] = this.InChar,
                [PietOps.InputNumber] = this.InNumber,
                [PietOps.OutputNumber] = this.OutNumeric,
                [PietOps.OutputChar] = this.OutChar
            };
        }
    }
}
