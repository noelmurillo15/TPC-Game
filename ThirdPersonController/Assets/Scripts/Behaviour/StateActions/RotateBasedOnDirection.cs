/*
* RotateBasedOnDirection - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Behaviour.Actions;
using ANM.Scriptables.Variables;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "MonoActions/RotateViaDirection")]
    public class RotateBasedOnDirection : StateAction
    {
        public TransformVariable cameraTransform;
        public InputManager inputManager;
        public float speed = 8;
        
        
        public override void Execute(StateManager stateManager)
        {
            if (cameraTransform.value == null) return;

            var valueX = inputManager.cameraViewInput.value.x;
            var valueY = inputManager.cameraViewInput.value.y;

            Vector3 targetDirection = cameraTransform.value.forward * valueY;
            targetDirection += cameraTransform.value.right * valueX;
            targetDirection.Normalize();

            targetDirection.y = 0;
            if (targetDirection == Vector3.zero)
                targetDirection = stateManager.myTransform.forward;

            var tr = Quaternion.LookRotation(targetDirection);
            var targetRotation = Quaternion.Slerp(stateManager.myTransform.rotation,
                tr, stateManager.deltaTime * inputManager.moveAmount * speed);

            stateManager.transform.rotation = targetRotation;
        }
    }
}
