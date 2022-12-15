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
    [SerializeField] private AlchemyNode _testNode;
    [SerializeField] private GameObject _linkParent;
    [SerializeField] private int _test_node_radius;
    [SerializeField] private int _spacing;
    


    [Button("Place Nodes"), GUIColor(0, 1, 1)]
    public void PlaceNodes()
    {
        List<AlchemyNode> alchemyNodes = FindAllNodes();

        foreach (AlchemyNode alchemyNode in alchemyNodes)
        {
            alchemyNode.transform.position = new Vector3(alchemyNode.X, alchemyNode.Y, 0);
        }
    }

    [Button("Place Test Nodes"), GUIColor(0, 1, 1)]
    public void PlaceTestNodes()
    {
        for (int x = -_test_node_radius; x <= _test_node_radius; x++)
        {
            for (int y = -_test_node_radius; y <= _test_node_radius; y++)
            {
                AlchemyNode node = Instantiate(_testNode, new Vector3(x*_spacing, y*_spacing, 0), Quaternion.identity);
                node.transform.SetParent(transform);
                node.X = x;
                node.Y = y;
            }
        }
    }
    
    private void SpawnTestNode(int x, int y)
    {
        AlchemyNode node = Instantiate(_testNode, new Vector3(x, y, 0), Quaternion.identity);
        node.transform.SetParent(transform);
        node.X = x;
        node.Y = y;
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