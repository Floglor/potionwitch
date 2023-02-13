using Inventory;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Garden
{
    public class GardenPlot : MonoBehaviour, IPointerDownHandler
    {
        private Sprite _sprite;
        public bool _notBought; 
        
        private GardenSeed _seed;
        IItem _futureIngredient;


        public bool _isPlanted;
        public bool IsSelected;
        private bool _ingredientReady;
        private float _growthTime=0;
        

        public void OnPointerDown(PointerEventData eventData)
        {

            Debug.Log("Clicked on Garden Plot!");
            if (_notBought)
            {
                Debug.Log("Not bought yet!");
                return;
            }

            if (!_isPlanted && !IsSelected)
            {
                Debug.Log("Succesfully clicked on Garden Plot! Activate Inventory!");
                GlobalAccess.Instance.Inventory.gameObject.SetActive(true);
                GlobalAccess.Instance.Inventory.SelectGardenPlot(this);
                IsSelected = true;
                return;
            }

            if (_ingredientReady)
            {
                Debug.Log("Got ingredient!");
                _futureIngredient = _seed.GetIngredient();
                GlobalAccess.Instance.Inventory.AddItem(_futureIngredient);
                _ingredientReady = false;
                _isPlanted = false;
            }

            if (IsSelected)
            {
                GlobalAccess.Instance.Inventory.DeselectGardenPlot(this);

            }
        }

        public void Plant(GardenSeed seed)
        {
            _seed = seed;
            _isPlanted = true;
        }

        void Update()
        {
            if (!_isPlanted)
            {
                return;
            }

            if (_growthTime > 0)
            {
                _growthTime -= Time.deltaTime;
            }
            else
            {
                _ingredientReady = true;
            }
        }

        public void Select()
        {
            GetComponent<SpriteRenderer>().color = Color.yellow;
            IsSelected = true;
        }

        public void Deselect()
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            IsSelected=false;
        }
    }
       
}