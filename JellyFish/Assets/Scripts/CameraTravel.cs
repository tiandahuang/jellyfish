using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class CameraTravel : MonoBehaviour
{
    public float SplinePathRadius;
    public float MaxDistanceFromJelly = 0.0f;
    public GameObject Jelly;
    void Start()
    {
        
    }
    void Update()
    {
        float Radius = SplinePathRadius - MaxDistanceFromJelly;

        Vector3 NormalizedDirection = Jelly.transform.position.normalized;
        Vector3 NewPosition = NormalizedDirection * Radius;
        NewPosition.y = Jelly.transform.position.y;
        transform.position = NewPosition;

        // Calculate the rotation while locking the x-axis
        Quaternion Rot = Quaternion.LookRotation(NormalizedDirection);
        Rot = Quaternion.Euler(0, Rot.eulerAngles.y, Rot.eulerAngles.z);

        transform.rotation = Rot;
    }
}
