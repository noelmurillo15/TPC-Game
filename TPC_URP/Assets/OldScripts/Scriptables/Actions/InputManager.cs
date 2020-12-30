/*
* InputManager - Executes InputButton & InputAxis logic & passes values to StateManager
* Created by : Allan N. Murillo
* Last Edited : 5/7/2020
*/

using UnityEngine;
using ANM.Scriptables.Variables;

namespace ANM.Scriptables.Actions
{
    [CreateAssetMenu(menuName = "Scriptables/MonoActions/InputManager")]
    public class InputManager : Action
    {
        [Header("Input Buttons")]
        public InputButton a;
        public InputButton b;
        public InputButton x;
        public InputButton y;

        public InputButton rt;
        public InputButton lt;
        public InputButton rb;
        public InputButton lb;

        public InputButton lockOn;

        [Space] [Header("Input Axis")]
        public CameraInputAxis cameraAxis;
        public MovementInputAxis movementAxis;

        [Space] [Header("Movement Variables")]
        public float moveAmount;
        public Vector3 rotateDirection;
        public TransformVariable cameraTransform;
        public StatesManagerVariable playerStates;


        public override void Execute()
        {
            a.Execute();
            x.Execute();
            y.Execute();
            b.Execute();

            rt.Execute();
            lt.Execute();
            rb.Execute();
            lb.Execute();

            lockOn.Execute();

            cameraAxis.Execute();
            movementAxis.Execute();

            moveAmount = Mathf.Clamp01(
                Mathf.Abs(movementAxis.value.x) + Mathf.Abs(movementAxis.value.y));

            if (cameraTransform.value != null)
            {
                rotateDirection = cameraTransform.value.forward * movementAxis.value.y;
                rotateDirection += cameraTransform.value.right * movementAxis.value.x;
            }

            if (playerStates.value == null) return;

            playerStates.value.vertical = movementAxis.value.y;
            playerStates.value.horizontal = movementAxis.value.x;
            playerStates.value.moveAmount = moveAmount;
            playerStates.value.rotateDirection = rotateDirection;

            playerStates.value.rb = rb.isPressed;
            playerStates.value.rt = rt.isPressed;
            playerStates.value.lb = lb.isPressed;
            playerStates.value.lt = lt.isPressed;
        }
    }
}
