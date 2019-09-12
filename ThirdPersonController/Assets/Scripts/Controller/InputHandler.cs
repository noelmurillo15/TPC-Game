using UnityEngine;


namespace SA
{
    public class InputHandler : MonoBehaviour
    {   //  Detects Input and passes it along to StateManager & CameraManager
        #region Class Member Variables
        float vertical;
        float horizontal;
        bool b_input;
        bool x_input;
        bool a_input;
        bool y_input;

        bool rb_input;
        bool rt_input;
        bool lb_input;
        bool lt_input;

        float rt_axis;
        float lt_axis;

        float b_timer;
        float delta;

        public GamePhase currentPhase;
        public StateManager stateManager;
        public CameraManager cameraManager;
        Transform cameraTransform;
        #endregion


        void Start()
        {
            Initialize();
        }

        void Initialize()
        {
            stateManager.m_resourcesManager = Resources.Load("ResourcesManager") as Managers.ResourcesManager;
            stateManager.m_resourcesManager.Initialize();

            stateManager.Initialize();
            cameraManager.Init(stateManager);
            cameraTransform = cameraManager.m_transform;
        }

        void Update()
        {
            delta = Time.deltaTime;
            GetInput_Update();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    SetInput_Update();
                    stateManager.Tick(delta);
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }

        void GetInput_Update()
        {
            b_input = Input.GetButton(StaticStrings.B);
            a_input = Input.GetButton(StaticStrings.A);
            x_input = Input.GetButton(StaticStrings.X);
            y_input = Input.GetButton(StaticStrings.Y);

            rb_input = Input.GetButton(StaticStrings.RB);
            lb_input = Input.GetButton(StaticStrings.LB);

            rt_input = Input.GetButton(StaticStrings.RT);
            lt_input = Input.GetButton(StaticStrings.LT);

            // rt_axis = Input.GetAxis(StaticStrings.RT);
            // if (rt_axis != 0f) rt_input = true;

            // lt_axis = Input.GetAxis(StaticStrings.LT);
            // if (lt_axis != 0f) lt_input = true;

            if (b_input)
                b_timer += delta;
        }

        void SetInput_Update()
        {
            stateManager.m_input.rb = rb_input;
            stateManager.m_input.lb = lb_input;
            stateManager.m_input.rt = rt_input;
            stateManager.m_input.lt = lt_input;
        }

        void FixedUpdate()
        {
            delta = Time.deltaTime;
            GetInput_FixedUpdate();

            switch (currentPhase)
            {
                case GamePhase.IN_GAME:
                    SetInput_FixedUpdate();
                    stateManager.Fixed_Tick(delta);
                    cameraManager.Tick(delta);
                    break;
                case GamePhase.IN_INVENTORY:
                    break;
                case GamePhase.IN_MENU:
                    break;
            }
        }

        void GetInput_FixedUpdate()
        {
            vertical = Input.GetAxis(StaticStrings.Vertical);
            horizontal = Input.GetAxis(StaticStrings.Horizontal);
        }

        void SetInput_FixedUpdate()
        {
            stateManager.m_input.vertical = vertical;
            stateManager.m_input.horizontal = horizontal;
            stateManager.m_input.moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

            Vector3 moveDir2 = cameraTransform.forward * vertical;
            moveDir2 += cameraTransform.right * horizontal;

            moveDir2.Normalize();
            stateManager.m_input.moveDir = moveDir2;
        }
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