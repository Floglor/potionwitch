using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Alchemy
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Alchemy/Effect")]
    public class Effect : ScriptableObject
    {
        public string EffectName;
        public string EffectDescription;
        public Sprite PotionSprite;
        public int BaseCost;
        public float Difficulty;

        [ShowInInspector] private int _effectId;

        [Button("Generate ID")]
        public void GenerateID()
        {
            _effectId = GetInstanceID();
        }
        
        public int GetEffectId()
        {
            return _effectId;
        }
    }
}