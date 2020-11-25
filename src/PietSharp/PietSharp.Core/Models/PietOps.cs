using System;
using System.Collections.Generic;
using System.Text;

namespace PietSharp.Core.Models
{
    public enum PietOps
    {
        Noop,
        Push,
        Pop,
        Add,
        Subtract,
        Multiply,
        Divide,
        Mod,
        Not,
        Greater,
        Pointer,
        Switch,
        Duplicate,
        Roll,
        InputNumber,
        InputChar,
        OutputNumber,
        OutputChar
    }
}
