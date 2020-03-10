/*
 * StateActions - the logic to execute when in the current State
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour
{
    public abstract class StateAction : ScriptableObject
    {
        public abstract void Execute(StateManager stateManager);
    }
}
