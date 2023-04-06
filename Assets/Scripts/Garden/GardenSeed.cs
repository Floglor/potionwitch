using UnityEngine;
using Alchemy;
using Inventory;

namespace Garden
{
    [CreateAssetMenu(fileName = "Seed", menuName = "Alchemy/Seed")]
    public class GardenSeed : ScriptableObject, IItem
    {
        //private int _quality;
        [SerializeField] private Ingredient _futureIngredient;

        public int GrowthDays;
        [SerializeField] private float _growthSpeed;
        [SerializeField] private int _price;
        

        public Sprite Icon;
        public string Name;

        public Sprite GrowingSprite;
        public Sprite RipeSprite;

        public void PlantSeed(GardenPlot gardenPlot)
        {
            gardenPlot.Plant(this);
        }

        public Ingredient GetIngredient()
        {
            return _futureIngredient;
        }

        public Sprite GetIcon()
        {
            return Icon;
        }

        public string GetName()
        {
            return Name;
        }

        public int GetPrice()
        {
            return _price;
        }
    }
}

