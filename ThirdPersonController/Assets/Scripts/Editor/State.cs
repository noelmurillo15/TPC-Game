/*
 * State SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/6/2020
 */

using UnityEngine;
using System.Collections.Generic;

namespace ANM.Editor
{
    [CreateAssetMenu(menuName = "BehaviourEditor/State")]
    public class State : ScriptableObject
    {
        public List<Transition> transitions = new List<Transition>();
        public StateAction[] onEnter;
        public StateAction[] onState;
        public StateAction[] onExit;


        public void OnEnter(BehaviourStateManager states)
        {
            ExecuteActions(states, onEnter);
        }

        public void Tick(BehaviourStateManager states)
        {
            ExecuteActions(states, onState);
            CheckTransitions(states);
        }

        public void OnExit(BehaviourStateManager states)
        {
            ExecuteActions(states, onExit);
        }

        private static void ExecuteActions(BehaviourStateManager states, IEnumerable<StateAction> actions)
        {
            foreach (var a in actions)
            {
                if (a != null)
                {
                    a.Execute(states);
                }
            }
        }

        private void CheckTransitions(BehaviourStateManager states)
        {
            foreach (var t in transitions)
            {
                if (t.disable) continue;
                if (t.targetState == null) continue;
                if (!t.condition.CheckCondition(states)) continue;
                states.currentState = t.targetState;
                OnExit(states);
                states.currentState.OnEnter(states);
                return;
            }
        }

        public Transition AddTransition()
        {
            var retVal = new Transition();
            transitions.Add(retVal);
            return retVal;
        }
    }
}
