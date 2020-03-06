/*
 * TransitionNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEngine;

namespace ANM.Editor.Nodes
{
    public class TransitionNode : ScriptableObject
    {
        /*public bool isDuplicate;
        public Condition targetConditionRef;
        public Condition previousCondition;
        public Transition transitionRef;

        public StateNode enterState;
        public StateNode targetState;


        public void Init(StateNode stateToEnter, Transition transition)
        {
            enterState = stateToEnter;
        }

        public void DrawWindow()
        {
            EditorGUILayout.LabelField("");
            targetConditionRef =
                (Condition) EditorGUILayout.ObjectField(
                    targetConditionRef, typeof(Condition), false);

            if (targetConditionRef == null)
            {
                EditorGUILayout.LabelField("No Condition!");
            }
            else
            {
                if (isDuplicate)
                {
                    EditorGUILayout.LabelField("Duplicate Condition!");
                }
                else
                {
                    /*if(transitionRef != null)
                        transitionRef.disable = EditorGUILayout.Toggle("Disable", transitionRef.disable);#1#
                }
            }

            if (previousCondition != targetConditionRef)
            {
                previousCondition = targetConditionRef;
                isDuplicate = BehaviourEditor.CurrentGraph.IsTransitionDuplicate(this);
                if (!isDuplicate) BehaviourEditor.CurrentGraph.SetNode(this);
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
        }*/
    }
}
