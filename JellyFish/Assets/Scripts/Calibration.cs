using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Calibration : MonoBehaviour
{
    private float OriginalThreshold;
    private bool StartDecrementing = true;

    public TMP_Text CalibrationText; 
    void Start()
    {
        InputManager.Instance.FloatInputAction.AddListener(OnFloatInput);
        OriginalThreshold = InputManager.Instance.Threshold;
        CalibrationText.text = "Grip Value: " + OriginalThreshold;
    }

    private void OnFloatInput(float arg0)
    {
        if (StartDecrementing)
        {
            InputManager.Instance.Threshold -= InputManager.Instance.ThresholdDecrement;
        }
        else
        {
            InputManager.Instance.Threshold += InputManager.Instance.ThresholdDecrement;
        }
        
        if (InputManager.Instance.Threshold <= 0 || InputManager.Instance.Threshold >= OriginalThreshold)
        {
            StartDecrementing = !StartDecrementing;
        }
        
        CalibrationText.text = "Grip Value: " + InputManager.Instance.Threshold;
    }
}
