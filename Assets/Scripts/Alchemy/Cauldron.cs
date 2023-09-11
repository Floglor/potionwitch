using System.Collections.Generic;
using Alchemy.Nodes;
using Inventory;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Alchemy
{
    public class Cauldron : MonoBehaviour, IDropHandler
    {
        public Selector UsedSelector;
        public List<Ingredient> UsedIngredients;
        public CircleGame _circleGame;

        [SerializeField] private Inventory.Inventory _inventory;

        private NodePlacer _nodePlacer;

        private void Start()
        {
            _nodePlacer = FindObjectOfType<NodePlacer>();
        }

        public void Brew()
        {
            if (_nodePlacer.Stone == null) return;

            if (UsedSelector.CursorNode.GetEffect() == null)
            {
                Debug.Log("Can't brew, node is empty");
                return;
            }

            _circleGame.gameObject.SetActive(true);
            _circleGame.transform.parent.gameObject.SetActive(true);
            PotionEffect potionEffect = UsedSelector.CursorNode.GetEffect();
            _circleGame.StartGame(potionEffect.CircleGameSpeed, potionEffect.CircleGameRotation);

            _circleGame.IsGameRunning = true;
            _circleGame.OnCompletion = delegate(bool quality) { AddPotion(quality ? 1f : 0f); };
        }

        private void AddPotion(float quality)
        {
            if (UsedSelector.CursorNode.GetEffect() == null)
                return;

            Debug.Log($"Creating potion. Quality: {quality}");
            _inventory.SpawnItem(ToPotion(UsedSelector.CursorNode.GetEffect(), quality));
            UsedIngredients.Clear();
            UsedSelector?.ReturnCursor();
        }

        public Potion ToPotion(PotionEffect potionEffect, float quality)
        {
            int price = (int) (potionEffect.BaseCost * quality * 2);
            return new Potion(potionEffect.PotionSprite, potionEffect.EffectName, price, quality,
                potionEffect.EffectDescription, potionEffect.GetEffectId());
        }


        public bool AddIngredient(Ingredient ingredient)
        {
            if (!UsedSelector.ApplyMoveSet(ingredient.MoveSet))
                return false;

            UsedIngredients.Add(ingredient);

            return true;
        }

        public void ResetCauldron()
        {
            foreach (Ingredient usedIngredient in UsedIngredients)
            {
                _inventory.SpawnItem(usedIngredient);
            }

            if (_nodePlacer.Stone == null) return;

            UsedIngredients.Clear();
            UsedSelector.ReturnCursor();
            _nodePlacer.ClearNodesAndStone();
        }

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("Cauldron OnDrop");

            if (eventData.pointerDrag.GetComponent<InventoryItem>() == null) return;


            IItem item = eventData.pointerDrag.GetComponent<InventoryItem>()
                .TargetItem;

            if (item is Stone stone)
            {
                _nodePlacer.Stone = stone;
                _nodePlacer.LoadFromStone();
                return;
            }

            if (item is Ingredient ingredient && _nodePlacer.Stone != null)
            {
                Debug.Log("it is an ingredient");

                if (AddIngredient(ingredient))
                {
                    _inventory.DestroyItem(ingredient);

                    Destroy(ingredient);
                }
            }
        }
    }
}