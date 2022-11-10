/*
* RotateBasedOnCamera - Rotates the state based on movement input
* Created by : Allan N. Murillo
* Last Edited : 3/14/2020
*/

using UnityEngine;
using ANM.Managers;
using ANM.Scriptables.Variables;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Scriptables/Behaviours/StateAction/Movement/Rotate Via Camera")]
    public class RotateBasedOnDirection : StateAction
    {
        public TransformVariable cameraTransform;
        public float speed = 8;


        public override void Execute(StateManager state)
        {
            if (cameraTransform.value == null) return;

            var valueX = state.horizontal;
            var valueY = state.vertical;

            var targetDirection = cameraTransform.value.forward * valueY;
            targetDirection += cameraTransform.value.right * valueX;
            targetDirection.Normalize();

            targetDirection.y = 0;
            if (targetDirection == Vector3.zero)
                targetDirection = state.transform.forward;

            var tr = Quaternion.LookRotation(targetDirection);
            var targetRotation = Quaternion.Slerp(state.transform.rotation,
                tr, state.delta * state.moveAmount * speed);

            state.transform.rotation = targetRotation;
        }
    }
}
