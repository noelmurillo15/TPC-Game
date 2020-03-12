/*
* MoveForward - 
* Created by : Allan N. Murillo
* Last Edited : 3/11/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Move Forward")]
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
