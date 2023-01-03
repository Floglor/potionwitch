using Alchemy;
using Sirenix.OdinInspector;
using UI;
using UnityEngine;

namespace DevTools
{
    public class CheatMenu : MonoBehaviour
    {
        [SerializeField] private Cauldron _cauldron;
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Ingredient _ingredient;

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

        //private void Start()
        //{
        //    GameObject button = Instantiate(_buttonPrefab, transform);
        //}
    }
}