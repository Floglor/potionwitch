using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Alchemy;

namespace Garden
{
    [CreateAssetMenu(fileName = "Seed", menuName = "Alchemy/Seed")]
    public class GardenSeed : ScriptableObject, IItem
    {
        //private int _quality;
        private Ingredient _futureIngredient;

        [SerializeField] private float _growthTime;
        [SerializeField] private float _growthSpeed;

        public Sprite Icon;
        public string Name;

        public void SeedPlanting(GardenPlot gardenPlot)
        {
            gardenPlot.Plant(this);
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

