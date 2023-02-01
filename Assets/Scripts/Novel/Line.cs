using System;
using System.Collections;
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
        
        public Character TargetCharacter;
        
        [ShowIf("@this.TargetCharacter != null")]
        [AssetSelector(Paths = "Assets/Sprites/Characters")]
        public Sprite CharacterSprite;

        [ShowIf("@this.TargetCharacter != null")]
        [EnumToggleButtons]
        public DialoguePosition Position;

        public bool ChangeUsedSprite;
        
        [ShowIf("@this.ChangeUsedSprite")]
        [EnumToggleButtons]
        public DialoguePosition UsedSpritePosition;

        [ShowIf("@this.ChangeUsedSprite")]
        public bool RemoveCharacterSprite;
        
        [ShowIf("@this.ChangeUsedSprite && !this.RemoveCharacterSprite")]
        [AssetSelector(Paths = "Assets/Sprites/Characters")]
        public Sprite ChangeSpriteTo;
        
        public bool IsInterrupted;
        
        [ShowIf("IsInterrupted")]
        [EnumToggleButtons]
        public DialogueInterruptType InterruptType;
            
        [ShowIf("@this.IsInterrupted == true && this.InterruptType == DialogueInterruptType.Potion")]
        public Effect interruptionPotion;
        
        [ShowIf("@this.IsInterrupted == true && this.InterruptType == DialogueInterruptType.Choice")]
        public List<Choice> InterruptionChoices;
        
        [ShowIf("@this.IsInterrupted == true && this.InterruptType == DialogueInterruptType.Dialogue")]
        public Dialogue InterruptionDialogue;
        
        private static IEnumerable GetAllCharacterSprites()
        {
            return Resources.LoadAll("Characters", typeof(Sprite));
        }
    }

    public enum DialogueInterruptType
    {
        Choice,
        Potion,
        Dialogue
    }

    public enum DialoguePosition
    {
        Center,
        Left,
        Right
    }
}