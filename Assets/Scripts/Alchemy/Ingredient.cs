using Inventory;
using Misc;
using Sirenix.OdinInspector;
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
          
          [HideInPlayMode]
          [Button]
          public void CreateSeed()
          {
               AssetCreator.CreateSeed(this, "Assets/Scriptable Objects/Seeds", Name + "Seed", Name + " Seed");
          }
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
