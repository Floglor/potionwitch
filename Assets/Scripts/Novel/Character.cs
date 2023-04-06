using Sirenix.OdinInspector;
using UnityEngine;

namespace Novel
{
    [CreateAssetMenu(fileName = "Character", menuName = "Character")]

    public class Character : ScriptableObject
    {
        public string Name;
        [PreviewField(150, ObjectFieldAlignment.Center)]
        public Sprite DefaultSprite;
    }
}