/*
 * HealthChanged SO -
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEngine;

namespace ANM.BehaviourNodeEditor.StateActions
{
    [CreateAssetMenu(menuName = "StateAction/Test/Add Health")]
    public class HealthChanged : StateAction
    {
        public override void Execute(BehaviourStateManager stateManager)
        {
            stateManager.health += 10;
        }
    }
}
