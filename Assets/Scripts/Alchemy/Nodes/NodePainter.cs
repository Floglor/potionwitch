using UnityEngine;

namespace Alchemy.Nodes
{
    public class NodePainter : MonoBehaviour
    {
        [SerializeField] private NodesHolder _holder;
        [SerializeField] private Color _wallColor;
        [SerializeField] private Color _selectionColor;
        [SerializeField] private Color _initialColor;
        [SerializeField] private Color _pathColor;
        [SerializeField] private Color  _potionColor;

        public void Awake()
        {
            //ResetAllNodeColors();
        }

        public void ResetAllNodeColors()
        {
            foreach (AlchemyNode node in _holder.Nodes)
            {
                ActualizeColor(node);
            }
        }

        public void ActualizeColor(AlchemyNode node)
        {
            if (node.GetEffect() != null)
            {
                node.ColorNode(_potionColor);
                return;
            }
            
            if (node.IsWall)
            {
                node.ColorNode(_wallColor);
                return;
            }
            
            node.ColorNode(_initialColor);
        }

        public void PaintPath(AlchemyNode node)
        {
            PaintNode(node, _pathColor);
        }

        public void PaintSelection(AlchemyNode node)
        {
            PaintNode(node, _selectionColor);
        }

        public void PaintNode(int x, int y, Color color)
        {
            AlchemyNode node = _holder.GetNode(x, y);
            if (node != null)
            {
                node.ColorNode(color);
            }
        }
        
        public void PaintNode(AlchemyNode node, Color color)
        {
            node.ColorNode(color);
        }
        
   
    }
}