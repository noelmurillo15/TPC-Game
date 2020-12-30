/*
* StopMovementAnimations - Stops the locomotion animation
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/StateAction/Movement/Stop Animation")]
    public class StopMovementAnimations : StateAction
    {
        public string verticalFloatName = "vertical";


        public override void Execute(StateManager state)
        {
            state.myAnimator.SetFloat(verticalFloatName, 0f, 0f, 0f);
        }
    }
}
