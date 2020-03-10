/*
* MoveForward - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Behaviour.Actions;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "MonoActions/MoveForward")]
    public class MoveForward : StateAction
    {
        public InputManager inputManager;
        public float moveSpeed = 2f;


        public override void Execute(StateManager stateManager)
        {
            stateManager.myRigidbody.drag = inputManager.moveAmount > 0.1f ? 0 : 4;
            var velocity = stateManager.myTransform.forward * (inputManager.moveAmount * moveSpeed);
            stateManager.myRigidbody.velocity = velocity;
        }
    }
}
