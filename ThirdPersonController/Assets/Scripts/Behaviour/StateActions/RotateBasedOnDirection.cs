/*
* RotateBasedOnDirection - 
* Created by : Allan N. Murillo
* Last Edited : 3/11/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Scriptables.Variables;

namespace ANM.Behaviour.StateActions
{
    [CreateAssetMenu(menuName = "MonoActions/RotateViaDirection")]
    public class RotateBasedOnDirection : StateAction
    {
        public TransformVariable cameraTransform;
        public float speed = 8;
        
        
        public override void Execute(StateManager stateManager)
        {
            if (cameraTransform.value == null) return;

            var valueX = stateManager.horizontal;
            var valueY = stateManager.vertical;

            Vector3 targetDirection = cameraTransform.value.forward * valueY;
            targetDirection += cameraTransform.value.right * valueX;
            targetDirection.Normalize();

            targetDirection.y = 0;
            if (targetDirection == Vector3.zero)
                targetDirection = stateManager.transform.forward;

            var tr = Quaternion.LookRotation(targetDirection);
            var targetRotation = Quaternion.Slerp(stateManager.transform.rotation,
                tr, stateManager.deltaTime * stateManager.moveAmount * speed);

            stateManager.transform.rotation = targetRotation;
        }
    }
}
