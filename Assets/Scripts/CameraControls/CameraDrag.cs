using UnityEngine;
using UnityEngine.EventSystems;

namespace CameraControls
{
    public class CameraDrag : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
    {

        [SerializeField] private BoxCollider2D _dragArea;
        
        public float dragSpeed = 2;
        private Vector3 _dragOrigin;

        private Camera _mainCamera;
        private bool _isDragging;
        

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
           if (_isDragging && Input.GetMouseButton(0))
           {
               MoveCamera();
           }
        }

        private void MoveCamera()
        {
            Vector3 pos = _mainCamera.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);

            _mainCamera.transform.Translate(move, Space.World);
        }

        public void OnDrag(PointerEventData eventData)
        {
            MoveCamera();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            _dragOrigin = Input.mousePosition;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _isDragging = false;
        }
    }
}