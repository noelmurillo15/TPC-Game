/*
 * InputHandler - Detects Input and passes it along to StateManager & CameraManager
 * Created by : Allan N. Murillo
 * Last Edited : 2/24/2020
 */

using System.Collections.Generic;
using ANM.Framework.Managers;
using ANM.Framework.Variables;
using SA.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ANM.Input
{
    public class InputHandler : MonoBehaviour
    {
        private Vector2 _moveDirection;
        private Vector2 _lookRotation;

        private bool _rollInput;
        private bool _xInput;
        private bool _aInput;
        private bool _yInput;

        private bool _rbInput;
        private bool _rtInput;
        private bool _lbInput;
        private bool _ltInput;

        private float _bTimer;
        private float _delta;
        
        //  References
        public GamePhase currentPhase;
        public StateManager stateManager;
        public CameraManager cameraManager;
        private ThirdPersonInput _controls;
        private Transform _cameraTransform;

        //  LockOn
        public TransformVariable mLockOnTransform;
        public bool isLockedOn;
        [SerializeField] private float lockOnBuffer;
        private const float LockOnMaxDistance = 20f;

        //  Enemies
        public List<Transform> mEnemies = new List<Transform>();
        public int enemyIndex;
        

        private void Awake()
        {
            ControllerSetup();
        }
        
        private void Start()
        {
            Initialize();
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
            _aInput = GetButtonStatus(_controls.CharacterInput.A.phase);
            _rollInput = GetButtonStatus(_controls.CharacterInput.Roll.phase);
            _xInput = GetButtonStatus(_controls.CharacterInput.X.phase);
            _yInput = GetButtonStatus(_controls.CharacterInput.Y.phase);

            _rbInput = GetButtonStatus(_controls.CharacterInput.RB.phase);
            _rtInput = GetButtonStatus(_controls.CharacterInput.RT.phase);
            _lbInput = GetButtonStatus(_controls.CharacterInput.LB.phase);
            _ltInput = GetButtonStatus(_controls.CharacterInput.LT.phase);

            LockOnSafetyCheck();

            if (_rollInput)
                _bTimer += _delta;
        }

        private void ApplyInput()
        {
            stateManager.inputVar.rb = _rbInput;
            stateManager.inputVar.lb = _lbInput;
            stateManager.inputVar.rt = _rtInput;
            stateManager.inputVar.lt = _ltInput;
            
            if (_rollInput)
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
            //GetFixedInput();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    ApplyFixedInput();
                    stateManager.Fixed_Tick(_delta);
                    cameraManager.Fixed_Tick(_delta, _lookRotation.x, _lookRotation.y);
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }
        
        private void GetFixedInput()
        {

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

        private void Initialize()
        {
            mLockOnTransform.value = null;

            stateManager.resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
            stateManager.resourcesManager?.Initialize();
            stateManager.Initialize();

            if (cameraManager == null) cameraManager = CameraManager.instance;
            cameraManager.Init(stateManager);
            _cameraTransform = cameraManager.myTransform;
        }

        private void ControllerSetup()
        {
            if(_controls == null) _controls = new ThirdPersonInput();
            
            _controls.CharacterInput.Movement.performed += context =>
                _moveDirection = context.ReadValue<Vector2>();

            _controls.CharacterInput.CameraRotation.performed += context =>
                _lookRotation = context.ReadValue<Vector2>();

            _controls.CharacterInput.Pause.performed += context =>
                GameManager.Instance.TogglePause();

            /*_controls.CharacterInput.RB.started += context => _rbInput = true;
            _controls.CharacterInput.RB.canceled += context => _rbInput = false; */

            _controls.CharacterInput.LockOnToggle.performed += LockOnInputCallback;
            
            _controls.Enable();
        }
        
        private void LockOnSafetyCheck()
        {
            if (isLockedOn)
            {
                if (mEnemies.Count == 0)
                {   //  Safety
                    isLockedOn = false;
                    mLockOnTransform.value = null;
                }
                else
                {   //  Swap Target
                    if (lockOnBuffer <= 0f)
                    {
                        if (_lookRotation.x < -0.8f)
                        {
                            enemyIndex--;
                            if (enemyIndex < 0) { enemyIndex = mEnemies.Count - 1; }
                            mLockOnTransform.value = mEnemies[enemyIndex];
                            lockOnBuffer = 0.5f;
                        }
                        else if (_lookRotation.x > 0.8f)
                        {
                            enemyIndex++;
                            if (enemyIndex > mEnemies.Count - 1) { enemyIndex = 0; }
                            mLockOnTransform.value = mEnemies[enemyIndex];
                            lockOnBuffer = 0.5f;
                        }
                    }
                }

                if (mLockOnTransform.value == null)
                {   //  Safety
                    isLockedOn = false;
                    mLockOnTransform.value = null;
                }
                else
                {   //  Distance check
                    var distanceToTarget = Vector3.Distance(stateManager.myTransform.position, mLockOnTransform.value.position);
                    if (distanceToTarget > LockOnMaxDistance) mLockOnTransform.value = null;
                }
            }

            //  Assign LockOn Target
            if (stateManager.inputVar.lockOnTransform != mLockOnTransform.value)
                stateManager.inputVar.lockOnTransform = mLockOnTransform.value;
        }
        
        private static bool GetButtonStatus(InputActionPhase phase)
        {
            return phase == InputActionPhase.Started;
        }
        

        private void LockOnInputCallback(InputAction.CallbackContext context)
        {
            isLockedOn = !isLockedOn;

            if (isLockedOn)
            {
                if (mEnemies.Count == 0)
                {
                    isLockedOn = false;
                    mLockOnTransform.value = null;
                }
                else
                {
                    enemyIndex = 0;
                    mLockOnTransform.value = mEnemies[enemyIndex];
                }
            }
            else { mLockOnTransform.value = null; }
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