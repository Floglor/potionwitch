using System.Collections;
using UI;
using UnityEngine;

namespace Garden
{
    public class GardenPlot : MonoBehaviour
    {
        private Sprite _sprite;
        public bool _isClosed;
        private float _growthTime;
        private float _growthSpeed;
        private string _seedType;

        public bool _isPlanted;

        public void OnClickDown()
        {
            if (_isClosed)
            {
                return;
            }

            if (!_isPlanted)
            {
                GlobalAccess.Instance.Inventory.gameObject.SetActive(true);
            }


        }
    }
       
}