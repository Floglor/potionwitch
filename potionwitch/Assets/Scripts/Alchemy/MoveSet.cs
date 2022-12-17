using System.Collections.Generic;
using UnityEngine;

namespace Alchemy
{
    [CreateAssetMenu(fileName = "Move Set", menuName = "Move Set")]
    public class MoveSet : ScriptableObject
    {
        [SerializeField] private List<Move> moves;
    
        public List<Move> Moves => moves;
    }
}