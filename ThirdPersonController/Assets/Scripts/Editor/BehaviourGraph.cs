/*
 * BehaviourGraph SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEngine;
using ANM.Editor.Nodes;
using System.Collections.Generic;

namespace ANM.Editor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Graph")]
    public class BehaviourGraph : ScriptableObject
    {
        public List<BaseNode> windows = new List<BaseNode>();


        #region Helper Methods

        public bool IsStateNodeDuplicate(StateNode node)
        {
            return false;
        }

        #endregion
    }
}
