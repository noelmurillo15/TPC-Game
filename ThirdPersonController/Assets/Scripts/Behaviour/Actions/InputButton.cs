/*
* InputButton - 
* Created by : Allan N. Murillo
* Last Edited : 3/11/2020
*/

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ANM.Behaviour.Actions
{
    [CreateAssetMenu(menuName = "MonoActions/Inputs/Button")]
    public class InputButton : Action
    {
        public string actionName;
        public bool isPressed;
        public ButtonState buttonState;
        private InputAction _currentAction;
        private ThirdPersonInput.CharacterInputActions inputs;
        [SerializeField] private Scriptables.Controller controller;


        private void OnEnable()
        {
            Debug.Log(actionName + " button OnEnable()");

            _currentAction = null;
            inputs = controller.input.CharacterInput;

            if (_currentAction == null)
            {
                _currentAction = inputs.Get().FindAction(actionName, true);
                
                if (_currentAction != null)
                {
                    switch (actionName)
                    {
                        case "ecef11ef-1547-4991-b7bb-c342f03c9bf0":
                            actionName = "A";
                            break;
                        case "0acba0fe-2f42-4537-a315-6bf7bfa3219f":
                            actionName = "X";
                            break;
                        case "0c66d08b-8dc2-4022-826a-3c6c4722dbc2":
                            actionName = "Y";
                            break;
                        case "b2b18d2b-d23c-4811-a9a1-9baca5ae5f61":
                            actionName = "Roll";
                            break;
                    }
                }
                else
                {
                    Debug.Log(actionName + " button does not exist!");
                }

                inputs.Enable();
                _currentAction?.Enable();
                controller.input.Enable();
                controller.input.CharacterInput.Enable();
            }
        }

        private void OnDisable()
        {
            switch (actionName)
            {
                case "A":
                    actionName = "ecef11ef-1547-4991-b7bb-c342f03c9bf0";
                    break;
                case "X":
                    actionName = "0acba0fe-2f42-4537-a315-6bf7bfa3219f";
                    break;
                case "Y":
                    actionName = "0c66d08b-8dc2-4022-826a-3c6c4722dbc2";
                    break;
                case "Roll":
                    actionName = "b2b18d2b-d23c-4811-a9a1-9baca5ae5f61";
                    break;
            }

            Debug.Log("Disabling " + actionName + " button action!");
            _currentAction?.Disable();
            _currentAction = null;
        }

        public override void Execute()
        {
            if (controller.input.asset.Contains(_currentAction))
            {
                Debug.Log("Checking : " + actionName + " for input!");
                switch (actionName)
                {
                    case "A":
                        isPressed = GetButtonDown(inputs.A.phase);
                        break;
                    case "X":
                        isPressed = GetButtonDown(inputs.X.phase);
                        break;
                    case "Y":
                        isPressed = GetButtonDown(inputs.Y.phase);
                        break;
                    case "Roll":
                        isPressed = GetButtonDown(controller.input.CharacterInput.Roll.phase);
                        break;
                }
            }
            else
            {
                Debug.Log("ThirdPersonController does not contain : " + actionName);
            }

            if (isPressed) Debug.Log(actionName + " has been pressed!");
        }

        public enum ButtonState
        {
            ON_DOWN,
            ON_CURRENT,
            ON_UP
        }

        private static bool GetButtonDown(InputActionPhase phase)
        {
            return phase == InputActionPhase.Started;
        }

        private static bool GetOnButtonDown(InputActionPhase phase)
        {
            return phase == InputActionPhase.Performed;
        }

        private static bool GetButtonUp(InputActionPhase phase)
        {
            return phase == InputActionPhase.Canceled;
        }
    }
}
