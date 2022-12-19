using System;
using Alchemy.Nodes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Alchemy
{
    public class AlchemySelector : MonoBehaviour
    {
        public AlchemyNode currentNode;
        private AlchemyNode _previousNode;
        public NodesHolder nodesHolder;

        private IAlchemySelectorTester _tester;

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
            currentNode.ColorNode(Color.yellow);
        }


        private void ReselectNode()
        {
            if (ChangeColorWhenSelected)
            {
                currentNode.ColorNode(Color.white);
            }

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
                MoveCursor(move);
            }

            ReselectNode();
        }

        public void ReturnCursor()
        {
            MoveCursor(0, 0);
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