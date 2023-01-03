using System;
using System.Collections.Generic;
using Alchemy;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Novel
{
    [Serializable]
    public class Line
    {
        [MultiLineProperty]
        public string Text;
        public Sprite CharacterSprite;
        public bool IsInterrupted;
        
        [ShowIf("IsInterrupted")]
        [EnumToggleButtons]
        public DialogueInterruptType InterruptType;
            
        [ShowIf("@this.IsInterrupted == true && this.InterruptType == DialogueInterruptType.Potion")]
        public Effect interruptionPotion;
        
        [ShowIf("@this.IsInterrupted == true && this.InterruptType == DialogueInterruptType.Choice")]
        public List<Choice> InterruptionChoices;
    }

    public enum DialogueInterruptType
    {
        Choice,
        Potion,
    }
}