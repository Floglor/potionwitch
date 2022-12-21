using System.Collections.Generic;
using Alchemy.Nodes;
using UnityEngine;

namespace Alchemy
{
    public class SelectorTester : MonoBehaviour, IAlchemySelectorTester
    {
        public MoveSet MoveSet;
        public List<Ingredient> testIngredients;

        public MoveSet CombineIngredients(List<Ingredient> ingredients)
        {
            MoveSet moveSet  = new MoveSet();

            foreach (Ingredient ingredient in ingredients)
            {
                foreach (Move move in ingredient.MoveSet.Moves)
                {
                    moveSet.Add(move);   
                }
            }
            
            return moveSet;
        }

        public void Test(Selector selector)
        {
            selector.ApplyMoveSet(CombineIngredients(testIngredients));
            //selector.ApplyMoveSet(MoveSet);
        }
    }
}