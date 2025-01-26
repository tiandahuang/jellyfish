using UnityEngine;

public class DynamicDiscRotation : MonoBehaviour
{
    public float maxTiltAngle = 20f; // 기울어지는 최대 각도 (20도)
    public float tiltSpeed = 5f;     // 기울어지는 부드러움 (보간 속도)

    private Vector3 previousPosition; // 이전 프레임의 위치
    private Vector3 velocity;         // 현재 속도 벡터

    private void Start()
    {
        // 시작 시 초기 위치 저장
        previousPosition = transform.position;
    }

    private void Update()
    {
        // 현재 속도 계산 (현재 위치 - 이전 위치)
        velocity = (transform.position - previousPosition) / Time.deltaTime;

        // 속도를 기준으로 기울임 계산
        Vector3 tiltDirection = new Vector3(-velocity.z * 10f, 0, velocity.x * 10f); // x-z 평면 기준
        float speedMagnitude = velocity.magnitude;

        // 최대 기울기 제한
        float tiltAngle = Mathf.Min(speedMagnitude, maxTiltAngle);

        // 목표 회전 계산 (기본 X축 90도 보정 포함)
        Quaternion baseRotation = Quaternion.Euler(90, 0, 0); // 기본 X축 90도 회전
        Quaternion targetRotation = baseRotation * Quaternion.Euler(tiltDirection.normalized * tiltAngle);

        // 부드러운 회전 적용
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);

        // 현재 위치를 이전 위치로 갱신
        previousPosition = transform.position;
    }
}
    