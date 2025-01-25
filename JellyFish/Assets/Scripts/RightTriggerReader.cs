using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTriggerReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rightTriggerValue = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        Debug.Log("Right Trigger Value: " + rightTriggerValue);

    }
}
