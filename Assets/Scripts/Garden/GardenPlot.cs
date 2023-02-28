using Inventory;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Garden
{
    public class GardenPlot : MonoBehaviour, IPointerDownHandler
    {
        public bool NotBought; 
        
        private GardenSeed _seed;
        IItem _futureIngredient;


        public bool IsPlanted;
        public bool IsSelected;
        private bool _ingredientReady;
        private float _growthTime=0;

        public SpriteRenderer SpriteRenderer;

        public Sprite OriginalSprite;
        public Sprite PlantedSprite;

        private void Awake()
        {
            SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (NotBought)
            {
                Debug.Log("Not bought yet!");
                return;
            }

            if (!IsPlanted && !IsSelected)
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
                IsPlanted = false;

                ChangeGardenSprite(OriginalSprite);
            }

            if (IsSelected)
            {
                GlobalAccess.Instance.Inventory.DeselectGardenPlot(this);

            }
        }

        public void Plant(GardenSeed seed)
        {
            if (IsPlanted)
            {
                return;
            }

            _seed = seed;
            IsPlanted = true;
            GlobalAccess.Instance.Inventory.DeselectGardenPlot(this);
            ChangeGardenSprite(PlantedSprite);
        }

        void Update()
        {
            if (!IsPlanted)
            {
                return;
            }

            if (_growthTime > 0)
            {
                _growthTime -= Time.deltaTime;
            }
            else
            {
                //growthTime not mentioned and never set to zero

                _ingredientReady = true;
                ChangeGardenSprite(_seed.RipeSprite);
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

        private void ChangeGardenSprite(Sprite TargetSprite)
        {
            SpriteRenderer.sprite = TargetSprite;
        }
    }      
}