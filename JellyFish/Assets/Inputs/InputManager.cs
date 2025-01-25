using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputManager : MonoBehaviour
{
    public InputActionAsset inputActionAsset;

    private InputAction floatAction;
    private InputActionMap inputActionMap;

    void OnEnable()
    {
        if (inputActionAsset != null)
        {
            #if UNITY_EDITOR
                inputActionMap = inputActionAsset.FindActionMap("PC");
            #elif UNITY_ANDROID
                // Sus does this actually work??
                inputActionMap = inputActionAsset.FindActionMap("Quest");
            #endif
            
            if (inputActionMap != null)
            {
                print("map not null");
                floatAction = inputActionMap.FindAction("Float");
            }

            if (floatAction != null)
            {
                print("float not null");
                floatAction.Enable();
                floatAction.performed += OnFloatActionPerformed;
            }

        }
    }

    private void OnDisable()
    {
        if (floatAction != null)
        {
            floatAction.Disable();
            floatAction.performed -= OnFloatActionPerformed;
        }
    }
    
    private void OnFloatActionPerformed(InputAction.CallbackContext context)
    {
        // Get the float value
        float value = context.ReadValue<float>();
        Debug.Log($"Float value received: {value}");
    }
}
