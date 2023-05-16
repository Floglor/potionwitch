using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CameraControls
{
    public class CameraDrag : MonoBehaviour, IDragHandler
    {

        [SerializeField] private BoxCollider2D _dragArea;
        
        public float dragSpeed = 2;
        private Vector3 _dragOrigin;

        private Camera _mainCamera;

        
        private void OnMouseDrag()
        {
            Debug.Log("OnMouseDrag");
            MoveCamera();
        }

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dragOrigin = Input.mousePosition;
                return;
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
    }
}