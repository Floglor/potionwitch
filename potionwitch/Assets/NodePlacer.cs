using System.Collections.Generic;
using _2DExtensions;
using Alchemy;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

[RequireComponent(typeof(AlchemyNode))]
public class NodePlacer : MonoBehaviour
{
    [SerializeField] private AlchemyNode _parentNode;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private GameObject _linkParent;
    

    private void Start()
    {
        _parentNode = GetComponent<AlchemyNode>();
    }

    [Button("Place Node Links"), GUIColor(0, 1, 0)]
    public void PlaceNodesGraphics()
    {
        _linkParent.transform.Clear();
        PlaceLinkSpriteRecursive(_parentNode, _parentNode.Children);
    }

    private void PlaceLinkSpriteRecursive(AlchemyNode nodeParent, List<AlchemyNode> nodeChildren)
    {
        foreach (AlchemyNode nodeChild in nodeChildren)
        {
            Vector3 pointBetween = (nodeParent.transform.position + nodeChild.transform.position) / 2;
            GameObject link = Instantiate(_sprite, pointBetween, Quaternion.identity);
            link.transform.SetParent(_linkParent.transform);
            link.transform.rotation = Quaternion.Euler(0, 0, nodeParent.transform.GetZAngle(nodeChild.transform));
            
            if (!nodeChild.Children.IsNullOrEmpty())
                PlaceLinkSpriteRecursive(nodeChild, nodeChild.Children);
        }
    }
}