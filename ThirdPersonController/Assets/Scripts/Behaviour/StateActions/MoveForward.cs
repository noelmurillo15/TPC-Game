/*
* MoveForward - 
* Created by : Allan N. Murillo
* Last Edited : 3/11/2020
*/

using UnityEngine;
using ANM.Managers;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "MonoActions/MoveForward")]
    public class MoveForward : StateAction
    {
        public float moveSpeed = 2f;


        public override void Execute(StateManager stateManager)
        {
            stateManager.myRigidbody.drag = stateManager.moveAmount > 0.1f ? 0 : 4;
            var velocity = stateManager.myTransform.forward * (stateManager.moveAmount * moveSpeed);
            stateManager.myRigidbody.velocity = velocity;
        }
    }
}
