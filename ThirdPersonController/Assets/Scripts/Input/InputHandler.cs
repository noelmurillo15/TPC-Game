using System;
using UnityEngine;
using SA.Managers;
using SA.Scriptable.Variables;
using UnityEngine.InputSystem;
using System.Collections.Generic;


namespace SA.Input
{
    //  TODO : add persistant game manager and attach this script to it -Singleton
    public class InputHandler : MonoBehaviour
    {   //  Detects Input and passes it along to StateManager & CameraManager
        #region Class Member Variables
        //  New Input System        
        [SerializeField] private bool xbox = false;
        private Gamepad gamepad;

        //  Input Variables
        private float vertical;
        private float horizontal;

        private float camVertical;
        private float camHorizontal;

        private bool b_input;
        private bool x_input;
        private bool a_input;
        private bool y_input;

        private bool rb_input;
        private bool rt_input;
        private bool lb_input;
        private bool lt_input;

        private float b_timer;
        private float delta;

        //  References
        public GamePhase currentPhase;
        public StateManager stateManager;
        public CameraManager cameraManager;
        private ThirdPersonInput inputController;
        private Transform cameraTransform;

        //  LockOn
        public TransformVariable m_lockOnTransform;

        public bool isLockedOn;
        [SerializeField] private float lockOnBuffer;
        private float lockOnMaxDistance = 20f;

        //  Enemies
        public List<Transform> m_enemies = new List<Transform>();
        public int enemyIndex;
        #endregion


        #region Initialization

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
            if (!xbox) return;
            inputController.ThirdPersonXboxInput.Select.performed += SelectInput;
            inputController.ThirdPersonXboxInput.Select.Enable();

            inputController.ThirdPersonXboxInput.LStick.performed += LeftStickInput;
            inputController.ThirdPersonXboxInput.LStick.Enable();
            inputController.ThirdPersonXboxInput.RStick.performed += LockOnInput;
            inputController.ThirdPersonXboxInput.RStick.Enable();
        }

        private void OnDisable()
        {
            if (!xbox) return;
            inputController.ThirdPersonXboxInput.Select.performed -= SelectInput;
            inputController.ThirdPersonXboxInput.Select.Disable();

            inputController.ThirdPersonXboxInput.LStick.performed -= LeftStickInput;
            inputController.ThirdPersonXboxInput.LStick.Disable();
            inputController.ThirdPersonXboxInput.RStick.performed -= LockOnInput;
            inputController.ThirdPersonXboxInput.RStick.Disable();
        }

        private void Initialize()
        {
            m_lockOnTransform.value = null;

            stateManager.m_resourcesManager = Resources.Load("ResourcesManager") as Managers.ResourcesManager;
            stateManager.m_resourcesManager.Initialize();
            stateManager.Initialize();

            if (cameraManager == null) cameraManager = CameraManager.singleton;
            cameraManager.Init(stateManager);
            cameraTransform = cameraManager.m_transform;
        }

        private void ControllerCheck()
        {
            inputController = new ThirdPersonInput();
            if (Gamepad.current != null)
            {
                xbox = true;
                gamepad = Gamepad.current;
            }
            else
            {
                xbox = false;
            }
        }
        #endregion

        #region Updates

        private void Update()
        {
            delta = Time.deltaTime;
            lockOnBuffer -= delta;
            GetInput_Update();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    ApplyInput_Update();
                    stateManager.Tick(delta);
                    cameraManager.Tick(delta, camHorizontal, camVertical);//    TODO : CAUTION!!! was in Fixed Update
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }

        private void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            GetInput_FixedUpdate();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    ApplyInput_FixedUpdate();
                    stateManager.Fixed_Tick(delta);    
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }

        private void GetInput_Update()
        {
            if (!xbox)
            {
                //  TODO : Get keyboard equivalent buttons pressed
            }
            else
            {
                if (gamepad == null) { xbox = false; return; }

                a_input = gamepad.aButton.isPressed;
                b_input = gamepad.bButton.isPressed;
                x_input = gamepad.xButton.isPressed;
                y_input = gamepad.yButton.isPressed;

                rb_input = gamepad.rightShoulder.isPressed;
                lb_input = gamepad.leftShoulder.isPressed;

                lt_input = gamepad.leftTrigger.isPressed;
                rt_input = gamepad.rightTrigger.isPressed;

                LockOnSafetyCheck();
            }

            if (b_input)
                b_timer += delta;
        }

        private void GetInput_FixedUpdate()
        {
            if (!xbox)
            {
                //  TODO : use mouse object to get camRotation delta
                //  TODO : use WASD Composite to get movement delta
            }
            else
            {
                if (gamepad == null) { xbox = false; return; }

                vertical = gamepad.leftStick.ReadValue().y;
                horizontal = gamepad.leftStick.ReadValue().x;

                camVertical = gamepad.rightStick.ReadValue().y;
                camHorizontal = gamepad.rightStick.ReadValue().x;
            }
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
                        if (camHorizontal < -0.8f)
                        {
                            enemyIndex--;
                            if (enemyIndex < 0) { enemyIndex = m_enemies.Count - 1; }
                            m_lockOnTransform.value = m_enemies[enemyIndex];
                            lockOnBuffer = 0.5f;
                        }
                        else if (camHorizontal > 0.8f)
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
                    float distanceToTarget = Vector3.Distance(stateManager.m_transform.position, m_lockOnTransform.value.position);
                    if (distanceToTarget > lockOnMaxDistance) m_lockOnTransform.value = null;
                }
            }

            //  Assign LockOn Target
            if (stateManager.m_input.lockOnTransform != m_lockOnTransform.value)
                stateManager.m_input.lockOnTransform = m_lockOnTransform.value;
        }

        private void ApplyInput_Update()
        {
            stateManager.m_input.rb = rb_input;
            stateManager.m_input.lb = lb_input;
            stateManager.m_input.rt = rt_input;
            stateManager.m_input.lt = lt_input;

            if (b_input)
            {
                b_timer += delta;

                if (b_timer > 0.5f)
                {   //  Hold B to RUN
                    stateManager.m_states.isRunning = true;
                }
            }
            else
            {
                if (b_timer > 0.05f && b_timer < 0.5f)
                {   //  Tap B to ROLL
                    stateManager.HandleRoll();
                }

                b_timer = 0f;
                stateManager.m_states.isRunning = false;
            }

            stateManager.m_states.isLockedOn = isLockedOn;
        }

        private void ApplyInput_FixedUpdate()
        {
            //  Pass Move Input to StateManager Input
            stateManager.m_input.vertical = vertical;
            stateManager.m_input.horizontal = horizontal;
            stateManager.m_input.moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

            //  Moves player based on camera angle
            Vector3 moveDir2 = cameraTransform.forward * vertical;
            moveDir2 += cameraTransform.right * horizontal;
            moveDir2.Normalize();

            //  Pass Move Direction to StateManager Input
            if (stateManager.m_characterState != StateManager.CharacterState.ROLL)
                stateManager.m_input.moveDir = moveDir2;
        }
        #endregion

        #region Input Events

        private static void SelectInput(InputAction.CallbackContext context)
        {   //  TODO : apply behaviour
            Debug.Log("Select Button was pressed!");
        }

        private static void LeftStickInput(InputAction.CallbackContext context)
        {   //  TODO : apply behaviour
            Debug.Log("Left Stick was pressed!");
        }

        private void LockOnInput(InputAction.CallbackContext context)
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
        #endregion
    }

    #region Helper Enums
    public enum GamePhase
    {
        IN_GAME, IN_MENU, IN_INVENTORY
    }

    public enum InputType
    {
        RT, LT, RB, LB
    }
    #endregion
}