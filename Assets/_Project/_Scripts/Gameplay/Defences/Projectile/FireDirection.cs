using System;

namespace _Project._Scripts.Defences.Projectile
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