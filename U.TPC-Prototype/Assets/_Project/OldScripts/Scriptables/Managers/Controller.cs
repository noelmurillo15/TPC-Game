/*
 * InputController - Holds a reference to PlayerControls & contains all Input Events
 * Created by : Allan N. Murillo
 * Last Edited : 3/8/2021
 */

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ANM.TPC.Input
{
    [CreateAssetMenu(menuName = "Scriptables/Single Instance/Controller")]
    public class Controller : ScriptableObject, ThirdPersonInput.ICharacterInputActions
    {
        [SerializeField] private bool debug;

        public event UnityAction<Vector2> OnMovementEvent = delegate { };
        public event UnityAction<Vector2> OnRotationEvent = delegate { };
        public event UnityAction OnLockonToggleEvent = delegate { };
        public event UnityAction OnQuitEvent = delegate { };

        public event UnityAction<bool> OnRbEvent = delegate { };
        public event UnityAction<bool> OnLbEvent = delegate { };
        public event UnityAction<bool> OnRtEvent = delegate { };
        public event UnityAction<bool> OnLtEvent = delegate { };
        public event UnityAction<bool> OnAEvent = delegate { };
        public event UnityAction<bool> OnBEvent = delegate { };
        public event UnityAction<bool> OnXEvent = delegate { };
        public event UnityAction<bool> OnYEvent = delegate { };

        private ThirdPersonInput _input;


        #region Unity Funcs

        private void OnEnable()
        {
            Log("OnEnable");
            _input ??= new ThirdPersonInput();
            _input.Enable();
            _input.CharacterInput.Enable();
            _input.CharacterInput.SetCallbacks(this);
            OnQuitEvent += EndGameInputHandler;
        }

        private void OnDisable()
        {
            Log("OnDisable");
            if (_input == null)
            {
                Debug.LogWarning("[Controller]: OnDisable - Input was NULL");
                return;
            }

            OnQuitEvent -= EndGameInputHandler;
            _input.Disable();
            //_input.Dispose();
            //_input = null;
        }

        #endregion

        #region Public Funcs

        public ThirdPersonInput GetInput() => _input;

        public void OnMovement(InputAction.CallbackContext context)
        {
            Log("OnMovement");
            OnMovementEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnCameraRotation(InputAction.CallbackContext context)
        {
            Log("OnCameraRotation");
            OnRotationEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnQuit(InputAction.CallbackContext context)
        {
            Log("OnQuit");
            OnQuitEvent?.Invoke();
        }

        public void OnLockOnToggle(InputAction.CallbackContext context)
        {
            Log("OnLockOnToggle");
            OnLockonToggleEvent?.Invoke();
        }

        public void OnRB(InputAction.CallbackContext context)
        {
            Log("OnRB");
            if (context.performed) OnRbEvent?.Invoke(true);
            else if (context.canceled) OnRbEvent?.Invoke(false);
        }

        public void OnLB(InputAction.CallbackContext context)
        {
            Log("OnLB");
            if (context.performed) OnLbEvent?.Invoke(true);
            else if (context.canceled) OnLbEvent?.Invoke(false);
        }

        public void OnRT(InputAction.CallbackContext context)
        {
            Log("OnRT");
            if (context.performed) OnRtEvent?.Invoke(true);
            else if (context.canceled) OnRtEvent?.Invoke(false);
        }

        public void OnLT(InputAction.CallbackContext context)
        {
            Log("OnLT");
            if (context.performed) OnLtEvent?.Invoke(true);
            else if (context.canceled) OnLtEvent?.Invoke(false);
        }

        public void OnA(InputAction.CallbackContext context)
        {
            Log("OnA");
            if (context.performed) OnAEvent?.Invoke(true);
            else if (context.canceled) OnAEvent?.Invoke(false);
        }

        public void OnRoll(InputAction.CallbackContext context)
        {
            Log("OnRoll");
            if (context.performed) OnBEvent?.Invoke(true);
            else if (context.canceled) OnBEvent?.Invoke(false);
        }

        public void OnX(InputAction.CallbackContext context)
        {
            Log("OnX");
            if (context.performed) OnXEvent?.Invoke(true);
            else if (context.canceled) OnXEvent?.Invoke(false);
        }

        public void OnY(InputAction.CallbackContext context)
        {
            Log("OnY");
            if (context.performed) OnYEvent?.Invoke(true);
            else if (context.canceled) OnYEvent?.Invoke(false);
        }

        #endregion

        #region Private Funcs

        private void Log(string msg)
        {
            if (!debug) return;
            Debug.Log("[Controller] : " + msg);
        }

#if !UNITY_EDITOR
        private static void EndGameInputHandler() => Application.Quit();
#else
        private static void EndGameInputHandler() => UnityEditor.EditorApplication.isPlaying = false;
#endif
        #endregion
    }
}
