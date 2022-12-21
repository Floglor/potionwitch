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
    }
}