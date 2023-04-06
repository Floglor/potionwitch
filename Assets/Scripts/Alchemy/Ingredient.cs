using Inventory;
using UnityEngine;

//ScriptableObject menu
namespace Alchemy
{
     [CreateAssetMenu(fileName = "Ingredient", menuName = "Alchemy/Ingredient")]
     public class Ingredient : ScriptableObject, IItem
     {
          public MoveSet MoveSet;
          public Sprite Icon;
          public string Name;
          public int Price;
          public Sprite GetIcon()
          {
               return Icon;
          }

          public string GetName()
          {
               return Name;
          }

          public int GetPrice()
          {
               return Price;
          }
     }
}
