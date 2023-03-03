using System.Collections.Generic;
using Alchemy.Nodes;
using UnityEngine;

namespace Alchemy
{
    public class Cauldron : MonoBehaviour
    {
        public Selector UsedSelector;
        public List<Ingredient> UsedIngredients;
        [SerializeField] private Inventory.Inventory _inventory;
        
        public void Brew()
        {
            if (UsedSelector.currentNode.GetEffect() == null) 
                return;
            
            _inventory.AddItem(ToPotion(UsedSelector.currentNode.GetEffect()));
            UsedIngredients.Clear();
            UsedSelector?.ReturnCursor();
        }
        public Potion ToPotion(PotionEffect potionEffect)
        {
            float quality = ReturnQuality();
            int price = (int) (potionEffect.BaseCost * quality * 2);
            return new Potion(potionEffect.PotionSprite, potionEffect.EffectName, price, quality, potionEffect.EffectDescription, potionEffect.GetEffectId());
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
            foreach (Ingredient usedIngredient in UsedIngredients)
            {
                _inventory.AddItem(usedIngredient);
            }
            UsedIngredients.Clear();
            UsedSelector.ReturnCursor();
        }
    }
}