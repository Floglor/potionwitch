using System;
using UnityEngine;

namespace Alchemy
{
    [Serializable]
    public struct Move
    {
        [Range(-1, 1)]
        public int X;
        [Range(-1, 1)]
        public int Y;

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}