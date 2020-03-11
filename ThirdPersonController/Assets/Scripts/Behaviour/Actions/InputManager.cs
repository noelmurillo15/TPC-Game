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
    [CreateAssetMenu(menuName = "MonoActions/InputManager")]
    public class InputManager : Action
    {
        public MovementInputAxis movementAxis;
        public CameraInputAxis cameraAxis;
        public float moveAmount;
        public Vector3 rotateDirection;
        public TransformVariable cameraTransform;
        public StatesManagerVariable statesManagerVar;

        public InputButton A;
        public InputButton B;
        public InputButton X;
        public InputButton Y;


        public override void Execute()
        {
            cameraAxis.Execute();
            movementAxis.Execute();
            
            A.Execute();
            X.Execute();
            Y.Execute();
            B.Execute();

            moveAmount = Mathf.Clamp01(
                Mathf.Abs(movementAxis.value.x) + Mathf.Abs(movementAxis.value.y));

            if (cameraTransform.value != null)
            {
                rotateDirection = cameraTransform.value.forward * movementAxis.value.y;
                rotateDirection += cameraTransform.value.right * movementAxis.value.x;
            }

            if (statesManagerVar.value == null) return;
            statesManagerVar.value.vertical = movementAxis.value.y;
            statesManagerVar.value.horizontal = movementAxis.value.x;
            statesManagerVar.value.moveAmount = moveAmount;
            statesManagerVar.value.rotateDirection = rotateDirection;
        }
    }
}
