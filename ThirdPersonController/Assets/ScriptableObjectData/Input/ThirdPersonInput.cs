// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/ThirdPersonInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @ThirdPersonInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @ThirdPersonInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ThirdPersonInput"",
    ""maps"": [
        {
            ""name"": ""CharacterInput"",
            ""id"": ""9e1a7afd-7157-4bca-b930-a0ba213e2547"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""016be5ce-2532-4264-92d0-e34effed1f07"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Camera Rotation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d17578ed-bc43-428b-a0e9-8b15f944bfb2"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""StickDeadzone"",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""e50ffb79-be6d-4f28-a626-149a184009be"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockOnToggle"",
                    ""type"": ""Button"",
                    ""id"": ""d76e5f02-4c4f-4b77-9fc8-2921a221896b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RB"",
                    ""type"": ""Button"",
                    ""id"": ""d99d452c-2b41-4a38-86fa-82c28cc202fb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LB"",
                    ""type"": ""Button"",
                    ""id"": ""6450fd32-bd2e-4b8a-af64-408976023375"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RT"",
                    ""type"": ""Button"",
                    ""id"": ""6f21ad24-52eb-45eb-a773-6472f2bd9da7"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LT"",
                    ""type"": ""Button"",
                    ""id"": ""f66ae0d2-a03a-4129-a5a3-99328e03e1e5"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""A"",
                    ""type"": ""Button"",
                    ""id"": ""ecef11ef-1547-4991-b7bb-c342f03c9bf0"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Roll"",
                    ""type"": ""Button"",
                    ""id"": ""b2b18d2b-d23c-4811-a9a1-9baca5ae5f61"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""X"",
                    ""type"": ""Button"",
                    ""id"": ""0acba0fe-2f42-4537-a315-6bf7bfa3219f"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Y"",
                    ""type"": ""Button"",
                    ""id"": ""0c66d08b-8dc2-4022-826a-3c6c4722dbc2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a1c13fb7-c659-43aa-a641-7d564579d2bc"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb93c7ad-847b-4010-bfa5-7c4fa72ece08"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc312608-3678-4e0f-aeae-f31cc55d5f76"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""LockOnToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46ce1872-15a3-4143-92c2-16af3cb98d2f"",
                    ""path"": ""<Keyboard>/z"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""LockOnToggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2300a148-57f5-42d6-84be-55a1b0d1b93e"",
                    ""path"": ""<XInputController>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""48e91773-4750-47ca-960a-e61b753eba89"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""76848c4b-0a78-45cf-a6e5-f396ae8cf6ff"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""134ebe94-2644-4155-834c-5e4b8cc56162"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d7780d9e-8128-4a80-8cb5-1c51df672f89"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ecdb4c6d-5209-461e-ad17-9679ffb22acb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""48038884-af94-4826-a2db-44ebab7e804d"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Camera Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a4c3d3f2-3ea0-44d4-a5fc-5379a2517ba3"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(x=0.5,y=0.5)"",
                    ""groups"": ""PC"",
                    ""action"": ""Camera Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20dff451-f96e-4556-bf04-a6a9468c4f12"",
                    ""path"": ""<XInputController>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""RB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d001357a-7504-4de9-87fc-09a5aee520ad"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""RB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c8482e70-5a6b-4442-b988-083d06abffff"",
                    ""path"": ""<XInputController>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""LB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6793c56f-49ee-40a6-94fd-ab8e85974200"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""LB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""70c7157c-c888-46fe-b8b1-97367d51ffc2"",
                    ""path"": ""<XInputController>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f4a460d8-4861-4d46-8ebf-b4a04d4cec8a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""A"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""feb1f334-ffd8-4a39-9f5f-76fab52bc73b"",
                    ""path"": ""<XInputController>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""da4f05d2-eedc-4ef0-a68a-a28efb1bfaea"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ab4f5295-0a04-49a2-b049-321f7fc736a1"",
                    ""path"": ""<XInputController>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f942ade-759c-41bf-a532-e0f947e8b92c"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""X"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62e9b0c9-16bb-44d5-9b14-7dc882417bda"",
                    ""path"": ""<XInputController>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3d758377-4339-4fa8-a14f-4b6e4bda0721"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Y"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8a5706ea-6dad-4e18-ad03-4683e2cc4500"",
                    ""path"": ""<XInputController>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""RT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cbc57a1f-df74-4603-9f28-7f1fb77208d7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""RT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7104f290-4a1c-40e2-b7da-cea392a53dbc"",
                    ""path"": ""<XInputController>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""LT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36aef8cf-efc7-4e91-8783-93633d86a35c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""LT"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Xbox"",
            ""bindingGroup"": ""Xbox"",
            ""devices"": [
                {
                    ""devicePath"": ""<XInputController>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""PC"",
            ""bindingGroup"": ""PC"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // CharacterInput
        m_CharacterInput = asset.FindActionMap("CharacterInput", throwIfNotFound: true);
        m_CharacterInput_Movement = m_CharacterInput.FindAction("Movement", throwIfNotFound: true);
        m_CharacterInput_CameraRotation = m_CharacterInput.FindAction("Camera Rotation", throwIfNotFound: true);
        m_CharacterInput_Pause = m_CharacterInput.FindAction("Pause", throwIfNotFound: true);
        m_CharacterInput_LockOnToggle = m_CharacterInput.FindAction("LockOnToggle", throwIfNotFound: true);
        m_CharacterInput_RB = m_CharacterInput.FindAction("RB", throwIfNotFound: true);
        m_CharacterInput_LB = m_CharacterInput.FindAction("LB", throwIfNotFound: true);
        m_CharacterInput_RT = m_CharacterInput.FindAction("RT", throwIfNotFound: true);
        m_CharacterInput_LT = m_CharacterInput.FindAction("LT", throwIfNotFound: true);
        m_CharacterInput_A = m_CharacterInput.FindAction("A", throwIfNotFound: true);
        m_CharacterInput_Roll = m_CharacterInput.FindAction("Roll", throwIfNotFound: true);
        m_CharacterInput_X = m_CharacterInput.FindAction("X", throwIfNotFound: true);
        m_CharacterInput_Y = m_CharacterInput.FindAction("Y", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // CharacterInput
    private readonly InputActionMap m_CharacterInput;
    private ICharacterInputActions m_CharacterInputActionsCallbackInterface;
    private readonly InputAction m_CharacterInput_Movement;
    private readonly InputAction m_CharacterInput_CameraRotation;
    private readonly InputAction m_CharacterInput_Pause;
    private readonly InputAction m_CharacterInput_LockOnToggle;
    private readonly InputAction m_CharacterInput_RB;
    private readonly InputAction m_CharacterInput_LB;
    private readonly InputAction m_CharacterInput_RT;
    private readonly InputAction m_CharacterInput_LT;
    private readonly InputAction m_CharacterInput_A;
    private readonly InputAction m_CharacterInput_Roll;
    private readonly InputAction m_CharacterInput_X;
    private readonly InputAction m_CharacterInput_Y;
    public struct CharacterInputActions
    {
        private @ThirdPersonInput m_Wrapper;
        public CharacterInputActions(@ThirdPersonInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CharacterInput_Movement;
        public InputAction @CameraRotation => m_Wrapper.m_CharacterInput_CameraRotation;
        public InputAction @Pause => m_Wrapper.m_CharacterInput_Pause;
        public InputAction @LockOnToggle => m_Wrapper.m_CharacterInput_LockOnToggle;
        public InputAction @RB => m_Wrapper.m_CharacterInput_RB;
        public InputAction @LB => m_Wrapper.m_CharacterInput_LB;
        public InputAction @RT => m_Wrapper.m_CharacterInput_RT;
        public InputAction @LT => m_Wrapper.m_CharacterInput_LT;
        public InputAction @A => m_Wrapper.m_CharacterInput_A;
        public InputAction @Roll => m_Wrapper.m_CharacterInput_Roll;
        public InputAction @X => m_Wrapper.m_CharacterInput_X;
        public InputAction @Y => m_Wrapper.m_CharacterInput_Y;
        public InputActionMap Get() { return m_Wrapper.m_CharacterInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterInputActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterInputActions instance)
        {
            if (m_Wrapper.m_CharacterInputActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnMovement;
                @CameraRotation.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnCameraRotation;
                @CameraRotation.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnCameraRotation;
                @Pause.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnPause;
                @LockOnToggle.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLockOnToggle;
                @LockOnToggle.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLockOnToggle;
                @LockOnToggle.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLockOnToggle;
                @RB.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRB;
                @RB.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRB;
                @RB.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRB;
                @LB.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLB;
                @LB.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLB;
                @LB.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLB;
                @RT.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRT;
                @RT.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRT;
                @RT.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRT;
                @LT.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLT;
                @LT.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLT;
                @LT.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLT;
                @A.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnA;
                @A.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnA;
                @A.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnA;
                @Roll.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRoll;
                @Roll.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRoll;
                @Roll.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRoll;
                @X.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnX;
                @X.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnX;
                @X.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnX;
                @Y.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnY;
                @Y.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnY;
                @Y.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnY;
            }
            m_Wrapper.m_CharacterInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @CameraRotation.started += instance.OnCameraRotation;
                @CameraRotation.performed += instance.OnCameraRotation;
                @CameraRotation.canceled += instance.OnCameraRotation;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @LockOnToggle.started += instance.OnLockOnToggle;
                @LockOnToggle.performed += instance.OnLockOnToggle;
                @LockOnToggle.canceled += instance.OnLockOnToggle;
                @RB.started += instance.OnRB;
                @RB.performed += instance.OnRB;
                @RB.canceled += instance.OnRB;
                @LB.started += instance.OnLB;
                @LB.performed += instance.OnLB;
                @LB.canceled += instance.OnLB;
                @RT.started += instance.OnRT;
                @RT.performed += instance.OnRT;
                @RT.canceled += instance.OnRT;
                @LT.started += instance.OnLT;
                @LT.performed += instance.OnLT;
                @LT.canceled += instance.OnLT;
                @A.started += instance.OnA;
                @A.performed += instance.OnA;
                @A.canceled += instance.OnA;
                @Roll.started += instance.OnRoll;
                @Roll.performed += instance.OnRoll;
                @Roll.canceled += instance.OnRoll;
                @X.started += instance.OnX;
                @X.performed += instance.OnX;
                @X.canceled += instance.OnX;
                @Y.started += instance.OnY;
                @Y.performed += instance.OnY;
                @Y.canceled += instance.OnY;
            }
        }
    }
    public CharacterInputActions @CharacterInput => new CharacterInputActions(this);
    private int m_XboxSchemeIndex = -1;
    public InputControlScheme XboxScheme
    {
        get
        {
            if (m_XboxSchemeIndex == -1) m_XboxSchemeIndex = asset.FindControlSchemeIndex("Xbox");
            return asset.controlSchemes[m_XboxSchemeIndex];
        }
    }
    private int m_PCSchemeIndex = -1;
    public InputControlScheme PCScheme
    {
        get
        {
            if (m_PCSchemeIndex == -1) m_PCSchemeIndex = asset.FindControlSchemeIndex("PC");
            return asset.controlSchemes[m_PCSchemeIndex];
        }
    }
    public interface ICharacterInputActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCameraRotation(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnLockOnToggle(InputAction.CallbackContext context);
        void OnRB(InputAction.CallbackContext context);
        void OnLB(InputAction.CallbackContext context);
        void OnRT(InputAction.CallbackContext context);
        void OnLT(InputAction.CallbackContext context);
        void OnA(InputAction.CallbackContext context);
        void OnRoll(InputAction.CallbackContext context);
        void OnX(InputAction.CallbackContext context);
        void OnY(InputAction.CallbackContext context);
    }
}
