﻿using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Alchemy.Nodes
{
    public class Selector : MonoBehaviour
    {
        public AlchemyNode currentNode;

        public NodesHolder nodesHolder;

        [SerializeField] private TextMeshProUGUI _nodeDebugText;

        private IAlchemySelectorTester _tester;
        private AlchemyNode _previousNode;
        private int _positionX;
        private int _positionY;

        [InfoBox("Disabled in Editor since editing material causes errors. Press play and Test")]
        [DisableInEditorMode]
        [Button("Test")]
        public void Test()
        {
            if (_tester == null)
            {
                _tester = GetComponent<IAlchemySelectorTester>();
            }

            _tester.Test(this);
        }

        public bool ChangeColorWhenSelected = true;

        public void MoveCursor(int x, int y)
        {
            _positionX = x;
            _positionY = y;

            ReselectNode();
        }

        public void MoveCursor(Move move)
        {
            _positionX += move.X;
            _positionY += move.Y;
        }

        private void Start()
        {
            currentNode = nodesHolder.GetNode(_positionX, _positionY);
            //Eto konesh kostil))
            ResetStartNode();
            currentNode.ColorNode(Color.yellow);
        }

        private void ResetStartNode()
        {
            _previousNode = currentNode;
        }
        private void ResetAllNodeColors()
        {
            foreach (AlchemyNode nodesHolderNode in nodesHolder.Nodes)
            {
                nodesHolderNode.ColorNode(nodesHolderNode.initialColor);
            }
        }

        private void ReselectNode()
        {
            if (ChangeColorWhenSelected)
            {
                currentNode.ColorNode(currentNode.initialColor);
            }

            if (_previousNode != null)
                _previousNode.ColorNode(Color.blue);

            _previousNode = currentNode;
            currentNode = nodesHolder.GetNode(_positionX, _positionY);

            if (currentNode == null)
            {
                currentNode = _previousNode;
                _positionX = _previousNode.X;
                _positionY = _previousNode.Y;

                Debug.Log($"Map edge reached on [{currentNode.X}], [{currentNode.Y}]");
            }

            if (ChangeColorWhenSelected)
            {
                currentNode.ColorNode(Color.yellow);
                SelectNode(currentNode);
            }
            
            if (_nodeDebugText == null) return;

            if (currentNode.GetEffect() == null)
                _nodeDebugText.text = $"Node: [{currentNode.X}], [{currentNode.Y}], Empty";
            else
                _nodeDebugText.text = $"Node: [{currentNode.X}], [{currentNode.Y},] {currentNode.GetEffect().name}";
        }

        public void ApplyMove(Move move)
        {
            MoveCursor(move);
            ReselectNode();
        }

        public void ApplyMoveSet(MoveSet moveSet)
        {
            foreach (Move move in moveSet.Moves)
            {
                ApplyMove(move);
            }

            ReselectNode();
        }

        public void ReturnCursor()
        {
            MoveCursor(0, 0);
            ResetAllNodeColors();
            ResetStartNode();
            ReselectNode();
        }

        public int PositionX
        {
            get => _positionX;
            set => _positionX = value;
        }

        public int PositionY
        {
            get => _positionY;
            set => _positionY = value;
        }


        public void SelectNode(AlchemyNode node)
        {
            Debug.Log("Node " + node + " is selected");
        }
    }
}