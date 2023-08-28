using System.Collections.Generic;
using Alchemy.Nodes;
using Inventory;
using UnityEngine;

namespace Alchemy
{
    public class Stone : MonoBehaviour, IItem
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private int _price;

        [SerializeField] public List<AlchemyNode> Nodes;

        public bool Initialized;

        public Sprite GetIcon()
        {
            return _icon;
        }

        public string GetName()
        {
            return _name;
        }

        public int GetPrice()
        {
            return _price;
        }
    }
}