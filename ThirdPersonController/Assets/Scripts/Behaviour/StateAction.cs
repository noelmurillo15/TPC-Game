/*
 * StateActions - Used by the Behaviour Node Editor to add logic (action) to a State
 * Created by : Allan N. Murillo
 * Last Edited : 3/12/2020
 */

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour
{
    public abstract class StateAction : ScriptableObject
    {
        public abstract void Execute(StateManager state);
    }
}
