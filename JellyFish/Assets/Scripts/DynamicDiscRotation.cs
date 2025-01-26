using UnityEngine;

public class DynamicDiscRotation : MonoBehaviour
{
    public float maxTiltAngle = 20f; // �������� �ִ� ���� (20��)
    public float tiltSpeed = 5f;     // �������� �ε巯�� (���� �ӵ�)

    private Vector3 previousPosition; // ���� �������� ��ġ
    private Vector3 velocity;         // ���� �ӵ� ����

    private void Start()
    {
        // ���� �� �ʱ� ��ġ ����
        previousPosition = transform.position;
    }

    private void Update()
    {
        // ���� �ӵ� ��� (���� ��ġ - ���� ��ġ)
        velocity = (transform.position - previousPosition) / Time.deltaTime;

        // �ӵ��� �������� ����� ���
        Vector3 tiltDirection = new Vector3(-velocity.z * 10f, 0, velocity.x * 10f); // x-z ��� ����
        float speedMagnitude = velocity.magnitude;

        // �ִ� ���� ����
        float tiltAngle = Mathf.Min(speedMagnitude, maxTiltAngle);

        // ��ǥ ȸ�� ��� (�⺻ X�� 90�� ���� ����)
        Quaternion baseRotation = Quaternion.Euler(90, 0, 0); // �⺻ X�� 90�� ȸ��
        Quaternion targetRotation = baseRotation * Quaternion.Euler(tiltDirection.normalized * tiltAngle);

        // �ε巯�� ȸ�� ����
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);

        // ���� ��ġ�� ���� ��ġ�� ����
        previousPosition = transform.position;
    }
}
    