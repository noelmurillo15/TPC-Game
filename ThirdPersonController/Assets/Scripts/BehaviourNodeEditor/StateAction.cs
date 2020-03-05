/*
 * StateActions SO - the logic to execute when in the current State
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEngine;

namespace ANM.BehaviourNodeEditor
{
    public abstract class StateAction : ScriptableObject
    {
        public abstract void Execute(BehaviourStateManager stateManager);
    }
}
