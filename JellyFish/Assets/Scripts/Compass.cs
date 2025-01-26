using UnityEngine;

public class Compass : MonoBehaviour
{
    public Transform targetObject;   // ��ħ���� ����ų ��� ������Ʈ
    public Transform cameraTransform; // ī�޶��� Transform (���� Camera.main)
    public float hideAngleThreshold = 20f; // Y�� ���� ��20��
    public float distanceFromCamera = 1.0f; // ī�޶�κ��� ��ħ���� �Ÿ�

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
