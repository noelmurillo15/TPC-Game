/*
* InputManager - 
* Created by : Allan N. Murillo
* Last Edited : 3/10/2020
*/

using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Behaviour.Actions
{
    [CreateAssetMenu(menuName = "MonoActions/InputManager")]
    public class InputManager : Action
    {
        public MovementInputAxis movementAxis;
        public CameraInputAxis cameraAxis;
        public float moveAmount;
        public Vector3 moveDirection;
        public TransformVariable cameraTransform;


        public override void Execute()
        {
            cameraAxis.Execute();
            movementAxis.Execute();

            moveAmount = Mathf.Clamp01(
                Mathf.Abs(movementAxis.value.x) + Mathf.Abs(movementAxis.value.y));

            if (cameraTransform.value == null) return;
            moveDirection = cameraTransform.value.forward * movementAxis.value.y;
            moveDirection += cameraTransform.value.right * movementAxis.value.x;
        }
    }
}
