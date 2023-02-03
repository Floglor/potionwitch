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

        [Button("Add Ingredient")]
        private void AddItem()
        {
            _inventory.AddItem(_ingredient);
        }
        [SerializeField] private Effect _potionEffect;

        [Button("Add Potion")]
        private void AddPotion()
        {
            _inventory.AddItem(_cauldron.ToPotion(_potionEffect));  
        }

        [Button("Add Seed")]
        private void AddSeed()
        {
            _inventory.AddItem(_seed);
        }

        //private void Start()
        //{
        //    GameObject button = Instantiate(_buttonPrefab, transform);
        //}
    }
}