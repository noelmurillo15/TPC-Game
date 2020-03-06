/*
 * Condition SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEngine;

namespace ANM.Editor
{
    public abstract class Condition : ScriptableObject
    {
        public abstract bool CheckCondition(BehaviourStateManager stateManager);
    }
}
