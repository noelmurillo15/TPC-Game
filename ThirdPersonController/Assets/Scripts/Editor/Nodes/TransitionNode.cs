/*
 * TransitionNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEditor;
using UnityEngine;

namespace ANM.Editor.Nodes
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Nodes/Transition Node")]
    public class TransitionNode : DrawNode
    {

        public void Init(StateNode stateToEnter, Transition transition)
        {

        }

        public override void DrawWindow(BaseNode b)
        {
            EditorGUILayout.LabelField("");

            BaseNode enter = BehaviourEditor.EditorSettings.currentGraph.GetNodeWithIndex(b.enterNode);
            Transition transition = enter.stateRefs.currentState.GetTransition(b.transRefs.transitionId);

            transition.condition = (Condition) EditorGUILayout.ObjectField(
                transition.condition, typeof(Condition), false);

            if (transition.condition == null)
            {
                EditorGUILayout.LabelField("No Condition!");
            }
            else
            {
                if (b.isDuplicate)
                {
                    EditorGUILayout.LabelField("Duplicate Condition!");
                }
                else
                {
                    // if(node.transitionRefs.targetCondition != null)
                    //     node.transitionRefs.targetCondition.disable = EditorGUILayout.Toggle(
                    //         "Disable", transitionRef.disable);
                }
            }

            if (b.transRefs.previousCondition != transition.condition)
            {
                b.transRefs.previousCondition = transition.condition;
                b.isDuplicate = BehaviourEditor.EditorSettings.currentGraph.IsTransitionDuplicate(b);
                // if (!node.isDuplicate) BehaviourEditor.CurrentGraph.SetNode(this);
            }
        }

        public override void DrawCurve(BaseNode node)
        {
            var nodeRect = node.windowRect;
            nodeRect.y += node.windowRect.height * 0.5f;
            nodeRect.width = 1;
            nodeRect.height = 1;

            BaseNode from = BehaviourEditor.EditorSettings.currentGraph.GetNodeWithIndex(node.enterNode);
            if (from == null)
            {
                BehaviourEditor.EditorSettings.currentGraph.DeleteNode(node.id);
            }
            else
            {
                Rect fromRect = from.windowRect;
                BehaviourEditor.DrawNodeCurve(fromRect, nodeRect, true, Color.green);
            }
        }
    }
}
