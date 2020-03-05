/*
 * IsDead SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEngine;

namespace ANM.BehaviourNodeEditor.Conditions
{
    [CreateAssetMenu(menuName = "Conditions/IsDead")]
    public class IsDead : Condition
    {
        public override bool CheckCondition(BehaviourStateManager stateManager)
        {
            return stateManager.health <= 0f;
        }
    }
}
