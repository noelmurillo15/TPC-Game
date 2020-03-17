/*
* MoveForward - Applies a velocity to the states based on a moveAmount via the state's forward Vector3
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Movement/Forward")]
    public class MoveForward : StateAction
    {
        public float moveSpeed = 2f;


        public override void Execute(StateManager state)
        {
            state.myRigidbody.drag = state.moveAmount > 0.1f ? 0 : 4;
            var velocity = state.myTransform.forward * (state.moveAmount * moveSpeed);
            state.myRigidbody.velocity = velocity;
        }
    }
}
