using System.Collections.Generic;
using System.Linq;
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
    
    [Button("Place Nodes"), GUIColor(0, 1, 1)]
    public void PlaceNodes()
    {
        List<AlchemyNode> alchemyNodes = FindAllNodes();

        foreach (AlchemyNode alchemyNode in alchemyNodes)
        {
            alchemyNode.transform.position = new Vector3(alchemyNode.X, alchemyNode.Y, 0);
        }
    }

    private List<AlchemyNode> FindAllNodes()
    {
        GameObject[] nodes = GameObject.FindGameObjectsWithTag("AlchemyNode");

        List<AlchemyNode> alchemyNodes = new List<AlchemyNode>();
        
        foreach (GameObject node in nodes)
        {
            alchemyNodes.Add(node.GetComponent<AlchemyNode>());
        }

        return alchemyNodes;
    }
}