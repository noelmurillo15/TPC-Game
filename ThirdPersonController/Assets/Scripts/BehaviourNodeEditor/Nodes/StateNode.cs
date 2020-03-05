/*
 * StateNode SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEditor;
using System.Collections.Generic;

namespace ANM.BehaviourNodeEditor.Nodes
{
    public class StateNode : BaseNode
    {
        private bool _collapse;
        public State currentState;
        private State _previousState;
        private List<BaseNode> _dependencies = new List<BaseNode>();

        public override void DrawWindow()
        {
            if (currentState == null)
            {
                EditorGUILayout.LabelField("Add State to Modify:");
            }
            else
            {
                windowRect.height = !_collapse ? 300 : 100;
                _collapse = EditorGUILayout.Toggle(" ", _collapse);
            }

            currentState = (State) EditorGUILayout.ObjectField(
                currentState, typeof(State), false);

            if (_previousState != currentState)
            {
                _previousState = currentState;
                ClearReferences();

                for (var i = 0; i < currentState.transitions.Count; i++)
                {
                    _dependencies.Add(BehaviourEditor.AddTransitionNode(i,
                        currentState.transitions[i], this));
                }
            }

            if (currentState != null)
            {

            }
        }

        public override void DrawCurve()
        {

        }

        public Transition AddTransition()
        {
            return currentState.AddTransition();
        }

        public void ClearReferences()
        {
            BehaviourEditor.ClearWindowsFromList(_dependencies);
            _dependencies?.Clear();
        }
    }
}
