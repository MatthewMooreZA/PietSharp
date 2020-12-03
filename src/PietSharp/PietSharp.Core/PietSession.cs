using System;
using System.Collections.Generic;
using System.Text;
using PietSharp.Core.Contracts;
using PietSharp.Core.Models;

namespace PietSharp.Core
{
    public class PietSession
    {
        public PietSession(uint[,] data, IPietIO io)
        {
            _data = data;

            _builder = new PietBlockerBuilder(_data);
            _navigator = new PietNavigator(_data);
            _opsResolver = new PietBlockOpResolver();
            _stack = new PietStack();

            _currentBlock = _builder.GetBlockAt(0, 0);

            var ops = new BaseOperations(_stack, io, 
                () => _currentBlock, 
                (i) => _navigator.RotateDirectionPointer(i),
                (i) => _navigator.ToggleCodalChooser(i));
            _actionMap = ops.GetMap();
        }

        public bool Running { get; private set; }

        public void Step()
        {
            if (!_navigator.TryNavigate(_currentBlock, out var result))
            {
                Running = false;
            }

            var newBlock = _builder.GetBlockAt(result.x, result.y);

            var opCode = _opsResolver.Resolve(_currentBlock, newBlock);

            if (_actionMap.TryGetValue(opCode, out var action))
            {
                action.Invoke();
            }

            _lastBlock = _currentBlock;
            _currentBlock = newBlock;
        }

        public void Run()
        {
            this.Running = true;
            while (Running)
            {
                Step();
            }
        }

        public Direction DirectionPointer => _navigator.Direction;
        public CodelChoice CodelChooser => _navigator.CodelChooser;

        private PietBlock _currentBlock;
        private PietBlock _lastBlock;
        private readonly Dictionary<PietOps, Action> _actionMap;

        private readonly uint[,] _data;

        private readonly PietStack _stack;
        private readonly PietBlockOpResolver _opsResolver;
        private readonly PietBlockerBuilder _builder;
        private readonly PietNavigator _navigator;
    }
}
