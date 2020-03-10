/*
 * InputHandler - Detects Input and passes it along to StateManager & CameraManager
 * Created by : Allan N. Murillo
 * Last Edited : 3/5/2020
 */

using UnityEngine;
using ANM.Managers;
using ANM.Framework.Managers;
using UnityEngine.InputSystem;
using ANM.Framework.Extensions;
using System.Collections.Generic;
using ANM.Scriptables.Variables;

namespace ANM.Input
{
    public class InputHandler : MonoBehaviour
    {
        [Header("References")] public GamePhase currentPhase;
        public StateManager stateManager;
        public CameraManager cameraManager;

        [Space] [Header("Lock On Variables")] public List<Transform> enemies = new List<Transform>();
        public TransformVariable lockOnTransform;
        [SerializeField] private float lockOnBuffer;
        [SerializeField] private bool isLockedOn;
        [SerializeField] private int enemyIndex;
        private const float LockOnMaxDistance = 20f;

        private ThirdPersonInput _controls;
        private Transform _cameraTransform;

        private Vector2 _moveDirection;
        private Vector2 _lookRotation;

        private float _delta;
        private float _bTimer;

        private bool _bInput;
        private bool _xInput;
        private bool _aInput;
        private bool _yInput;
        private bool _rbInput;
        private bool _rtInput;
        private bool _lbInput;
        private bool _ltInput;


        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            ControllerSetup();

            lockOnTransform.value = null;
            stateManager.resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
            stateManager.resourcesManager?.Initialize();
            stateManager.Initialize();

            if (cameraManager == null) cameraManager = CameraManager.instance;
            cameraManager.Init(stateManager);
            _cameraTransform = cameraManager.myTransform;
        }

        private void ControllerSetup()
        {
            if (_controls == null)
            {
                _controls = SceneExtension.IsThisSceneActive(SceneExtension.MenuUiSceneName)
                    ? FindObjectOfType<MenuManager>().GetControllerInput()
                    : new ThirdPersonInput();
            }

            _controls.Disable();
            // _controls.CharacterInput.Movement.performed += context =>
            //     _moveDirection = context.ReadValue<Vector2>();

            _controls.CharacterInput.CameraRotation.performed += context =>
                _lookRotation = context.ReadValue<Vector2>();

            // _controls.CharacterInput.RB.started += context => _rbInput = true;
            // _controls.CharacterInput.RB.canceled += context => _rbInput = false; 

            _controls.CharacterInput.LockOnToggle.performed += LockOnInputCallback;
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.CharacterInput.LockOnToggle.performed -= LockOnInputCallback;
            _controls.CharacterInput.LockOnToggle.Disable();
            _controls.Disable();
        }

        private void Update()
        {
            _delta = Time.deltaTime;
            lockOnBuffer -= _delta;
            GetInput();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    ApplyInput();
                    //stateManager.Tick(_delta);
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }

        private void FixedUpdate()
        {
            _delta = Time.fixedDeltaTime;
            GetFixedInput();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    ApplyFixedInput();
                    //stateManager.Fixed_Tick(_delta);
                    cameraManager.Fixed_Tick(_delta, _lookRotation.x, _lookRotation.y);
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }

        private void GetInput()
        {
            _aInput = GetButtonStatus(_controls.CharacterInput.A.phase);
            _bInput = GetButtonStatus(_controls.CharacterInput.Roll.phase);
            _xInput = GetButtonStatus(_controls.CharacterInput.X.phase);
            _yInput = GetButtonStatus(_controls.CharacterInput.Y.phase);

            _rbInput = GetButtonStatus(_controls.CharacterInput.RB.phase);
            _rtInput = GetButtonStatus(_controls.CharacterInput.RT.phase);
            _lbInput = GetButtonStatus(_controls.CharacterInput.LB.phase);
            _ltInput = GetButtonStatus(_controls.CharacterInput.LT.phase);

            LockOnSafetyCheck();

            if (_bInput)
                _bTimer += _delta;
        }

        private void GetFixedInput()
        {

        }

        private void ApplyInput()
        {
            stateManager.inputVar.rb = _rbInput;
            stateManager.inputVar.lb = _lbInput;
            stateManager.inputVar.rt = _rtInput;
            stateManager.inputVar.lt = _ltInput;

            if (_bInput)
            {
                _bTimer += _delta;

                if (_bTimer > 0.5f)
                {
                    //  Hold B to RUN
                    stateManager.states.isRunning = true;
                }
            }
            else
            {
                if (_bTimer > 0.05f && _bTimer < 0.5f)
                {
                    //  Tap B to ROLL
                    stateManager.HandleRoll();
                }

                _bTimer = 0f;
                stateManager.states.isRunning = false;
            }

            stateManager.states.isLockedOn = isLockedOn;
        }

        private void ApplyFixedInput()
        {
            var vertical = _moveDirection.y;
            var horizontal = _moveDirection.x;
            stateManager.inputVar.vertical = vertical;
            stateManager.inputVar.horizontal = horizontal;
            stateManager.inputVar.moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

            //  Moves player based on camera angle
            var moveDirection = _cameraTransform.forward * vertical;
            moveDirection += _cameraTransform.right * horizontal;
            moveDirection.Normalize();

            //  Pass Move Direction to StateManager Input
            if (stateManager.characterState != StateManager.CharacterState.ROLL)
                stateManager.inputVar.moveDir = moveDirection;
        }

        private void LockOnSafetyCheck()
        {
            if (isLockedOn)
            {
                if (enemies.Count == 0)
                {
                    //  Safety
                    isLockedOn = false;
                    lockOnTransform.value = null;
                }
                else
                {
                    //  Swap Target
                    if (lockOnBuffer <= 0f)
                    {
                        if (_lookRotation.x < -0.8f)
                        {
                            enemyIndex--;
                            if (enemyIndex < 0)
                            {
                                enemyIndex = enemies.Count - 1;
                            }

                            lockOnTransform.value = enemies[enemyIndex];
                            lockOnBuffer = 0.5f;
                        }
                        else if (_lookRotation.x > 0.8f)
                        {
                            enemyIndex++;
                            if (enemyIndex > enemies.Count - 1)
                            {
                                enemyIndex = 0;
                            }

                            lockOnTransform.value = enemies[enemyIndex];
                            lockOnBuffer = 0.5f;
                        }
                    }
                }

                if (lockOnTransform.value == null)
                {
                    //  Safety
                    isLockedOn = false;
                    lockOnTransform.value = null;
                }
                else
                {
                    //  Distance check
                    var distanceToTarget =
                        Vector3.Distance(stateManager.myTransform.position, lockOnTransform.value.position);
                    if (distanceToTarget > LockOnMaxDistance) lockOnTransform.value = null;
                }
            }

            //  Assign LockOn Target
            if (stateManager.inputVar.lockOnTransform != lockOnTransform.value)
                stateManager.inputVar.lockOnTransform = lockOnTransform.value;
        }

        private void LockOnInputCallback(InputAction.CallbackContext context)
        {
            isLockedOn = !isLockedOn;

            if (isLockedOn)
            {
                if (enemies.Count == 0)
                {
                    isLockedOn = false;
                    lockOnTransform.value = null;
                }
                else
                {
                    enemyIndex = 0;
                    lockOnTransform.value = enemies[enemyIndex];
                }
            }
            else
            {
                lockOnTransform.value = null;
            }
        }

        private static bool GetButtonStatus(InputActionPhase phase)
        {
            return phase == InputActionPhase.Started;

        }
    }

    public enum GamePhase
    {
        IN_GAME,
        IN_MENU,
        IN_INVENTORY
    }

    public enum InputType
    {
        RT,
        LT,
        RB,
        LB
    }
}
