using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform targetObject;   // 나침판이 가리킬 대상 오브젝트
    public Transform cameraTransform; // 카메라의 Transform (보통 Camera.main)
    public float hideAngleThreshold = 20f; // Y축 기준 ±20도
    public float distanceFromCamera = 1.0f; // 카메라로부터 나침판의 거리

    private void Update()
    {
        if (targetObject == null || cameraTransform == null)
            return;
        Vector3 targetDirection = targetObject.position - cameraTransform.position;

        float angleToTarget = Vector3.Angle(cameraTransform.forward, targetDirection);

        if (angleToTarget <= hideAngleThreshold)
        {
            SetCompassVisibility(false);
            return;
        }

        SetCompassVisibility(true);

        Vector3 compassPosition = cameraTransform.position + cameraTransform.forward * distanceFromCamera;
        transform.position = compassPosition;

        transform.rotation = Quaternion.LookRotation(targetDirection, Vector3.up);
    }

    private void SetCompassVisibility(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
