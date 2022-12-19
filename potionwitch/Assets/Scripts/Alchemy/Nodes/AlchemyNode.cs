using UnityEngine;

namespace Alchemy.Nodes
{
   public class AlchemyNode : MonoBehaviour
   {
      public int X;
      public int Y;
      public Color initialColor;
      
      [SerializeField] private Effect _effect;

      public void ColorNode(Color colour)
      {
          GetComponent<SpriteRenderer>().color = colour;
      }

      private void Awake()
      {
          ColorNode(initialColor);
      }
   }
}