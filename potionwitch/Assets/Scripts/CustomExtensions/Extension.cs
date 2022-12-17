using UnityEngine;

namespace CustomExtensions
{
    public static class Extension
    {
        public static float GetZAngle(this Transform lookingObject, Transform lookingAtObject)
        {
            Vector3 diff = lookingAtObject.transform.position - lookingObject.position;
            diff.Normalize();
            
            float zRotation = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
            return zRotation;
        }
        
        public static void Clear(this Transform transform)
        {
            int children = transform.childCount;
            for (int i = children - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}