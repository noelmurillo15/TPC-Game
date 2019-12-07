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
            ""name"": ""ThirdPersonXboxInput"",
            ""id"": ""9e1a7afd-7157-4bca-b930-a0ba213e2547"",
            ""actions"": [
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
        // ThirdPersonXboxInput
        m_ThirdPersonXboxInput = asset.GetActionMap("ThirdPersonXboxInput");
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

    // ThirdPersonXboxInput
    private readonly InputActionMap m_ThirdPersonXboxInput;
    private IThirdPersonXboxInputActions m_ThirdPersonXboxInputActionsCallbackInterface;
    private readonly InputAction m_ThirdPersonXboxInput_Select;
    private readonly InputAction m_ThirdPersonXboxInput_LStick;
    private readonly InputAction m_ThirdPersonXboxInput_RStick;
    public struct ThirdPersonXboxInputActions
    {
        private ThirdPersonInput m_Wrapper;
        public ThirdPersonXboxInputActions(ThirdPersonInput wrapper) { m_Wrapper = wrapper; }
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
    public interface IThirdPersonXboxInputActions
    {
        void OnSelect(InputAction.CallbackContext context);
        void OnLStick(InputAction.CallbackContext context);
        void OnRStick(InputAction.CallbackContext context);
    }
}
