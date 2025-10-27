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
        ForwardLeft = 1 << 4,
        ForwardRight = 1 << 5,
        BackLeft = 1 << 6,
        BackRight = 1 << 7
    }
}