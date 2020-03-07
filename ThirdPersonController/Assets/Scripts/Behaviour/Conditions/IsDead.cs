/*
 * IsDead SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/7/2020
 */

using UnityEngine;

namespace ANM.Behaviour.Conditions
{
    [CreateAssetMenu(menuName = "BehaviourEditor/Conditions/IsDead")]
    public class IsDead : Condition
    {
        public override bool CheckCondition(BehaviourStateManager stateManager)
        {
            return stateManager.health <= 0f;
        }
    }
}
