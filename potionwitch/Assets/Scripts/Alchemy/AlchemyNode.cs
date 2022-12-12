using System.Collections.Generic;

public class AlchemyNode
{
   private List<AlchemyNode> Children { get; }
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
}
