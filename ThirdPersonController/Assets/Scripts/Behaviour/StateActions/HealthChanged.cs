/*
 * HealthChanged ScriptableObject -
 * Created by : Allan N. Murillo
 * Last Edited : 3/10/2020
 */

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "BehaviourEditor/StateAction/Test/Add Health")]
    public class HealthChanged : StateAction
    {
        public override void Execute(StateManager stateManager)
        {
            // stateManager. += 10;
        }
    }
}
