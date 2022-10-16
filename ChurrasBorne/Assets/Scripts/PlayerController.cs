//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/PlayerController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerController"",
    ""maps"": [
        {
            ""name"": ""Movimento"",
            ""id"": ""0f2fa741-c1cf-4309-b27b-3086f4173487"",
            ""actions"": [
                {
                    ""name"": ""Norte/Sul"",
                    ""type"": ""Button"",
                    ""id"": ""b3124b72-cd92-4333-93cd-de8c646f08a8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Leste / Oeste"",
                    ""type"": ""Button"",
                    ""id"": ""1964364a-1536-4a7b-84f2-5cc82aed8b0b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rolar"",
                    ""type"": ""Button"",
                    ""id"": ""20506db5-fb52-48c5-a8ff-7e414d737fb7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""69c6f311-9d19-404f-84c1-b9c6d98f09a5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""16d99780-6620-4238-abf4-ad6938562ee5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Curar"",
                    ""type"": ""Button"",
                    ""id"": ""8e8bb773-447a-4c40-8d05-a43f78d1d59f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""d9804a59-8427-4329-a55b-d2f910cf9da3"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Norte/Sul"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7128da3a-947f-4ad7-9ebf-5692fed4caa8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Norte/Sul"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e471f4ac-309e-49a8-b109-a79e1ba06c87"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Norte/Sul"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Setas"",
                    ""id"": ""9cd92c22-5cfb-4cbb-a848-95d70c8146ea"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Norte/Sul"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""7c6515a9-6589-462a-967e-ae64727d8255"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Norte/Sul"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c18fa4e2-d72f-421a-a820-12222cdeead9"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Norte/Sul"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controle"",
                    ""id"": ""1ee7882c-96fa-41d2-a7c2-96b8ff4df235"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Norte/Sul"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8183c473-98bb-46da-a1af-afec8ad9a5d4"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Norte/Sul"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""0ce1967e-2ae9-46f4-9d4d-e6d679c9ade8"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Norte/Sul"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""e6f55967-b567-4555-9c8a-3e812ba6e350"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leste / Oeste"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""001e7e51-2dfa-4c4a-8581-c609349e9078"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leste / Oeste"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e6712cb5-11c5-46b5-b211-647f5713593b"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leste / Oeste"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Setas"",
                    ""id"": ""1776fbcd-d899-4f16-8fa2-768eb4005632"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leste / Oeste"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d51de57a-e2a4-4daf-b506-58c17c8f6c2b"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leste / Oeste"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a3b9d578-03ed-4d89-920d-c3256a4ea859"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leste / Oeste"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Controle"",
                    ""id"": ""8f44bf81-7d4a-426e-a5b7-773c0dd00170"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leste / Oeste"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f7324266-cb1b-406c-8dd0-cb864e7fcbb5"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leste / Oeste"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f7e709ad-6e5e-44a5-8eaa-d886214d471d"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Leste / Oeste"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""33995b0a-3d06-4ebd-95d5-d75baa19ef0d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rolar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c936fedf-a739-4db9-8923-af5158db3b0a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rolar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a28d1d7-af53-4467-9e54-c9951d377f5f"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8861326a-86e1-4baf-a82f-d2cf68574a72"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f633cbdf-a6a0-42fc-b9c9-e6d505d3a3e0"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cedf5a18-4849-469d-95ab-fe5605cf8116"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e5086d9-0e81-4bd5-831b-88aaf1e621fe"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Curar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""212594ba-2cac-49cb-9aa4-db84458cbfb5"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Curar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""3ff5332d-f18a-4baa-8d33-8d6bd1df74ef"",
            ""actions"": [
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""9d57fb0c-e2fa-4bb5-8441-0d4e28ed8c61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a103544e-ba82-483e-89d1-41967ab3c309"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11b91e13-c992-4c96-9826-e728d5fb9154"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Tester"",
            ""id"": ""3d9c69ec-23f8-499f-bc71-36c1e7ffd1e4"",
            ""actions"": [
                {
                    ""name"": ""LKey"",
                    ""type"": ""Button"",
                    ""id"": ""e23eb623-70fe-4d11-a2a4-962c8e7fa665"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PKey"",
                    ""type"": ""Button"",
                    ""id"": ""ea025e6c-2c3e-471a-ad06-5c6b804bd59d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""65b6f02d-85cc-4a76-898d-78e310cd6b9d"",
                    ""path"": ""<Keyboard>/l"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""570893b8-eb36-4244-97ad-97369871b8db"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movimento
        m_Movimento = asset.FindActionMap("Movimento", throwIfNotFound: true);
        m_Movimento_NorteSul = m_Movimento.FindAction("Norte/Sul", throwIfNotFound: true);
        m_Movimento_LesteOeste = m_Movimento.FindAction("Leste / Oeste", throwIfNotFound: true);
        m_Movimento_Rolar = m_Movimento.FindAction("Rolar", throwIfNotFound: true);
        m_Movimento_MousePosition = m_Movimento.FindAction("MousePosition", throwIfNotFound: true);
        m_Movimento_Attack = m_Movimento.FindAction("Attack", throwIfNotFound: true);
        m_Movimento_Curar = m_Movimento.FindAction("Curar", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Pause = m_UI.FindAction("Pause", throwIfNotFound: true);
        // Tester
        m_Tester = asset.FindActionMap("Tester", throwIfNotFound: true);
        m_Tester_LKey = m_Tester.FindAction("LKey", throwIfNotFound: true);
        m_Tester_PKey = m_Tester.FindAction("PKey", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Movimento
    private readonly InputActionMap m_Movimento;
    private IMovimentoActions m_MovimentoActionsCallbackInterface;
    private readonly InputAction m_Movimento_NorteSul;
    private readonly InputAction m_Movimento_LesteOeste;
    private readonly InputAction m_Movimento_Rolar;
    private readonly InputAction m_Movimento_MousePosition;
    private readonly InputAction m_Movimento_Attack;
    private readonly InputAction m_Movimento_Curar;
    public struct MovimentoActions
    {
        private @PlayerController m_Wrapper;
        public MovimentoActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @NorteSul => m_Wrapper.m_Movimento_NorteSul;
        public InputAction @LesteOeste => m_Wrapper.m_Movimento_LesteOeste;
        public InputAction @Rolar => m_Wrapper.m_Movimento_Rolar;
        public InputAction @MousePosition => m_Wrapper.m_Movimento_MousePosition;
        public InputAction @Attack => m_Wrapper.m_Movimento_Attack;
        public InputAction @Curar => m_Wrapper.m_Movimento_Curar;
        public InputActionMap Get() { return m_Wrapper.m_Movimento; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovimentoActions set) { return set.Get(); }
        public void SetCallbacks(IMovimentoActions instance)
        {
            if (m_Wrapper.m_MovimentoActionsCallbackInterface != null)
            {
                @NorteSul.started -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnNorteSul;
                @NorteSul.performed -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnNorteSul;
                @NorteSul.canceled -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnNorteSul;
                @LesteOeste.started -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnLesteOeste;
                @LesteOeste.performed -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnLesteOeste;
                @LesteOeste.canceled -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnLesteOeste;
                @Rolar.started -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnRolar;
                @Rolar.performed -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnRolar;
                @Rolar.canceled -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnRolar;
                @MousePosition.started -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnMousePosition;
                @Attack.started -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnAttack;
                @Curar.started -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnCurar;
                @Curar.performed -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnCurar;
                @Curar.canceled -= m_Wrapper.m_MovimentoActionsCallbackInterface.OnCurar;
            }
            m_Wrapper.m_MovimentoActionsCallbackInterface = instance;
            if (instance != null)
            {
                @NorteSul.started += instance.OnNorteSul;
                @NorteSul.performed += instance.OnNorteSul;
                @NorteSul.canceled += instance.OnNorteSul;
                @LesteOeste.started += instance.OnLesteOeste;
                @LesteOeste.performed += instance.OnLesteOeste;
                @LesteOeste.canceled += instance.OnLesteOeste;
                @Rolar.started += instance.OnRolar;
                @Rolar.performed += instance.OnRolar;
                @Rolar.canceled += instance.OnRolar;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @Curar.started += instance.OnCurar;
                @Curar.performed += instance.OnCurar;
                @Curar.canceled += instance.OnCurar;
            }
        }
    }
    public MovimentoActions @Movimento => new MovimentoActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Pause;
    public struct UIActions
    {
        private @PlayerController m_Wrapper;
        public UIActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @Pause => m_Wrapper.m_UI_Pause;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Pause.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public UIActions @UI => new UIActions(this);

    // Tester
    private readonly InputActionMap m_Tester;
    private ITesterActions m_TesterActionsCallbackInterface;
    private readonly InputAction m_Tester_LKey;
    private readonly InputAction m_Tester_PKey;
    public struct TesterActions
    {
        private @PlayerController m_Wrapper;
        public TesterActions(@PlayerController wrapper) { m_Wrapper = wrapper; }
        public InputAction @LKey => m_Wrapper.m_Tester_LKey;
        public InputAction @PKey => m_Wrapper.m_Tester_PKey;
        public InputActionMap Get() { return m_Wrapper.m_Tester; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TesterActions set) { return set.Get(); }
        public void SetCallbacks(ITesterActions instance)
        {
            if (m_Wrapper.m_TesterActionsCallbackInterface != null)
            {
                @LKey.started -= m_Wrapper.m_TesterActionsCallbackInterface.OnLKey;
                @LKey.performed -= m_Wrapper.m_TesterActionsCallbackInterface.OnLKey;
                @LKey.canceled -= m_Wrapper.m_TesterActionsCallbackInterface.OnLKey;
                @PKey.started -= m_Wrapper.m_TesterActionsCallbackInterface.OnPKey;
                @PKey.performed -= m_Wrapper.m_TesterActionsCallbackInterface.OnPKey;
                @PKey.canceled -= m_Wrapper.m_TesterActionsCallbackInterface.OnPKey;
            }
            m_Wrapper.m_TesterActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LKey.started += instance.OnLKey;
                @LKey.performed += instance.OnLKey;
                @LKey.canceled += instance.OnLKey;
                @PKey.started += instance.OnPKey;
                @PKey.performed += instance.OnPKey;
                @PKey.canceled += instance.OnPKey;
            }
        }
    }
    public TesterActions @Tester => new TesterActions(this);
    public interface IMovimentoActions
    {
        void OnNorteSul(InputAction.CallbackContext context);
        void OnLesteOeste(InputAction.CallbackContext context);
        void OnRolar(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnCurar(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnPause(InputAction.CallbackContext context);
    }
    public interface ITesterActions
    {
        void OnLKey(InputAction.CallbackContext context);
        void OnPKey(InputAction.CallbackContext context);
    }
}
