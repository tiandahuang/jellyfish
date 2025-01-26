using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class FollowSpline : MonoBehaviour
{
    public GameObject Spline; // Reference to the SplineContainer
    private SplineContainer splineContainer;
    public float speed = 0.001f; // Movement speed
    private float t = 0f; // Normalized position along the spline (0 to 1)

    public GameObject Jelly;
    void Start()
    {
        splineContainer = Spline.GetComponent<SplineContainer>();
        if (InputManager.Instance != null)
        {
            InputManager.Instance.FloatInputAction.AddListener(HandleFloatSpeed);
        }
    }

    public void HandleFloatSpeed(float valuespeed)
    {
        //Debug.Log($"[Receiver] Received float value: {valuespeed}");
        speed = valuespeed * 0.05f + 0.01f;
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
        transform.parent.transform.position = position;

        // Optionally, align the GameObject to the spline's tangent
        Vector3 rotation = splineContainer.EvaluateTangent(t);

    }
}
