/*
* MoveWithRootMotion - Used to move a states' position when playing an animation that uses RootMotion
* Created by : Allan N. Murillo
* Last Edited : 5/7/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/StateAction/Movement/With RootMotion")]
    public class MoveWithRootMotion : StateAction
    {
        public override void Execute(StateManager state)
        {
            state.myRigidbody.isKinematic = false;
            var velocity = state.myRigidbody.velocity;
            var targetVelocity = state.myAnimator.deltaPosition;
            targetVelocity *= 60f;
            targetVelocity.y = velocity.y;
            state.myRigidbody.velocity = targetVelocity;
        }
    }
}
