using System;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Alchemy.Nodes
{
    [RequireComponent(typeof(NodePainter))]
    public class Selector : MonoBehaviour
    {
        public AlchemyNode CursorNode;

        public NodesHolder nodesHolder;

        [SerializeField] private TextMeshProUGUI _nodeDebugText;
         private NodePainter _nodePainter;
        

        private IAlchemySelectorTester _tester;
        private AlchemyNode _previousNode;
        private int _positionX;
        private int _positionY;

        private void Awake()
        {
            _nodePainter = GetComponent<NodePainter>();
        }

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

            ValidateNode();
        }

        public void MoveCursor(Move move)
        {
            _positionX += move.X;
            _positionY += move.Y;
        }

        private void Start()
        {
            CursorNode = nodesHolder.GetNode(_positionX, _positionY);
            ResetStartNode();
           _nodePainter.PaintSelection(CursorNode);
        }

        private void ResetStartNode()
        {
            _previousNode = CursorNode;
        }
        
        private bool ValidateNode()
        {
            if (_previousNode != null)
                _nodePainter.PaintPath(_previousNode);

            _previousNode = CursorNode;
            CursorNode = nodesHolder.GetNode(_positionX, _positionY);

            if (CursorNode == null)
            {
                ReturnToPreviousPosition();

                Debug.Log($"Map edge reached on [{CursorNode.X}], [{CursorNode.Y}]");
                return false;
            }

            if (CursorNode.IsWall)
            {
                ReturnToPreviousPosition();
                
                Debug.Log($"Wall reached on [{CursorNode.X}], [{CursorNode.Y}]");
                return false;
            }

            if (ChangeColorWhenSelected)
            {
                _nodePainter.PaintSelection(CursorNode);

                SelectNode(CursorNode);
            }

            if (_nodeDebugText == null)
                return true;

            if (CursorNode.GetEffect() == null)
                _nodeDebugText.text = $"Node: [{CursorNode.X}], [{CursorNode.Y}], Empty";
            else
                _nodeDebugText.text = $"Node: [{CursorNode.X}], [{CursorNode.Y},] {CursorNode.GetEffect().name}";

            return true;
        }

        private void ReturnToPreviousPosition()
        {
            CursorNode = _previousNode;
            _positionX = _previousNode.X;
            _positionY = _previousNode.Y;
        }

        public void ApplyMove(Move move)
        {
            MoveCursor(move);
            ValidateNode();
        }

        public void ApplyMoveSet(MoveSet moveSet)
        {
            foreach (Move move in moveSet.Moves)
            {
                ApplyMove(move);
            }

            ValidateNode();
        }

        public void ReturnCursor()
        {
            MoveCursor(0, 0);
            _nodePainter.ResetAllNodeColors();
            ResetStartNode();
            ValidateNode();
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