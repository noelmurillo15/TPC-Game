/*
 * PortalNode -
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEditor;
using UnityEngine;
using ANM.Scriptables.Behaviour;

namespace ANM.Editor.Nodes
{
    [CreateAssetMenu(menuName = "Scriptables/BehaviourEditor/Nodes/Portal Node")]
    public class PortalNode : DrawNode
    {
        public override void DrawWindow(BaseNode node)
        {
            node.stateRefs.currentState = (State) EditorGUILayout.ObjectField(
                node.stateRefs.currentState, typeof(State), false);
            node.isAssigned = node.stateRefs.currentState != null;

            if (node.stateRefs.previousState == node.stateRefs.currentState) return;
            node.stateRefs.previousState = node.stateRefs.currentState;
            BehaviourEditor.ForceSetDirty = true;
        }

        public override void DrawCurve(BaseNode node)
        {

        }
    }
}
