using Alchemy;
using Garden;
using Sirenix.OdinInspector;
using UnityEngine;

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
            _inventory.SpawnItem(_ingredient);
        }


        [Button("Add Potion")]
        private void AddPotion()
        {
            if (_potionEffect is null) return;
            _inventory.SpawnItem(_cauldron.ToPotion(_potionEffect));
        }

        [Button("Add Seed")]
        private void AddSeed()
        {
            if (_seed is null) return;
            _inventory.SpawnItem(_seed);
        }

        private void Start()
        {
            AddItem();
            AddPotion();
            AddSeed();
        }
    }
}