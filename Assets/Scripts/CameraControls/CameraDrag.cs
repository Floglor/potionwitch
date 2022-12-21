using UnityEngine;

namespace CameraControls
{
    public class CameraDrag : MonoBehaviour
    {
        public float dragSpeed = 2;
        private Vector3 _dragOrigin;


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _dragOrigin = Input.mousePosition;
                return;
            }
 
            if (!Input.GetMouseButton(0)) return;
 
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - _dragOrigin);
            Vector3 move = new Vector3(pos.x * dragSpeed, pos.y * dragSpeed, 0);
 
            transform.Translate(move, Space.World);  
        }
 
 
    }
}