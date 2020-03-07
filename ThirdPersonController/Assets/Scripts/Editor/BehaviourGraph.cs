/*
 * BehaviourGraph SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEngine;
using System.Collections.Generic;

namespace ANM.Editor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Graph")]
    public class BehaviourGraph : ScriptableObject
    {
        public List<BaseNode> windows = new List<BaseNode>();
        public int idCount;
        List<int> indexToDelete = new List<int>();


        #region Helper Methods

        public BaseNode GetNodeWithIndex(int index)
        {
            for (var i = 0; i < windows.Count; i++)
            {
                if (windows[i].id == index)
                    return windows[i];
            }

            return null;
        }

        public void DeleteWindowsThatNeedTo()
        {
            for (int i = 0; i < indexToDelete.Count; i++)
            {
                BaseNode node = GetNodeWithIndex(indexToDelete[i]);
                if (node != null)
                    windows.Remove(node);
            }

            indexToDelete.Clear();
        }

        public void DeleteNode(int index)
        {
            indexToDelete.Add(index);
        }

        public bool IsStateDuplicate(BaseNode node)
        {
            for (int i = 0; i < windows.Count; i++)
            {
                if (windows[i].id == node.id) continue;

                if (windows[i].stateRefs.currentState == node.stateRefs.currentState && !windows[i].isDuplicate)
                    return true;
            }

            return false;
        }

        public bool IsTransitionDuplicate(BaseNode node)
        {
            BaseNode enter = GetNodeWithIndex(node.enterNode);
            if (enter == null)
                return false;

            for (int i = 0; i < enter.stateRefs.currentState.transitions.Count; i++)
            {
                Transition t = enter.stateRefs.currentState.transitions[i];
                if (t.condition == node.transRefs.previousCondition && node.transRefs.transitionId != t.id)
                    return true;
            }

            return false;
        }

        #endregion
    }
}
