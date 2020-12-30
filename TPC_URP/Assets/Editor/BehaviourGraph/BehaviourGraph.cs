/*
 * BehaviourGraph SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/7/2020
 */

using UnityEngine;
using System.Linq;
using System.Collections.Generic;

namespace ANM.Editor
{
    [CreateAssetMenu(menuName = "Scriptables/BehaviourEditor/Graph")]
    public class BehaviourGraph : ScriptableObject
    {
        public List<BaseNode> windows = new List<BaseNode>();
        public int idCount;
        private readonly List<int> _indexToDelete = new List<int>();


        #region Helper Methods

        public BaseNode GetNodeWithIndex(int index)
        {
            return windows.FirstOrDefault(node => node.id == index);
        }

        public void DeleteWindowsThatNeedTo()
        {
            foreach (var node in _indexToDelete.Select(GetNodeWithIndex).Where(node => node != null))
                windows.Remove(node);

            _indexToDelete.Clear();
        }

        public void DeleteNode(int index)
        {
            if (!_indexToDelete.Contains(index))
                _indexToDelete.Add(index);
        }

        public bool IsStateDuplicate(BaseNode node)
        {
            return windows.Where(b => b.id != node.id)
                .Any(b => b.stateRefs.currentState == node.stateRefs.currentState && !b.isDuplicate);
        }

        public bool IsTransitionDuplicate(BaseNode node)
        {
            var enterNode = GetNodeWithIndex(node.enterNode);
            return enterNode != null && enterNode.stateRefs.currentState.transitions
                .Any(t => t.condition == node.transRefs.previousCondition
                          && node.transRefs.transitionId != t.id);
        }

        #endregion
    }
}
