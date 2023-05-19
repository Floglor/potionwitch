using System.Linq;
using Novel;
using UnityEngine;

namespace Misc
{
    public static class CharacterFinder
    {
        public static Character FindScriptableObjectByName(string objectName)
        {
            Character[] scriptableObjects = Resources.FindObjectsOfTypeAll<Character>();

            return scriptableObjects.FirstOrDefault(scriptableObject => scriptableObject.name == objectName);
        }
    }
}