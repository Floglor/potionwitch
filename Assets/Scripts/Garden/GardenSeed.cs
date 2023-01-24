using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Alchemy;

namespace Garden
{
    public class GardenSeed : MonoBehaviour, IItem
    {
        //private int _quality;
        private Ingredient _futureIngredient;

        public Sprite GetIcon()
        {
            throw new System.NotImplementedException();
        }

        public string GetName()
        {
            throw new System.NotImplementedException();
        }
    }
}

