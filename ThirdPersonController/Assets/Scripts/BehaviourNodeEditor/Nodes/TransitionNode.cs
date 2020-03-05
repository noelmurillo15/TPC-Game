/*
 * TransitionNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEditor;
using UnityEngine;

namespace ANM.BehaviourNodeEditor.Nodes
{
    public class TransitionNode : BaseNode
    {
        public Transition targetTransition;
        public StateNode enterState;
        public StateNode targetState;


        public void Init(StateNode stateToEnter, Transition transition)
        {
            enterState = stateToEnter;
            targetTransition = transition;
        }

        public override void DrawWindow()
        {
            if (targetTransition == null) return;

            EditorGUILayout.LabelField("");
            targetTransition.condition =
                (Condition) EditorGUILayout.ObjectField(
                    targetTransition.condition, typeof(Condition), false);

            if (targetTransition.condition == null)
            {
                EditorGUILayout.LabelField("No Condition!");
            }
            else
            {
                targetTransition.disable = EditorGUILayout.Toggle("Disable", targetTransition.disable);
            }
        }

        public override void DrawCurve()
        {
            if (!enterState) return;
            var rect = windowRect;
            rect.y += windowRect.height * 0.5f;
            rect.width = 1;
            rect.height = 1;
            BehaviourEditor.DrawNodeCurve(enterState.windowRect, rect, true, Color.green);
        }
    }
}
