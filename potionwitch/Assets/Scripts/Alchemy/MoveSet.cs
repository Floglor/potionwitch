using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Alchemy
{
    [Serializable]
    public class MoveSet
    {
        [SerializeField] private List<Move> moves = new List<Move>();

        public List<Move> Moves => moves;

        public void Add(Move move)
        {
            Moves.Add(move);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Move move in moves)
            {
                stringBuilder.AppendLine(move.ToString());
            }

            return stringBuilder.ToString();
        }
    }
}