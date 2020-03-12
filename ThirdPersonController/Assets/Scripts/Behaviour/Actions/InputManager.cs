/*
* InputManager - 
* Created by : Allan N. Murillo
* Last Edited : 3/11/2020
*/

using UnityEngine;
using ANM.Scriptables;
using ANM.Scriptables.Variables;

namespace ANM.Behaviour.Actions
{
    [CreateAssetMenu(menuName = "Behaviours/MonoActions/InputManager")]
    public class InputManager : Action
    {
        [Header("Input Buttons")] 
        public InputButton A;
        public InputButton B;
        public InputButton X;
        public InputButton Y;

        public InputButton Rt;
        public InputButton Lt;
        public InputButton Rb;
        public InputButton Lb;

        [Space] [Header("Input Axis")] 
        public CameraInputAxis CameraAxis;
        public MovementInputAxis MovementAxis;

        [Space] [Header("Movement Variables")] 
        public float moveAmount;
        public Vector3 rotateDirection;
        public TransformVariable cameraTransform;
        public StatesManagerVariable playerStates;
        

        public override void Execute()
        {
            A.Execute();
            X.Execute();
            Y.Execute();
            B.Execute();

            Rt.Execute();
            Lt.Execute();
            Rb.Execute();
            Lb.Execute();

            CameraAxis.Execute();
            MovementAxis.Execute();

            moveAmount = Mathf.Clamp01(
                Mathf.Abs(MovementAxis.value.x) + Mathf.Abs(MovementAxis.value.y));

            if (cameraTransform.value != null)
            {
                rotateDirection = cameraTransform.value.forward * MovementAxis.value.y;
                rotateDirection += cameraTransform.value.right * MovementAxis.value.x;
            }

            if (playerStates.value == null) return;

            playerStates.value.vertical = MovementAxis.value.y;
            playerStates.value.horizontal = MovementAxis.value.x;
            playerStates.value.moveAmount = moveAmount;
            playerStates.value.rotateDirection = rotateDirection;
            
            playerStates.value.rb = Rb.isPressed;
            playerStates.value.rt = Rt.isPressed;
            playerStates.value.lb = Lb.isPressed;
            playerStates.value.lt = Lt.isPressed;
        }
    }
}
