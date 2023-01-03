using System;
using UI;
using UnityEngine;

namespace Alchemy
{
    [Serializable]
    public class Potion : IItem
    {
       private Sprite _sprite;
       private string _name;
       private int _cost;
       private float _quality;
       private string _description;
       
         public Potion(Sprite sprite, string name, int cost, float quality, string description)
         {
              _sprite = sprite;
              _name = name;
              _cost = cost;
              _quality = quality;
              _description = description;
         }

         public Sprite GetIcon()
         {
             return _sprite;
         }

         public string GetName()
         {
             return _name;
         }
    }
}