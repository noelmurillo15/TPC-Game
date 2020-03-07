/*
 * PortalNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/7/2020
 */

using UnityEditor;
using UnityEngine;
using ANM.Behaviour;

namespace ANM.Editor.Nodes
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Portal Node")]
    public class PortalNode : DrawNode
    {
        public override void DrawWindow(BaseNode node)
        {
            node.stateRefs.currentState = (State) EditorGUILayout.ObjectField(
                node.stateRefs.currentState, typeof(State), false);
            node.isAssigned = node.stateRefs.currentState != null;
        }

        public override void DrawCurve(BaseNode node)
        {

        }
    }
}