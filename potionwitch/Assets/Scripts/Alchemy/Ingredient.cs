using UnityEngine;

//ScriptableObject menu
namespace Alchemy
{
     [CreateAssetMenu(fileName = "Ingredient", menuName = "Alchemy/Ingredient")]
     public class Ingredient : ScriptableObject
     {
          public string Name;
          public Sprite Sprite;
          public MoveSet MoveSet;
     }
}
