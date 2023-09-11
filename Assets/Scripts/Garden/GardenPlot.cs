using Inventory;
using Misc;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Garden
{
    public class GardenPlot : MonoBehaviour, IPointerDownHandler, IObserver
    {

        public bool IsBought; 
        
        private GardenSeed _seed;
        IItem _futureIngredient;


        public bool IsPlanted;
        public bool IsSelected;
        private bool _ingredientReady;
        private int _growthDaysLeft=0; 

        public SpriteRenderer SpriteRenderer;

        public Sprite OriginalSprite;
        public Sprite PlantedSprite;

        [SerializeField] private Color _genericColor;
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _unboughtColor;

        

        private void Awake()
        {
            SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            TimeController _timeController = FindObjectOfType<TimeController>();
            _timeController.Attach(this);
        }

        private void Start()
        {
            if (!IsBought)
            {
                GetComponent<SpriteRenderer>().color = _unboughtColor;
                IsSelected = true;
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!IsBought)
            {
                Debug.Log("Haven't bought yet!");
                return;
            }

            if (!IsPlanted && !IsSelected)
            {
                GlobalAccess.Instance.Inventory.gameObject.SetActive(true);
                GlobalAccess.Instance.Inventory.SelectGardenPlot(this);
                IsSelected = true;
                return;
            }

            if (_ingredientReady)
            {
                Debug.Log("Got ingredient!");
                _futureIngredient = _seed.GetIngredient();
                GlobalAccess.Instance.Inventory.SpawnItem(_futureIngredient);

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
            _growthDaysLeft = _seed.GrowthDays;
        }

        public void Select()
        {
            GetComponent<SpriteRenderer>().color = _selectedColor;
            IsSelected = true;
        }

        public void Deselect()
        {
            GetComponent<SpriteRenderer>().color = _genericColor;
            IsSelected=false;
        }

        private void ChangeGardenSprite(Sprite TargetSprite)
        {
            SpriteRenderer.sprite = TargetSprite;
        }

        public void UpdateObserver(ISubject subject)
        {
            GrowGardenPlot();
        }

        public void GrowGardenPlot()
        {
            if (!IsPlanted)
            {
                return;
            }

            _growthDaysLeft--;

            if (_growthDaysLeft <= 0)
            {
                _ingredientReady = true;
                ChangeGardenSprite(_seed.RipeSprite);
            }
            else
            {
                ChangeGardenSprite(_seed.GrowingSprite);
            }
            
        }
    }      
}