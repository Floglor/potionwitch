using System.Collections.Generic;
using UnityEngine;

namespace Alchemy.Nodes
{
    public class NodesHolder : MonoBehaviour
    {
        [SerializeField] private List<AlchemyNode> _nodes;

        public List<AlchemyNode> Nodes
        {
            get => _nodes;
            set => _nodes = value;
        }

        public AlchemyNode GetNode(int x, int y)
        {
            foreach (AlchemyNode alchemyNode in _nodes)
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
            _nodes.Add(node);
        }
    
        public void AddNodes(List<AlchemyNode> nodes)
        {
            _nodes.AddRange(nodes);
        }

        public void RemoveNode(AlchemyNode node)
        {
            _nodes.Remove(node);
        }

        public void Clear()
        {
            _nodes.Clear();
        }
    }
}