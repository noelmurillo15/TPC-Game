/*
 * BehaviourGraph SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEngine;
using ANM.Editor.Nodes;
using System.Collections.Generic;

namespace ANM.Editor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Graph")]
    public class BehaviourGraph : ScriptableObject
    {
        public List<SavedStateNode> savedStateNodes = new List<SavedStateNode>();
        private Dictionary<State, StateNode> _stateDict = new Dictionary<State, StateNode>();
        private Dictionary<StateNode, SavedStateNode> _stateNodesDict = new Dictionary<StateNode, SavedStateNode>();


        public void Init()
        {
            _stateDict.Clear();
            _stateNodesDict.Clear();
        }

        public void SetStateNode(StateNode node)
        {
            SavedStateNode savedNode = GetSavedState(node);
            if (savedNode == null)
            {
                savedNode = new SavedStateNode();
                savedStateNodes.Add(savedNode);
                _stateNodesDict.Add(node, savedNode);
            }

            savedNode.state = node.currentState;
            savedNode.position = new Vector2(node.windowRect.x, node.windowRect.y);
        }

        public SavedStateNode GetSavedState(StateNode node)
        {
            _stateNodesDict.TryGetValue(node, out var savedNode);
            return savedNode;
        }

        public void ClearStateNode(StateNode node)
        {
            SavedStateNode savedNode = GetSavedState(node);
            if (savedNode == null) return;
            savedStateNodes.Remove(savedNode);
            _stateNodesDict.Remove(node);
        }

        public StateNode GetStateNode(State state)
        {
            _stateDict.TryGetValue(state, out var node);
            return node;
        }
    }

    [System.Serializable]
    public class SavedStateNode
    {
        public State state;
        public Vector2 position;
    }

    [System.Serializable]
    public class SavedTransition
    {

    }
}
