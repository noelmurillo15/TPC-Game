/*
 * Condition - Tells a transition to go to another state when the condition has been met
 * Created by : Allan N. Murillo
 * Last Edited : 3/12/2020
 */

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool CheckCondition(StateManager state);
    }
}
