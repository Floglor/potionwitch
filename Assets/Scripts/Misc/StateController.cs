using Sirenix.OdinInspector;
using UnityEngine;

namespace Misc
{
    [InfoBox("Singleton. Use StateController.Instance to access. No more than one instance of this class should exist in the scene.")]
    public class StateController : MonoBehaviour
    {
        public static StateController Instance { get; private set; }
        public bool areIngredientsSelectable;
        public bool arePotionsSelectable;
    
        private void Awake()
        {
            Instance = this;
        }
    }
}