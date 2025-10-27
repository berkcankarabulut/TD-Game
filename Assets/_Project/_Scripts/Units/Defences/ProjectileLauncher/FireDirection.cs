using System;

namespace _Project._Scripts.Units.Defence
{
    [Flags]
    public enum FireDirection
    { 
        Forward = 1 << 0,
        Back = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3, 
    }
}