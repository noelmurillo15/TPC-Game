using System;
using UnityEngine;
using SA.Managers;
using ANM.Framework;
using SA.Scriptable.Variables;
using UnityEngine.InputSystem;
using System.Collections.Generic;

namespace SA.Input
{
    //  TODO : add persistent game manager and attach this script to it -Singleton
    public class InputHandler : MonoBehaviour
    {   //  Detects Input and passes it along to StateManager & CameraManager
        #region Class Member Variables
        public GameEvent onGamePauseEvent;
        public GameEvent onGameResumeEvent;

        [SerializeField] private bool xbox = false;
        
        private Mouse _mouse;
        private Gamepad _gamepad;
        private Keyboard _keyboard;

        private float _vertical;
        private float _horizontal;

        private float _camVertical;
        private float _camHorizontal;

        private bool _bInput;
        private bool _xInput;
        private bool _aInput;
        private bool _yInput;

        private bool _rbInput;
        private bool _rtInput;
        private bool _lbInput;
        private bool _ltInput;

        private float _bTimer;
        private float _delta;

        private const string XboxControllerName = "XInputControllerWindows";

        //  References
        public GamePhase currentPhase;
        public StateManager stateManager;
        public CameraManager cameraManager;
        private ThirdPersonInput _inputController;
        private Transform _cameraTransform;

        //  LockOn
        public TransformVariable m_lockOnTransform;
        public bool isLockedOn;
        [SerializeField] private float lockOnBuffer;
        private const float LockOnMaxDistance = 20f;

        //  Enemies
        public List<Transform> m_enemies = new List<Transform>();
        public int enemyIndex;
        #endregion


        private void Awake()
        {
            ControllerCheck();
        }

        private void Start()
        {
            Initialize();
        }

        private void OnEnable()
        {
            _inputController.CharacterInput.Select.performed += SelectInput;
            _inputController.CharacterInput.Select.Enable();
            _inputController.CharacterInput.LStick.performed += LeftStickInput;
            _inputController.CharacterInput.LStick.Enable();
            _inputController.CharacterInput.RStick.performed += RightStickInput;
            _inputController.CharacterInput.RStick.Enable();
        }

        private void OnDisable()
        {
            _inputController.CharacterInput.Select.performed -= SelectInput;
            _inputController.CharacterInput.Select.Disable();
            _inputController.CharacterInput.LStick.performed -= LeftStickInput;
            _inputController.CharacterInput.LStick.Disable();
            _inputController.CharacterInput.RStick.performed -= RightStickInput;
            _inputController.CharacterInput.RStick.Disable();
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
                    stateManager.Tick(_delta);
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }
        
        private void GetInput()
        {
            if (!xbox)
            {
                //  TODO : Get keyboard equivalent buttons pressed
            }
            else
            {
                if (_gamepad == null) { xbox = false; return; }

                _aInput = _gamepad.aButton.isPressed;
                _bInput = _gamepad.bButton.isPressed;
                _xInput = _gamepad.xButton.isPressed;
                _yInput = _gamepad.yButton.isPressed;

                _rbInput = _gamepad.rightShoulder.isPressed;
                _lbInput = _gamepad.leftShoulder.isPressed;

                _ltInput = _gamepad.leftTrigger.isPressed;
                _rtInput = _gamepad.rightTrigger.isPressed;
            }
            
            LockOnSafetyCheck();

            if (_bInput)
                _bTimer += _delta;
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
                {   //  Hold B to RUN
                    stateManager.states.isRunning = true;
                }
            }
            else
            {
                if (_bTimer > 0.05f && _bTimer < 0.5f)
                {   //  Tap B to ROLL
                    stateManager.HandleRoll();
                }

                _bTimer = 0f;
                stateManager.states.isRunning = false;
            }

            stateManager.states.isLockedOn = isLockedOn;
        }

        
        private void FixedUpdate()
        {
            _delta = Time.fixedDeltaTime;
            GetFixedInput();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    ApplyFixedInput();
                    stateManager.Fixed_Tick(_delta);
                    cameraManager.Fixed_Tick(_delta, _camHorizontal, _camVertical);
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }
        
        private void GetFixedInput()
        {
            if (!xbox)
            {
                //  TODO : use WASD Composite to get movement delta
                _camVertical = _mouse.delta.ReadValue().y;
                _camHorizontal = _mouse.delta.ReadValue().x;
            }
            else
            {
                if (_gamepad == null) { xbox = false; return; }

                _vertical = _gamepad.leftStick.ReadValue().y;
                _horizontal = _gamepad.leftStick.ReadValue().x;

                _camVertical = _gamepad.rightStick.ReadValue().y;
                _camHorizontal = _gamepad.rightStick.ReadValue().x;
            }
        }
        
        private void ApplyFixedInput()
        {
            stateManager.inputVar.vertical = _vertical;
            stateManager.inputVar.horizontal = _horizontal;
            stateManager.inputVar.moveAmount = Mathf.Clamp01(Mathf.Abs(_vertical) + Mathf.Abs(_horizontal));

            //  Moves player based on camera angle
            Vector3 moveDirection = _cameraTransform.forward * _vertical;
            moveDirection += _cameraTransform.right * _horizontal;
            moveDirection.Normalize();

            //  Pass Move Direction to StateManager Input
            if (stateManager.characterState != StateManager.CharacterState.ROLL)
                stateManager.inputVar.moveDir = moveDirection;
        }
        
        
        private void Initialize()
        {
            m_lockOnTransform.value = null;

            stateManager.resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
            stateManager.resourcesManager.Initialize();
            stateManager.Initialize();

            if (cameraManager == null) cameraManager = CameraManager.Instance;
            cameraManager.Init(stateManager);
            _cameraTransform = cameraManager.myTransform;
        }

        private void ControllerCheck()
        {
            _inputController = new ThirdPersonInput();
            if (Gamepad.current != null)
            {
                xbox = true;
                _gamepad = Gamepad.current;
            }
            else
            {
                xbox = false;
                _gamepad = null;
            }
            _keyboard = Keyboard.current;
            _mouse = Mouse.current;
            
            InputSystem.onDeviceChange += (device, change) =>
            {
                switch (change)
                {
                    case InputDeviceChange.Added:
                        //Debug.Log("InputHandler::New Device Added : " + device.name);
                        break;
                    case InputDeviceChange.Removed:
                        Debug.Log("InputHandler::New Device Removed : " + device.name);
                        if (device.name.Contains(XboxControllerName))
                        {  xbox = false; }
                        break;
                    case InputDeviceChange.Disconnected:
                        Debug.Log("InputHandler::Device Disconnected : " + device.name);
                        if (device.name.Contains(XboxControllerName))
                        {  xbox = false;  }
                        break;
                    case InputDeviceChange.Reconnected:
                        Debug.Log("InputHandler::Device Reconnected : " + device.name);
                        if (device.name.Contains(XboxControllerName))
                        {  xbox = true;  }
                        break;
                    case InputDeviceChange.Enabled:
                        //Debug.Log("InputHandler::Device Enabled : " + device.name);
                        break;
                    case InputDeviceChange.Disabled:
                        //Debug.Log("InputHandler::Device Disabled : " + device.name);
                        break;
                    case InputDeviceChange.UsageChanged:
                        //Debug.Log("InputHandler::Device usage change : " + device.name);
                        break;
                    case InputDeviceChange.ConfigurationChanged:
                        //Debug.Log("InputHandler::Device config change : " + device.name);
                        break;
                    case InputDeviceChange.Destroyed:
                        //Debug.Log("InputHandler::Device destroyed : " + device.name);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(change), change, null);
                }
            };
        }
        
        private void LockOnSafetyCheck()
        {
            if (isLockedOn)
            {
                if (m_enemies.Count == 0)
                {   //  Safety
                    isLockedOn = false;
                    m_lockOnTransform.value = null;
                }
                else
                {   //  Swap Target
                    if (lockOnBuffer <= 0f)
                    {
                        if (_camHorizontal < -0.8f)
                        {
                            enemyIndex--;
                            if (enemyIndex < 0) { enemyIndex = m_enemies.Count - 1; }
                            m_lockOnTransform.value = m_enemies[enemyIndex];
                            lockOnBuffer = 0.5f;
                        }
                        else if (_camHorizontal > 0.8f)
                        {
                            enemyIndex++;
                            if (enemyIndex > m_enemies.Count - 1) { enemyIndex = 0; }
                            m_lockOnTransform.value = m_enemies[enemyIndex];
                            lockOnBuffer = 0.5f;
                        }
                    }
                }

                if (m_lockOnTransform.value == null)
                {   //  Safety
                    isLockedOn = false;
                    m_lockOnTransform.value = null;
                }
                else
                {   //  Distance check
                    float distanceToTarget = Vector3.Distance(stateManager.myTransform.position, m_lockOnTransform.value.position);
                    if (distanceToTarget > LockOnMaxDistance) m_lockOnTransform.value = null;
                }
            }

            //  Assign LockOn Target
            if (stateManager.inputVar.lockOnTransform != m_lockOnTransform.value)
                stateManager.inputVar.lockOnTransform = m_lockOnTransform.value;
        }
        
        
        private void SelectInput(InputAction.CallbackContext context)
        {
            if (GameManager.Instance.GetIsGamePaused())
            {
                onGameResumeEvent.Raise();
            }
            else
            {
                onGamePauseEvent.Raise();
            }
        }

        private static void LeftStickInput(InputAction.CallbackContext context)
        {   //  TODO : apply behaviour
            Debug.Log("InputHandler::Left Stick was pressed!");
        }

        private void RightStickInput(InputAction.CallbackContext context)
        {
            isLockedOn = !isLockedOn;

            if (isLockedOn)
            {
                if (m_enemies.Count == 0)
                {
                    isLockedOn = false;
                    m_lockOnTransform.value = null;
                }
                else
                {
                    enemyIndex = 0;
                    m_lockOnTransform.value = m_enemies[enemyIndex];
                }
            }
            else { m_lockOnTransform.value = null; }
        }
    }

    
    public enum GamePhase
    {
        IN_GAME, IN_MENU, IN_INVENTORY
    }

    public enum InputType
    {
        RT, LT, RB, LB
    }
}