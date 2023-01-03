using System.Collections.Generic;
using UnityEngine;

namespace Novel
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public List<Line> Lines;
    }
}