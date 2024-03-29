﻿using System.Collections.Generic;
using CustomExtensions;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Alchemy.Nodes
{
    public class NodePlacer : MonoBehaviour
    {
        [SerializeField] private AlchemyNode _testNode;
        [SerializeField] private NodesHolder _nodesHolder;
        [SerializeField] private GameObject _linkParent;
        [SerializeField] private GameObject _link;
        [SerializeField] private int _testNodeRadius;
        [SerializeField] private int _spacing;
        [SerializeField] private List<Sprite> _starSprites;
        [SerializeField] private NodePainter _nodePainter;
        [SerializeField] private Selector _selector;
        
        
        public Stone Stone;

        private void Awake()
        {
            _nodePainter = FindObjectOfType<NodePainter>();
            _selector = FindObjectOfType<Selector>();
        }

        [Button]
        private void SaveToStone()
        {
            if (Stone == null) return;

            Stone.Nodes = new List<AlchemyNode>();

            foreach (AlchemyNode alchemyNode in _nodesHolder._nodes)
            {
                Stone.Nodes.Add(Instantiate(alchemyNode.gameObject, Stone.transform)
                    .GetComponent<AlchemyNode>());
            }

            GameObject stone = Stone.gameObject;

            SaveObject(stone);


            DestroyImmediate(Stone.gameObject);
            //node.gameObject.SetActive(false);
        }


        private void SaveObject(GameObject stone)
        {
            GameObject prefab = PrefabUtility.SaveAsPrefabAsset(stone, "Assets/" + stone.name + ".prefab");

            if (prefab != null)
            {
                Debug.Log("Prefab saved: " + prefab.name);
            }
            else
            {
                Debug.LogError("Failed to save prefab.");
            }
        }

        [Button]
        public void LoadFromStone()
        {
            DeleteTestNodes();
            foreach (AlchemyNode alchemyNode in Stone.Nodes)
            {
                GameObject node = Instantiate(alchemyNode.gameObject, transform);
                _nodesHolder.AddNode(node.GetComponent<AlchemyNode>());
            }
            

            _nodePainter.ResetAllNodeColors();
            _selector.Initialize();

        }

        [Button("Randomize Nodes Sprites"), GUIColor(0, 1, 1)]
        public void RandomizeNodesSprites()
        {
            List<AlchemyNode> alchemyNodes = FindAllNodes();

            foreach (AlchemyNode alchemyNode in alchemyNodes)
            {
                alchemyNode.SetSprite(_starSprites[Random.Range(0, _starSprites.Count)]);
            }
        }

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
}