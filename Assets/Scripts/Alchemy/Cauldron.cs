using System.Collections.Generic;
using Alchemy.Nodes;
using UnityEngine;

namespace Alchemy
{
    public class Cauldron : MonoBehaviour
    {
        public Selector UsedSelector;
        public List<Ingredient> UsedIngredients;

        public Potion ToPotion(Effect effect)
        {
            float quality = ReturnQuality();
            int price = (int) (effect.BaseCost * quality * 2);
            return new Potion(effect.PotionSprite, effect.EffectName, price, quality, effect.EffectDescription);
        }

        public float ReturnQuality()
        {
            return Random.Range(0f, 1f);
        }

        public void AddIngredient(Ingredient ingredient)
        {
            UsedSelector.ApplyMoveSet(ingredient.MoveSet);
            UsedIngredients.Add(ingredient);
        }

        public void ResetCauldron()
        {
            UsedIngredients.Clear();
            UsedSelector.ReturnCursor();
        }
    }
}