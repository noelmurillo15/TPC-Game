/*
 * HealthChanged SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/7/2020
 */

using UnityEngine;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "BehaviourEditor/StateAction/Test/Add Health")]
    public class HealthChanged : StateAction
    {
        public override void Execute(BehaviourStateManager stateManager)
        {
            stateManager.health += 10;
        }
    }
}
