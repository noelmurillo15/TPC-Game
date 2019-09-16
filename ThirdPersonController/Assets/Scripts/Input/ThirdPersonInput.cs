// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/ThirdPersonInput.inputactions'

using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class ThirdPersonInput : IInputActionCollection
{
    private InputActionAsset asset;
    public ThirdPersonInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""ThirdPersonInput"",
    ""maps"": [
        {
            ""name"": ""ThirdPersonKeyboardInput"",
            ""id"": ""5fdced05-a5e7-483d-bec0-e0d1abb8f526"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""9c71fb59-6a7f-4183-972c-cd0ef2f6a6d9"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f1fbc23c-9794-4350-a834-30630b54688b"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""ThirdPersonXboxInput"",
            ""id"": ""9e1a7afd-7157-4bca-b930-a0ba213e2547"",
            ""actions"": [
                {
                    ""name"": ""Start"",
                    ""type"": ""Button"",
                    ""id"": ""a6d4cd1f-f7ab-4df2-913d-09e5be0bd093"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""e50ffb79-be6d-4f28-a626-149a184009be"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LStick"",
                    ""type"": ""Button"",
                    ""id"": ""8b688111-ba05-4fb7-b4bd-8a9c05ffd9de"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RStick"",
                    ""type"": ""Button"",
                    ""id"": ""d76e5f02-4c4f-4b77-9fc8-2921a221896b"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""24eb41a7-6b81-46de-808e-5369e6afd2af"",
                    ""path"": ""<XInputController>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb93c7ad-847b-4010-bfa5-7c4fa72ece08"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f285ab2-b14f-4730-84ff-57bf9262bbbe"",
                    ""path"": ""<XInputController>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""46ce1872-15a3-4143-92c2-16af3cb98d2f"",
                    ""path"": ""<XInputController>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // ThirdPersonKeyboardInput
        m_ThirdPersonKeyboardInput = asset.GetActionMap("ThirdPersonKeyboardInput");
        m_ThirdPersonKeyboardInput_Newaction = m_ThirdPersonKeyboardInput.GetAction("New action");
        // ThirdPersonXboxInput
        m_ThirdPersonXboxInput = asset.GetActionMap("ThirdPersonXboxInput");
        m_ThirdPersonXboxInput_Start = m_ThirdPersonXboxInput.GetAction("Start");
        m_ThirdPersonXboxInput_Select = m_ThirdPersonXboxInput.GetAction("Select");
        m_ThirdPersonXboxInput_LStick = m_ThirdPersonXboxInput.GetAction("LStick");
        m_ThirdPersonXboxInput_RStick = m_ThirdPersonXboxInput.GetAction("RStick");
    }

    ~ThirdPersonInput()
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

    // ThirdPersonKeyboardInput
    private readonly InputActionMap m_ThirdPersonKeyboardInput;
    private IThirdPersonKeyboardInputActions m_ThirdPersonKeyboardInputActionsCallbackInterface;
    private readonly InputAction m_ThirdPersonKeyboardInput_Newaction;
    public struct ThirdPersonKeyboardInputActions
    {
        private ThirdPersonInput m_Wrapper;
        public ThirdPersonKeyboardInputActions(ThirdPersonInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Newaction => m_Wrapper.m_ThirdPersonKeyboardInput_Newaction;
        public InputActionMap Get() { return m_Wrapper.m_ThirdPersonKeyboardInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ThirdPersonKeyboardInputActions set) { return set.Get(); }
        public void SetCallbacks(IThirdPersonKeyboardInputActions instance)
        {
            if (m_Wrapper.m_ThirdPersonKeyboardInputActionsCallbackInterface != null)
            {
                Newaction.started -= m_Wrapper.m_ThirdPersonKeyboardInputActionsCallbackInterface.OnNewaction;
                Newaction.performed -= m_Wrapper.m_ThirdPersonKeyboardInputActionsCallbackInterface.OnNewaction;
                Newaction.canceled -= m_Wrapper.m_ThirdPersonKeyboardInputActionsCallbackInterface.OnNewaction;
            }
            m_Wrapper.m_ThirdPersonKeyboardInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                Newaction.started += instance.OnNewaction;
                Newaction.performed += instance.OnNewaction;
                Newaction.canceled += instance.OnNewaction;
            }
        }
    }
    public ThirdPersonKeyboardInputActions @ThirdPersonKeyboardInput => new ThirdPersonKeyboardInputActions(this);

    // ThirdPersonXboxInput
    private readonly InputActionMap m_ThirdPersonXboxInput;
    private IThirdPersonXboxInputActions m_ThirdPersonXboxInputActionsCallbackInterface;
    private readonly InputAction m_ThirdPersonXboxInput_Start;
    private readonly InputAction m_ThirdPersonXboxInput_Select;
    private readonly InputAction m_ThirdPersonXboxInput_LStick;
    private readonly InputAction m_ThirdPersonXboxInput_RStick;
    public struct ThirdPersonXboxInputActions
    {
        private ThirdPersonInput m_Wrapper;
        public ThirdPersonXboxInputActions(ThirdPersonInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Start => m_Wrapper.m_ThirdPersonXboxInput_Start;
        public InputAction @Select => m_Wrapper.m_ThirdPersonXboxInput_Select;
        public InputAction @LStick => m_Wrapper.m_ThirdPersonXboxInput_LStick;
        public InputAction @RStick => m_Wrapper.m_ThirdPersonXboxInput_RStick;
        public InputActionMap Get() { return m_Wrapper.m_ThirdPersonXboxInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ThirdPersonXboxInputActions set) { return set.Get(); }
        public void SetCallbacks(IThirdPersonXboxInputActions instance)
        {
            if (m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface != null)
            {
                Start.started -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnStart;
                Start.performed -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnStart;
                Start.canceled -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnStart;
                Select.started -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnSelect;
                Select.performed -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnSelect;
                Select.canceled -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnSelect;
                LStick.started -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnLStick;
                LStick.performed -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnLStick;
                LStick.canceled -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnLStick;
                RStick.started -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnRStick;
                RStick.performed -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnRStick;
                RStick.canceled -= m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface.OnRStick;
            }
            m_Wrapper.m_ThirdPersonXboxInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                Start.started += instance.OnStart;
                Start.performed += instance.OnStart;
                Start.canceled += instance.OnStart;
                Select.started += instance.OnSelect;
                Select.performed += instance.OnSelect;
                Select.canceled += instance.OnSelect;
                LStick.started += instance.OnLStick;
                LStick.performed += instance.OnLStick;
                LStick.canceled += instance.OnLStick;
                RStick.started += instance.OnRStick;
                RStick.performed += instance.OnRStick;
                RStick.canceled += instance.OnRStick;
            }
        }
    }
    public ThirdPersonXboxInputActions @ThirdPersonXboxInput => new ThirdPersonXboxInputActions(this);
    public interface IThirdPersonKeyboardInputActions
    {
        void OnNewaction(InputAction.CallbackContext context);
    }
    public interface IThirdPersonXboxInputActions
    {
        void OnStart(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnLStick(InputAction.CallbackContext context);
        void OnRStick(InputAction.CallbackContext context);
    }
}
