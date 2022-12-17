using System.Collections.Generic;
using CustomExtensions;
using Alchemy;
using Sirenix.OdinInspector;
using UnityEngine;

public class NodePlacer : MonoBehaviour
{
    [SerializeField] private AlchemyNode _testNode;
    [SerializeField] private NodesHolder _nodesHolder;
    [SerializeField] private GameObject _linkParent;
    [SerializeField] private GameObject _link;
    [SerializeField] private int _testNodeRadius;
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
        AlchemyNode lastNode = null;
        
        for (int x = -_testNodeRadius; x <= _testNodeRadius; x++)
        {
            for (int y = -_testNodeRadius; y <= _testNodeRadius; y++)
            {
                _nodesHolder.AddNode(PlaceNodeWithSpacing(x, y, ref lastNode));
                
            }
        }
    }
    
    [Button("Delete test nodes"), GUIColor(1, 0, 0)]
    public void DeleteTestNodes()
    {
        transform.Clear();
        _nodesHolder.Clear();
    }

    private AlchemyNode PlaceNodeWithSpacing(int x, int y, ref AlchemyNode lastNode)
    {
        AlchemyNode node = Instantiate(_testNode, new Vector3(x * _spacing, y * _spacing, 0),
            Quaternion.identity);

       // if (lastNode != null)
       // {
       //     PlaceLink(lastNode, node);
       // }

        lastNode = node;
        node.transform.SetParent(transform);
        node.X = x;
        node.Y = y;

        return node;
    }

    private void PlaceLink(AlchemyNode originNode, AlchemyNode targetNode)
    {
        Vector3 pointBetween = (originNode.transform.position + targetNode.transform.position) / 2;
        GameObject link = Instantiate(_link, pointBetween, Quaternion.identity);
        link.transform.SetParent(_linkParent.transform);
        link.transform.rotation = Quaternion.Euler(0, 0, originNode.transform.GetZAngle(targetNode.transform));
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