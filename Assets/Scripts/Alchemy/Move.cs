using System;

namespace Alchemy
{
    [Serializable]
    public struct Move
    {
        public int X;
        public int Y;

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }
    }
}