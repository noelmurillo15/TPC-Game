using UnityEngine;
using GameFramework.Managers;
using UnityEngine.InputSystem;


[CreateAssetMenu]
public class UI_InputHandler : ScriptableObject
{
    private Gamepad gamepad;
    private Mouse mouse;
    private Keyboard keyboard;
    private GameSettingsManager gameSettingsUI;
    private InputAction pauseAction = null;


    #region Unity Events

    private void Awake()
    {
        Debug.Log("UI_Input::Awake()");
        if (pauseAction == null) return;
        pauseAction = new InputAction("pause", binding: "<Gamepad>/start");
        pauseAction.AddBinding("<Keyboard>/escape");
    }

    private void OnEnable()
    {
        Debug.Log("UI_Input::OnEnable()");
        if (Gamepad.current != null)
        {
            EnableGamepad();
        }
        else
        {
            Debug.Log("No gamepad input detected!");
            if (Keyboard.current != null)
            {
                keyboard = Keyboard.current;
            }
            else
            {
                Debug.LogError("No keyboard input detected!");
            }

            if (Mouse.current != null)
            {
                mouse = Mouse.current;
            }
            else
            {
                Debug.LogError("No mouse input detected!");
            }
        }
    }

    private void OnDisable()
    {
        Debug.Log("UI_Input::OnDisable()");
        if (gamepad != null)
        {
            DisableGamepad();
        }
        keyboard = null;
        mouse = null;
    }

    private void OnDestroy()
    {
        Debug.Log("UI_Input::OnDestroy()");
        pauseAction.Dispose();
        pauseAction = null;
    }
    #endregion

    #region Helper Functions

    private void EnableGamepad()
    {
        Debug.Log("UI_Input::EnableGamepad()");
        gamepad = Gamepad.current;

        if (pauseAction == null)
        {
            Debug.Log("-    Pause Action was NULL");
            pauseAction = new InputAction("pause", binding: "<Gamepad>/start");
            pauseAction.AddBinding("<Keyboard>/escape");
        }

        pauseAction.performed += OnPauseAction;
        pauseAction.Enable();
    }

    private void DisableGamepad()
    {
        Debug.Log("UI_Input::DisableGamepad()");
        if (pauseAction != null)
        {
            pauseAction.performed -= OnPauseAction;
            pauseAction.Disable();
        }
        gamepad = null;
    }
    #endregion

    #region Public Functions
    public void AssignGameSettings(GameSettingsManager gsm)
    {
        Debug.Log("UI_Input::AssignGameSettings()");
        gameSettingsUI = gsm;
        if (gamepad != null) return;
        if (Gamepad.current != null)
        {
            EnableGamepad();
        }
    }
    #endregion

    #region Input Callbacks

    private void OnPauseAction(InputAction.CallbackContext context)
    {
        Debug.Log("UI_Input::OnPauseAction()");
        if (gameSettingsUI == null) return;
        if (!gameSettingsUI.paused)
        {
            gameSettingsUI.Pause();
        }
        else
        {
            gameSettingsUI.Resume();
        }
    }
    #endregion
}
