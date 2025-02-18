//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.11.2
//     from Assets/Inputs/Controls.inputactions
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

public partial class @Controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""PC"",
            ""id"": ""3dbd3f00-dca3-4dd0-bf83-da98faea7d0d"",
            ""actions"": [
                {
                    ""name"": ""Float"",
                    ""type"": ""Button"",
                    ""id"": ""28c2f66b-1b48-43d5-8f44-204fa0118a92"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""de839ae0-f508-4009-8f6a-41c65aa629b0"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Float"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ea8e35e5-e372-45c0-8e8e-63226755cc38"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": """",
                    ""action"": ""Float"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Quest"",
            ""id"": ""edb2d7ac-13dc-45ab-a9f6-69dd5c5ff57c"",
            ""actions"": [
                {
                    ""name"": ""Float"",
                    ""type"": ""Button"",
                    ""id"": ""5adfe02a-cc82-4b8b-b21c-4734e4507a0d"",
                    ""expectedControlType"": """",
                    ""processors"": ""Invert"",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8166d767-3be5-47dd-955b-4b7ce58ff3af"",
                    ""path"": ""<XRController>{RightHand}/{PrimaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Float"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f123281d-cc4d-45f6-8071-bf1836c8a228"",
                    ""path"": ""<XRController>{RightHand}/{SecondaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Float"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PC
        m_PC = asset.FindActionMap("PC", throwIfNotFound: true);
        m_PC_Float = m_PC.FindAction("Float", throwIfNotFound: true);
        // Quest
        m_Quest = asset.FindActionMap("Quest", throwIfNotFound: true);
        m_Quest_Float = m_Quest.FindAction("Float", throwIfNotFound: true);
    }

    ~@Controls()
    {
        UnityEngine.Debug.Assert(!m_PC.enabled, "This will cause a leak and performance issues, Controls.PC.Disable() has not been called.");
        UnityEngine.Debug.Assert(!m_Quest.enabled, "This will cause a leak and performance issues, Controls.Quest.Disable() has not been called.");
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

    // PC
    private readonly InputActionMap m_PC;
    private List<IPCActions> m_PCActionsCallbackInterfaces = new List<IPCActions>();
    private readonly InputAction m_PC_Float;
    public struct PCActions
    {
        private @Controls m_Wrapper;
        public PCActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Float => m_Wrapper.m_PC_Float;
        public InputActionMap Get() { return m_Wrapper.m_PC; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PCActions set) { return set.Get(); }
        public void AddCallbacks(IPCActions instance)
        {
            if (instance == null || m_Wrapper.m_PCActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PCActionsCallbackInterfaces.Add(instance);
            @Float.started += instance.OnFloat;
            @Float.performed += instance.OnFloat;
            @Float.canceled += instance.OnFloat;
        }

        private void UnregisterCallbacks(IPCActions instance)
        {
            @Float.started -= instance.OnFloat;
            @Float.performed -= instance.OnFloat;
            @Float.canceled -= instance.OnFloat;
        }

        public void RemoveCallbacks(IPCActions instance)
        {
            if (m_Wrapper.m_PCActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPCActions instance)
        {
            foreach (var item in m_Wrapper.m_PCActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PCActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PCActions @PC => new PCActions(this);

    // Quest
    private readonly InputActionMap m_Quest;
    private List<IQuestActions> m_QuestActionsCallbackInterfaces = new List<IQuestActions>();
    private readonly InputAction m_Quest_Float;
    public struct QuestActions
    {
        private @Controls m_Wrapper;
        public QuestActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Float => m_Wrapper.m_Quest_Float;
        public InputActionMap Get() { return m_Wrapper.m_Quest; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(QuestActions set) { return set.Get(); }
        public void AddCallbacks(IQuestActions instance)
        {
            if (instance == null || m_Wrapper.m_QuestActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_QuestActionsCallbackInterfaces.Add(instance);
            @Float.started += instance.OnFloat;
            @Float.performed += instance.OnFloat;
            @Float.canceled += instance.OnFloat;
        }

        private void UnregisterCallbacks(IQuestActions instance)
        {
            @Float.started -= instance.OnFloat;
            @Float.performed -= instance.OnFloat;
            @Float.canceled -= instance.OnFloat;
        }

        public void RemoveCallbacks(IQuestActions instance)
        {
            if (m_Wrapper.m_QuestActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IQuestActions instance)
        {
            foreach (var item in m_Wrapper.m_QuestActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_QuestActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public QuestActions @Quest => new QuestActions(this);
    public interface IPCActions
    {
        void OnFloat(InputAction.CallbackContext context);
    }
    public interface IQuestActions
    {
        void OnFloat(InputAction.CallbackContext context);
    }
}
