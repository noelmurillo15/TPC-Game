/*
 * TransitionNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEditor;
using UnityEngine;
using ANM.Behaviour;
using ANM.Behaviour.Conditions;

namespace ANM.Editor.Nodes
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Transition Node")]
    public class TransitionNode : DrawNode
    {

        public void Init(StateNode stateToEnter, Transition transition)
        {

        }

        public override void DrawWindow(BaseNode node)
        {
            EditorGUILayout.LabelField(" ");
            var enterNode = BehaviourEditor.EditorSettings.currentGraph.GetNodeWithIndex(node.enterNode);
            if (enterNode == null) return;

            if (enterNode.stateRefs.currentState == null)
            {
                BehaviourEditor.EditorSettings.currentGraph.DeleteNode(node.id);
                return;
            }

            var transition = enterNode.stateRefs.currentState.GetTransition(node.transRefs.transitionId);
            if (transition == null) return;

            transition.condition = (Condition) EditorGUILayout.ObjectField(
                transition.condition, typeof(Condition), false);

            if (transition.condition == null)
            {
                EditorGUILayout.LabelField("No Condition!");
                node.isAssigned = false;
            }
            else
            {
                node.isAssigned = true;
                if (node.isDuplicate)
                    EditorGUILayout.LabelField("Duplicate Condition!");
                else
                {
                    var tNode = BehaviourEditor.EditorSettings.currentGraph.GetNodeWithIndex(node.targetNode);

                    if (tNode != null)
                        transition.targetState = !tNode.isDuplicate
                            ? tNode.stateRefs.currentState
                            : null;
                    else
                        transition.targetState = null;
                }
            }

            if (node.transRefs.previousCondition == transition.condition) return;
            node.transRefs.previousCondition = transition.condition;
            node.isDuplicate = BehaviourEditor.EditorSettings.currentGraph.IsTransitionDuplicate(node);
            if (!node.isDuplicate)
                BehaviourEditor.ForceSetDirty = true;
        }

        public override void DrawCurve(BaseNode node)
        {
            Color targetColor;
            var enterNode = BehaviourEditor.EditorSettings.currentGraph.GetNodeWithIndex(node.enterNode);

            if (enterNode == null) BehaviourEditor.EditorSettings.currentGraph.DeleteNode(node.id);
            else
            {
                var startRect = enterNode.windowRect;
                var endRect = node.windowRect;
                endRect.y += node.windowRect.height * 0.5f;
                endRect.height = 1;
                endRect.width = 1;

                targetColor = !node.isAssigned || node.isDuplicate ? Color.red : Color.green;
                BehaviourEditor.DrawNodeCurve(startRect, endRect, true, targetColor);
            }

            if (node.targetNode <= 0 || node.isDuplicate) return;

            var tNode = BehaviourEditor.EditorSettings.currentGraph.GetNodeWithIndex(node.targetNode);
            if (tNode == null) node.targetNode = -1;
            else
            {
                var startRect = node.windowRect;
                var endRect = tNode.windowRect;
                endRect.x -= endRect.width * 0.5f;
                startRect.x += startRect.width;

                if (tNode.drawNode is StateNode)
                    targetColor = !tNode.isAssigned || tNode.isDuplicate ? Color.red : Color.green;
                else
                    targetColor = !tNode.isAssigned ? Color.red : Color.cyan;

                BehaviourEditor.DrawNodeCurve(startRect, endRect, false, targetColor);
            }
        }
    }
}
