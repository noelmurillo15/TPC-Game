/*
 * State - Contains Lists to actions to execute from a StateManager
 * Used by the Behaviour Node Editor to visually script 
 * Created by : Allan N. Murillo
 * Last Edited : 3/12/2020
 */

using UnityEngine;
using System.Linq;
using ANM.Managers;
using System.Collections.Generic;

namespace ANM.Behaviour
{
    [CreateAssetMenu(menuName = "Behaviours/State")]
    public class State : ScriptableObject
    {
        public List<Transition> transitions = new List<Transition>();
        public StateAction[] onEnter;
        public StateAction[] onUpdate;
        public StateAction[] onFixed;
        public StateAction[] onExit;
        public int idCount;

        public void OnEnter(StateManager states)
        {
            ExecuteActions(states, onEnter);
        }
        
        public void FixedTick(StateManager states)
        {
            ExecuteActions(states, onFixed);
            CheckTransitions(states);
        }

        public void Tick(StateManager states)
        {
            ExecuteActions(states, onUpdate);
            CheckTransitions(states);
        }

        public void OnExit(StateManager states)
        {
            ExecuteActions(states, onExit);
        }

        private static void ExecuteActions(StateManager states, IEnumerable<StateAction> actions)
        {
            foreach (var a in actions)
            {
                if (a != null)
                {
                    a.Execute(states);
                }
            }
        }

        private void CheckTransitions(StateManager states)
        {
            foreach (var transition in from t in transitions
                where !t.disable
                where t.targetState != null
                where t.condition.CheckCondition(states)
                select t)
            {
                states.currentState = transition.targetState;
                OnExit(states);
                states.currentState.OnEnter(states);
                return;
            }
        }

        public Transition GetTransition(int id)
        {
            return transitions.FirstOrDefault(t => t.id == id);
        }

        public Transition AddTransition()
        {
            var retVal = new Transition();
            transitions.Add(retVal);
            retVal.id = idCount;
            idCount++;
            return retVal;
        }

        public void RemoveTransition(int transitionId)
        {
            for (var i = 0; i < transitions.Count; i++)
            {
                if (transitions[i].id == transitionId)
                    transitions.Remove(transitions[i]);
            }
        }
    }
}
