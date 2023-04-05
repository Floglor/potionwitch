using System;
using Inventory;
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

       private int _effectId;
       
         public Potion(Sprite sprite, string name, int cost, float quality, string description, int id)
         {
              _sprite = sprite;
              _name = name;
              _cost = cost;
              _quality = quality;
              _description = description;
              _effectId = id;
         }

         public Sprite GetIcon()
         {
             return _sprite;
         }

         public string GetName()
         {
             return _name;
         }

         public int GetPrice()
         {
             return 0;
         }

         public int GetEffectId()
         {
             return _effectId;
         }
    }
}