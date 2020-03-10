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
        public MouseInputAxis cameraViewInput;

        public float moveAmount;
        public Vector3 moveDirection;
        public TransformVariable cameraTransform;


        public override void Execute()
        {
            cameraViewInput.Execute();

            moveAmount = Mathf.Clamp01(
                Mathf.Abs(cameraViewInput.value.x) + Mathf.Abs(cameraViewInput.value.y));

            if (cameraTransform.value == null) return;
            moveDirection = cameraTransform.value.forward * cameraViewInput.value.y;
            moveDirection += cameraTransform.value.right * cameraViewInput.value.x;
        }
    }
}
