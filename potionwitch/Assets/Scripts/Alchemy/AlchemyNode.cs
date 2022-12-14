using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

//scriptableObject menu
namespace Alchemy
{
   public class AlchemyNode : MonoBehaviour
   {
      public List<AlchemyNode> Children;
      public List<AlchemyNode> Parents;
      private int EffectId { get; set; }
   
      public AlchemyNode(int effectId)
      {
         EffectId = effectId;
         Children = new List<AlchemyNode>();
      }
   
      public void AddChild(AlchemyNode child)
      {
         Children.Add(child);
      }
   
      public void RemoveChild(AlchemyNode child)
      {
         Children.Remove(child);
      }
      
      [Button("Detect Placement"), GUIColor(0, 1, 0)]
      public void DetectPlacement()
      {
         Parents.Clear();
         Children.Clear();
         DetectChildren();
         DetectParent();
      }

      private void DetectParent()
      {
         if (transform.parent != null)
         {
            AlchemyNode parentNode = transform.parent.GetComponent<AlchemyNode>();

            if (parentNode != null)
            {
               Parents.Add(parentNode);
            }
         }
      }

      private void OnTransformParentChanged()
      {
         DetectPlacement();
      }

      private void DetectChildren()
      {
         for (int i = 0; i < transform.childCount; i++)
         {
            AlchemyNode child = transform.GetChild(i).GetComponent<AlchemyNode>();
            if (child != null)
            {
               AddChild(child);
            }
         }
      }
   }
}
