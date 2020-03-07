/*
 * Condition SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/7/2020
 */

using UnityEngine;

namespace ANM.Behaviour.Conditions
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool CheckCondition(BehaviourStateManager stateManager);
    }
}
