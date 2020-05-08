/*
* MoveForwardWithAngle - Applies a smooth (lerp) velocity based on input, ground check & forward angled raycast
* Created by : Allan N. Murillo
* Last Edited : 5/7/2020
*/

using ANM.Managers;
using UnityEngine;

namespace ANM.Scriptables.Behaviour.StateActions.Movement
{
    [CreateAssetMenu(menuName = "Behaviours/StateAction/Movement/Forward With Angle")]
    public class MoveForwardWithAngle : StateAction
    {
        public float frontRayOffset = 0.5f;
        public float movementSpeed = 4f;
        public float adaptSpeed = 10f;


        public override void Execute(StateManager state)
        {
            float frontY = 0;
            Vector3 origin = state.myTransform.position + (state.myTransform.forward * frontRayOffset);
            origin.y += frontRayOffset;

            if (Physics.Raycast(origin, -Vector3.up, out var hit, 1f, state.ignoreForGroundCheck))
            {
                var y = hit.point.y;
                frontY = y - state.myTransform.position.y;
            }

            var moveAmount = state.moveAmount;
            Vector3 currentVelocity = state.myRigidbody.velocity;
            Vector3 targetVelocity = state.myTransform.forward * moveAmount * movementSpeed;

            if (state.isGrounded)
            {
                if (moveAmount > 0.1f)
                {
                    state.myRigidbody.isKinematic = false;
                    state.myRigidbody.drag = 0f;
                    if (Mathf.Abs(frontY) > 0.02f)
                        targetVelocity.y = ((frontY > 0) ? frontY + 0.2f : frontY - 0.2f) * movementSpeed;
                }
                else
                {
                    if (Mathf.Abs(frontY) > 0.02f)
                    {
                        state.myRigidbody.isKinematic = true;
                        targetVelocity.y = 0;
                        state.myRigidbody.drag = 4;
                    }
                }
            }
            else
            {
                state.myRigidbody.isKinematic = false;
                state.myRigidbody.drag = 0f;
                targetVelocity.y = currentVelocity.y;
            }

            state.myRigidbody.velocity = Vector3.Lerp(
                currentVelocity, targetVelocity, state.deltaTime * adaptSpeed);
        }
    }
}
