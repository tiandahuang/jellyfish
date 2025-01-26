using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public float CurrentRightTriggerValue { get; private set; }

    public AudioSource audioSource;
    private bool isPlaying = false;

    [Header("Input Settings")]
    public InputActionAsset inputActionAsset;

    private InputAction floatAction;
    private InputActionMap inputActionMap;

    public UnityEvent<float> FloatInputAction; 
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
        float rightTriggerValue = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        Debug.Log("Right Trigger Value: " + rightTriggerValue);
        CurrentRightTriggerValue = rightTriggerValue;
        OnFloatActionPerformedOldInputSystem(rightTriggerValue);

        if (rightTriggerValue >= 0.7f && !isPlaying)
        {
            StartCoroutine(PlaySound());
        }
#endif
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
        FloatInputAction.Invoke(value);
    }

    private void OnFloatActionPerformedOldInputSystem(float  value)
    {
        //Debug.Log($"[Old Input System] Float value received: {value}");
        FloatInputAction.Invoke(value);
    }

    public void InvokeFloatActionManually(float value)
    {
        Debug.Log($"[Manual Invoke] Float value received: {value}");
        FloatInputAction?.Invoke(value);
    }
}
