/*
* InputButton - Reads Button values from ThirdPersonInput ActionMap based on ID
* Created by : Allan N. Murillo
* Last Edited : 5/7/2020
*/

using UnityEngine;
using UnityEngine.InputSystem;

namespace ANM.Scriptables.Actions
{
    [CreateAssetMenu(menuName = "Scriptables/MonoActions/Inputs/Button")]
    public class InputButton : Action
    {
        public bool isPressed;
        public string actionName;
        public ButtonState buttonState;
        private InputAction _currentAction;
        private ThirdPersonInput.CharacterInputActions _inputs;
        [SerializeField] private Managers.Controller controller;


        private void OnEnable()
        {
            _inputs = controller.input.CharacterInput;
            _currentAction?.Disable();
            _currentAction = null;

            _currentAction = _inputs.Get().FindAction(actionName, true);

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
                    case "d99d452c-2b41-4a38-86fa-82c28cc202fb":
                        actionName = "RB";
                        break;
                    case "6450fd32-bd2e-4b8a-af64-408976023375":
                        actionName = "LB";
                        break;
                    case "6f21ad24-52eb-45eb-a773-6472f2bd9da7":
                        actionName = "RT";
                        break;
                    case "f66ae0d2-a03a-4129-a5a3-99328e03e1e5":
                        actionName = "LT";
                        break;
                    case "d76e5f02-4c4f-4b77-9fc8-2921a221896b":
                        actionName = "LockOnToggle";
                        break;
                }
            }
            else
            {
                Debug.Log(actionName + " button does not exist!");
            }

            _inputs.Enable();
            _currentAction?.Enable();
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
                case "RB":
                    actionName = "d99d452c-2b41-4a38-86fa-82c28cc202fb";
                    break;
                case "LB":
                    actionName = "6450fd32-bd2e-4b8a-af64-408976023375";
                    break;
                case "RT":
                    actionName = "6f21ad24-52eb-45eb-a773-6472f2bd9da7";
                    break;
                case "LT":
                    actionName = "f66ae0d2-a03a-4129-a5a3-99328e03e1e5";
                    break;
                case "LockOnToggle":
                    actionName = "d76e5f02-4c4f-4b77-9fc8-2921a221896b";
                    break;
            }

            _currentAction?.Disable();
            _currentAction = null;
        }

        public override void Execute()
        {
            if (controller.input.Contains(_currentAction))
            {
                switch (actionName)
                {
                    case "A":
                        isPressed = GetButtonInputState(_inputs.A.phase);
                        break;
                    case "X":
                        isPressed = GetButtonInputState(_inputs.X.phase);
                        break;
                    case "Y":
                        isPressed = GetButtonInputState(_inputs.Y.phase);
                        break;
                    case "Roll":
                        isPressed = GetButtonInputState(_inputs.Roll.phase);
                        break;
                    case "RT":
                        isPressed = GetButtonInputState(_inputs.RT.phase);
                        break;
                    case "LT":
                        isPressed = GetButtonInputState(_inputs.LT.phase);
                        break;
                    case "RB":
                        isPressed = GetButtonInputState(_inputs.RB.phase);
                        break;
                    case "LB":
                        isPressed = GetButtonInputState(_inputs.LB.phase);
                        break;
                    case "LockOnToggle":
                        isPressed = GetButtonInputState(_inputs.LockOnToggle.phase);
                        break;

                    default:
                        Debug.Log(actionName + " has not been assigned!");
                        break;
                }
            }
            else
            {
                Debug.Log("ThirdPersonController does not contain : " + actionName);
            }
        }

        public enum ButtonState
        {
            ON_DOWN,
            ON_CURRENT,
            ON_UP
        }

        private bool GetButtonInputState(InputActionPhase phase)
        {
            switch (buttonState)
            {
                case ButtonState.ON_DOWN:
                    return GetButtonDown(phase);
                case ButtonState.ON_CURRENT:
                    return GetButtonDown(phase);
                case ButtonState.ON_UP:
                    return GetButtonDown(phase);
                default:
                    return false;
            }
        }

        private static bool GetButtonDown(InputActionPhase phase)
        {
            return phase == InputActionPhase.Started;
        }
    }
}
