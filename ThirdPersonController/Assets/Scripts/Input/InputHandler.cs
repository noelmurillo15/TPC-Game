using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;


namespace SA
{
    public class InputHandler : MonoBehaviour
    {   //  Detects Input and passes it along to StateManager & CameraManager
        #region Class Member Variables
        //  New Input System        
        [SerializeField] bool xbox = false;
        Gamepad gamepad;
        Mouse m_mouse;

        //  Input Variables
        float vertical;
        float horizontal;

        float camVertical;
        float camHorizontal;

        bool b_input;
        bool x_input;
        bool a_input;
        bool y_input;

        bool rb_input;
        bool rt_input;
        bool lb_input;
        bool lt_input;

        float b_timer;
        float delta;

        //  References
        public GamePhase currentPhase;
        public StateManager stateManager;
        public CameraManager cameraManager;
        ThirdPersonInput inputController;
        Transform cameraTransform;

        //  LockOn
        public TransformVariable m_lockOnTransform;

        public bool isLockedOn;
        [SerializeField] float lockOnBuffer;
        float lockOnMaxDistance = 20f;

        //  Enemies
        public List<Transform> m_enemies = new List<Transform>();
        public int enemyIndex;
        #endregion


        #region Initialization
        void Awake()
        {
            ControllerCheck();
        }

        void Start()
        {
            Initialize();
        }

        void OnEnable()
        {
            if (xbox)
            {
                inputController.ThirdPersonXboxInput.Start.performed += PauseInput;
                inputController.ThirdPersonXboxInput.Start.Enable();
                inputController.ThirdPersonXboxInput.Select.performed += SelectInput;
                inputController.ThirdPersonXboxInput.Select.Enable();

                inputController.ThirdPersonXboxInput.LStick.performed += LeftStickInput;
                inputController.ThirdPersonXboxInput.LStick.Enable();
                inputController.ThirdPersonXboxInput.RStick.performed += LockOnInput;
                inputController.ThirdPersonXboxInput.RStick.Enable();
            }
        }

        void OnDisable()
        {
            if (xbox)
            {
                inputController.ThirdPersonXboxInput.Start.performed -= PauseInput;
                inputController.ThirdPersonXboxInput.Start.Disable();
                inputController.ThirdPersonXboxInput.Select.performed -= SelectInput;
                inputController.ThirdPersonXboxInput.Select.Disable();

                inputController.ThirdPersonXboxInput.LStick.performed -= LeftStickInput;
                inputController.ThirdPersonXboxInput.LStick.Disable();
                inputController.ThirdPersonXboxInput.RStick.performed -= LockOnInput;
                inputController.ThirdPersonXboxInput.RStick.Disable();
            }
        }

        void Initialize()
        {
            m_lockOnTransform.value = null;

            stateManager.m_resourcesManager = Resources.Load("ResourcesManager") as Managers.ResourcesManager;
            stateManager.m_resourcesManager.Initialize();
            stateManager.Initialize();

            if (cameraManager == null) cameraManager = CameraManager.singleton;
            cameraManager.Init(stateManager);
            cameraTransform = cameraManager.m_transform;
        }

        void ControllerCheck()
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
                m_mouse = Mouse.current;
            }
        }
        #endregion

        #region Updates
        void Update()
        {
            delta = Time.deltaTime;
            lockOnBuffer -= delta;
            GetInput_Update();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    ApplyInput_Update();
                    stateManager.Tick(delta);
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }

        void FixedUpdate()
        {
            delta = Time.fixedDeltaTime;
            GetInput_FixedUpdate();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    ApplyInput_FixedUpdate();
                    stateManager.Fixed_Tick(delta);
                    cameraManager.Tick(delta, camHorizontal, camVertical);
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }

        void GetInput_Update()
        {
            if (!xbox)
            {
                // b_input = Input.GetButton(StaticStrings.B);
                // a_input = Input.GetButton(StaticStrings.A);
                // x_input = Input.GetButton(StaticStrings.X);
                // y_input = Input.GetButton(StaticStrings.Y);

                // rb_input = Input.GetButton(StaticStrings.RB);
                // lb_input = Input.GetButton(StaticStrings.LB);

                // rt_input = Input.GetButton(StaticStrings.RT);
                // lt_input = Input.GetButton(StaticStrings.LT);
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

        void GetInput_FixedUpdate()
        {
            if (!xbox)
            {
                // vertical = Input.GetAxis(StaticStrings.Vertical);
                // horizontal = Input.GetAxis(StaticStrings.Horizontal);
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

        void LockOnSafetyCheck()
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

        void ApplyInput_Update()
        {
            stateManager.m_input.rb = rb_input;
            stateManager.m_input.lb = lb_input;
            stateManager.m_input.rt = rt_input;
            stateManager.m_input.lt = lt_input;

            stateManager.m_states.isLockedOn = isLockedOn;
        }

        void ApplyInput_FixedUpdate()
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
            stateManager.m_input.moveDir = moveDir2;
        }
        #endregion

        #region Input Events
        void PauseInput(InputAction.CallbackContext context)
        {   //  TODO : apply behaviour
            Debug.Log("Start Button was pressed!");
        }

        void SelectInput(InputAction.CallbackContext context)
        {   //  TODO : apply behaviour
            Debug.Log("Select Button was pressed!");
        }

        void LeftStickInput(InputAction.CallbackContext context)
        {   //  TODO : apply behaviour
            Debug.Log("Left Stick was pressed!");
        }

        void LockOnInput(InputAction.CallbackContext context)
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