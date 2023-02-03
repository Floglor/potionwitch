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

        [SerializeField] private float _growthTime;
        [SerializeField] private float _growthSpeed;

        public Sprite Icon;
        public string Name;

        public void SeedPlanting(GardenPlot gardenPlot)
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
    }
}

