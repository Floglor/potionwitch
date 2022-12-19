using UnityEngine;

namespace Alchemy
{
    [CreateAssetMenu(fileName = "Ingredient", menuName = "Alchemy/Effect")]
    public class Effect : ScriptableObject
    {
        public string EffectName;
        public string EffectDescription;
    }
}