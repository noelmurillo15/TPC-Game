/*
* MoveForward - Applies a velocity to the states based on a moveAmount via the state's forward Vector3
* Created by : Allan N. Murillo
* Last Edited : 5/7/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/StateAction/Movement/Forward")]
    public class MoveForward : StateAction
    {
        public float moveSpeed = 2f;


        public override void Execute(StateManager state)
        {
            var originalVelocity = state.myRigidbody.velocity;
            var velocity = state.myTransform.forward * (state.moveAmount * moveSpeed);

            if (state.isGrounded)
            {
                state.myRigidbody.drag = state.moveAmount > 0.1f ? 0 : 4;
                velocity.y = 0f;
            }
            else
            {
                state.myRigidbody.drag = 0f;
                velocity.y = originalVelocity.y;
            }

            state.myRigidbody.velocity = velocity;
        }
    }
}
