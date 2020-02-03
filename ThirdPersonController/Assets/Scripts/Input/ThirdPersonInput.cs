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
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a1c13fb7-c659-43aa-a641-7d564579d2bc"",
                    ""path"": ""<XInputController>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f285ab2-b14f-4730-84ff-57bf9262bbbe"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PC"",
                    ""action"": ""LStick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e67ca712-73af-4dff-8676-f630cb0d6700"",
                    ""path"": ""<XInputController>/leftStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Xbox"",
                    ""action"": ""LStick"",
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
                    ""action"": ""RStick"",
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
                    ""action"": ""RStick"",
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
        m_CharacterInput_Select = m_CharacterInput.FindAction("Select", throwIfNotFound: true);
        m_CharacterInput_LStick = m_CharacterInput.FindAction("LStick", throwIfNotFound: true);
        m_CharacterInput_RStick = m_CharacterInput.FindAction("RStick", throwIfNotFound: true);
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
    private readonly InputAction m_CharacterInput_Select;
    private readonly InputAction m_CharacterInput_LStick;
    private readonly InputAction m_CharacterInput_RStick;
    public struct CharacterInputActions
    {
        private @ThirdPersonInput m_Wrapper;
        public CharacterInputActions(@ThirdPersonInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Select => m_Wrapper.m_CharacterInput_Select;
        public InputAction @LStick => m_Wrapper.m_CharacterInput_LStick;
        public InputAction @RStick => m_Wrapper.m_CharacterInput_RStick;
        public InputActionMap Get() { return m_Wrapper.m_CharacterInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterInputActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterInputActions instance)
        {
            if (m_Wrapper.m_CharacterInputActionsCallbackInterface != null)
            {
                @Select.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnSelect;
                @LStick.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLStick;
                @LStick.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLStick;
                @LStick.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnLStick;
                @RStick.started -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRStick;
                @RStick.performed -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRStick;
                @RStick.canceled -= m_Wrapper.m_CharacterInputActionsCallbackInterface.OnRStick;
            }
            m_Wrapper.m_CharacterInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @LStick.started += instance.OnLStick;
                @LStick.performed += instance.OnLStick;
                @LStick.canceled += instance.OnLStick;
                @RStick.started += instance.OnRStick;
                @RStick.performed += instance.OnRStick;
                @RStick.canceled += instance.OnRStick;
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
        void OnSelect(InputAction.CallbackContext context);
        void OnLStick(InputAction.CallbackContext context);
        void OnRStick(InputAction.CallbackContext context);
    }
}
