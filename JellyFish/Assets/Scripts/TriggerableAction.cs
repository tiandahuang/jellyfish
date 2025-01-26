using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerableAction : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputs;
    public UnityEvent Trigger;
    
    void Start()
    {
        inputs.FloatInputAction.AddListener(OnTriggered);
    }

    public void OnTriggered(float Triggered)
    {   
        Trigger.Invoke();
    }
}
