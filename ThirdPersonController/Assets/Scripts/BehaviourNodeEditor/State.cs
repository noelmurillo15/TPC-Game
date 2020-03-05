/*
 * State SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEngine;
using System.Collections.Generic;

namespace ANM.BehaviourNodeEditor
{
    [CreateAssetMenu]
    public class State : ScriptableObject
    {
        public List<Transition> transitions = new List<Transition>();
        public StateAction[] actions;
        public StateAction[] onEnter;
        public StateAction[] onExit;


        public void Tick()
        {

        }

        public Transition AddTransition()
        {
            var retVal = new Transition();
            transitions.Add(retVal);
            return retVal;
        }
    }
}
