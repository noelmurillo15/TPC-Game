using UnityEngine;


namespace SA
{
    public class InputHandler : MonoBehaviour
    {
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
            InitInGame();
        }

        void InitInGame()
        {
            stateManager.Init();
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

            rt_axis = Input.GetAxis(StaticStrings.RT);
            if (rt_axis != 0f) rt_input = true;

            lt_axis = Input.GetAxis(StaticStrings.LT);
            if (lt_axis != 0f) lt_input = true;

            if (b_input)
                b_timer += delta;
        }

        void SetInput_Update()
        {
            stateManager.input.rb = rb_input;
            stateManager.input.lb = lb_input;
            stateManager.input.rt = rt_input;
            stateManager.input.lt = lt_input;
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
            stateManager.input.vertical = vertical;
            stateManager.input.horizontal = horizontal;
            stateManager.input.moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

            Vector3 moveDir2 = cameraTransform.forward * vertical;
            moveDir2 += cameraTransform.right * horizontal;

            moveDir2.Normalize();
            stateManager.input.moveDir = moveDir2;
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