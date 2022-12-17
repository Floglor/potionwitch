using UnityEngine;

namespace Alchemy
{
    public class SelectorTester : MonoBehaviour, IAlchemySelectorTester
    {
        public MoveSet MoveSet;

        public void Test(AlchemySelector selector)
        {
            selector.ApplyMoveSet(MoveSet);
        }
    }
}