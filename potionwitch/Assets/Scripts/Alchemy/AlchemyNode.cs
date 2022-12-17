using UnityEngine;

namespace Alchemy
{
   public class AlchemyNode : MonoBehaviour
   {
      public int X;
      public int Y;
      
      [SerializeField] private Potion _targetPotion;

      public void ColorNode(Color color)
      {
          GetComponent<SpriteRenderer>().color = color;
      }
   }
}