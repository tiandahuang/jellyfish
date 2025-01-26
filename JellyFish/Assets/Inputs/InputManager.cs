using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public bool CurrentRightTriggerValue { get; private set; }

    public AudioSource audioSource;
    private bool isPlaying = false;

    [Header("Input Settings")]
    public InputActionAsset inputActionAsset;

    private InputAction floatAction;
    private InputActionMap inputActionMap;
    
    public UnityEvent<float> FloatInputAction;
    public UnityEvent<float> SecondaryInputAction;
    public UnityEvent<float> BLEInputAction;

    
    public float Threshold = 120;
    public float ThresholdDecrement = 10;
    
    private bool isButtonPressed = false; // Tracks the current state of the button

    private bool isSecondaryButtonPressed = false;
    // Maybe it could be public
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
#if UNITY_ANDROID //&& !UNITY_EDITOR
        bool currentButtonState = OVRInput.Get(OVRInput.Button.One);

        // If the button state changes
        if (currentButtonState != isButtonPressed)
        {
            isButtonPressed = currentButtonState;
            if (isButtonPressed)
            {
                // Trigger event for button press
                OnFloatActionPerformedOldInputSystem(1);
            }
            else
            {
                OnFloatActionPerformedOldInputSystem(0);
            }
        }
        
        bool SecondaryButtonState = OVRInput.Get(OVRInput.Button.Two);

        // If the button state changes
        if (SecondaryButtonState != isSecondaryButtonPressed)
        {
            isSecondaryButtonPressed = SecondaryButtonState;
            if (isSecondaryButtonPressed)
            {
                // Trigger event for button press
                SecondaryTrigger(1);
            }
            else
            {
                SecondaryTrigger(0);
            }
        }

        if (CurrentRightTriggerValue && !isPlaying)
        {
            StartCoroutine(PlaySound());
        }
#endif
    }

    private void SecondaryTrigger(int p0)
    {
        SecondaryInputAction.Invoke(p0);
    }

    private IEnumerator PlaySound()
    {
        isPlaying = true;
        audioSource.Play();
        yield return new WaitForSeconds(2f);

        isPlaying = false;
    }

    void OnEnable()
    {
#if UNITY_EDITOR
        if (inputActionAsset != null)
        {
            inputActionMap = inputActionAsset.FindActionMap("PC");
            
            if (inputActionMap != null)
            {
                floatAction = inputActionMap.FindAction("Float");
            }

            if (floatAction != null)
            {
                floatAction.Enable();
                floatAction.performed += OnFloatActionPerformedNewInputSystem;
            }
        }
#endif
        // TODO bind to BLE signal here
        // ex) BLEManager.BLEFloatEvent += OnFloatActionPerformedOldInputSystem;
    }

    private void OnDisable()
    {
#if UNITY_EDITOR
        if (floatAction != null)
        {
            floatAction.Disable();
            floatAction.performed -= OnFloatActionPerformedNewInputSystem;
        }
        // quest input here. bind to that delegate
#endif
        // TODO unbind to BLE signal here
        // ex) BLEManager.BLEFloatEvent -= OnFloatActionPerformedOldInputSystem;
    }

    private void OnFloatActionPerformedNewInputSystem(InputAction.CallbackContext context)
    {
        // Get the float value
        float value = context.ReadValue<float>();
        Debug.Log($"Float value received: {value}");
        BLEInputAction.Invoke(value);
    }

    private void OnFloatActionPerformedOldInputSystem(float  value)
    {
        //Debug.Log($"[Old Input System] Float value received: {value}");
        FloatInputAction.Invoke(value);
    }

    public void InvokeFloatActionBLE(float value)
    {
        Debug.Log($"[Manual Invoke] Float value received: {value}");
        if (value >= Threshold)
        {
            BLEInputAction?.Invoke(1);
        }
        else
        {
            BLEInputAction?.Invoke(0);
        }
    }
}
