using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvaluateUI : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform targetObject;
    public float heightOffset = 0.1f;
    public GameObject mainCamera;

    private void LateUpdate()
    {
        if (targetObject == null || mainCamera == null)
            return;


        Vector3 targetPosition = targetObject.position + Vector3.up * heightOffset + Vector3.back * heightOffset * 10f;

        transform.position = targetPosition;

        transform.LookAt(mainCamera.transform);

        transform.rotation = Quaternion.LookRotation(transform.position - mainCamera.transform.position);
    }
}
