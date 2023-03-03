using Alchemy;
using Garden;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace DevTools
{
    public class CheatMenu : MonoBehaviour
    {
        [SerializeField] private Cauldron _cauldron;
        [SerializeField] private Inventory.Inventory _inventory;
        [SerializeField] private Ingredient _ingredient;
        [SerializeField] private GardenSeed _seed;
        [SerializeField] private PotionEffect _potionEffect;

        [Button("Add Ingredient")]
        private void AddItem()
        {
            if (_ingredient is null) return;
            _inventory.AddItem(_ingredient);
        }


        [Button("Add Potion")]
        private void AddPotion()
        {
            if (_potionEffect is null) return;
            _inventory.AddItem(_cauldron.ToPotion(_potionEffect));
        }

        [Button("Add Seed")]
        private void AddSeed()
        {
            if (_seed is null) return;
            _inventory.AddItem(_seed);
        }

        private void Start()
        {
            AddItem();
            AddPotion();
            AddSeed();
        }
    }
}