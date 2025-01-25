using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    public GameObject Spline; // Reference to the SplineContainer
    private SplineContainer splineContainer;
    public float speed = 1.0f; // Movement speed
    private float t = 0f; // Normalized position along the spline (0 to 1)

    void Start()
    {
        splineContainer = Spline.GetComponent<SplineContainer>();
    }
    void Update()
    {
        if (splineContainer == null) return;

        // Update the normalized position based on speed and time
        t += speed * Time.deltaTime;

        // Loop back to the start if it exceeds 1
        if (t > 1f) t -= 1f;

        // Get the position on the spline
        Vector3 position = splineContainer.EvaluatePosition(t);

        // Update the GameObject's position
        transform.position = position;

        // Optionally, align the GameObject to the spline's tangent
        Vector3 rotation = splineContainer.EvaluateTangent(t);
        transform.rotation = Quaternion.LookRotation(rotation);
    }
}
