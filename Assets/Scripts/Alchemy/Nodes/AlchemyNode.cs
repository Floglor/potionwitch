using UnityEngine;

namespace Alchemy.Nodes
{
   public class AlchemyNode : MonoBehaviour
   {
      public int X;
      public int Y;
      public bool IsWall;
      
      [SerializeField] private PotionEffect potionEffect;

      public PotionEffect GetEffect() => potionEffect;
      
      public void SetSprite(Sprite sprite)
      {
          GetComponent<SpriteRenderer>().sprite = sprite;
      }
      
      public void ColorNode(Color colour)
      {
          GetComponent<SpriteRenderer>().color = colour;
      }
      
      
   }
}