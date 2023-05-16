using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Alchemy.Nodes
{
    public class NodesHolder : MonoBehaviour
    {
        [SerializeField] public List<AlchemyNode> _nodes;
        public Stone Stone;

        [Button]
        private void SaveToStone()
        {
            if (Stone == null) return;
            
            Stone.Nodes = new List<AlchemyNode>();
            foreach (AlchemyNode alchemyNode in Nodes)
            {
                Stone.Nodes.Add(alchemyNode);
            }
        }

        [Button]
        private void LoadFromStone()
        {
            
        }
        
        public List<AlchemyNode> Nodes
        {
            get => _nodes;
            set => _nodes = value;
        }

        public AlchemyNode GetNode(int x, int y)
        {
            foreach (AlchemyNode alchemyNode in Nodes)
            {
                if (alchemyNode.X == x && alchemyNode.Y == y)
                {
                    return alchemyNode;
                }
            }

            return null;    
        }

        public void AddNode(AlchemyNode node)
        {
            Nodes.Add(node);
        }
    
        public void AddNodes(List<AlchemyNode> nodes)
        {
            Nodes.AddRange(nodes);
        }

        public void RemoveNode(AlchemyNode node)
        {
            Nodes.Remove(node);
        }

        public void Clear()
        {
            Nodes.Clear();
        }
    }
}