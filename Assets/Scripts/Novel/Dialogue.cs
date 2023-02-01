using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Novel
{
    [InfoBox("Default(None) Character means that this is the withes line")]
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [ListDrawerSettings(CustomAddFunction = "AddCharacterAndSpriteFromPrevious")]
        public List<Line> Lines;
        
        public Line AddCharacterAndSpriteFromPrevious()
        {
            Line line = new Line();
            
            if (Lines.Count <= 0) return line;
            
            line.TargetCharacter = Lines[Lines.Count - 1].TargetCharacter;
            line.CharacterSprite = Lines[Lines.Count - 1].CharacterSprite;
            
            return line;
        }
    }
}