using UnityEngine;

namespace Misc
{
    public class GlobalAccess : MonoBehaviour
    {
        public static GlobalAccess Instance;
        public Inventory.Inventory Inventory;

        private void Awake()
        {
            Instance = this;
        }
    }
}
