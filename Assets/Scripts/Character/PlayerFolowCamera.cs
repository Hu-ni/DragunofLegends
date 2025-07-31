using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    // 추적할 플레이어 오브젝트를 인스펙터에서 할당합니다.
    public Transform playerTransform;

    // 카메라의 Y축 추적 속도를 조절합니다. 값이 높을수록 빠르게 따라갑니다.
    public float smoothSpeed = 0.125f;

    // 카메라의 X축 고정 위치입니다. 이 값을 기준으로 X축이 고정됩니다.
    // 보통 플레이어의 초기 X 위치나 맵의 중앙 X 위치로 설정합니다.
    public float fixedXPosition = 0f;

    // 카메라의 Y축 최소/최대 제한입니다.
    // 카메라가 특정 Y 범위 밖으로 나가지 않도록 설정할 수 있습니다.
    public float minYPosition = -10f; // 필요에 따라 조정
    public float maxYPosition = 10f;   // 필요에 따라 조정

    // 스크립트가 활성화될 때 또는 값이 변경될 때마다 호출됩니다.
    void LateUpdate()
    {
        // 플레이어 트랜스폼이 할당되지 않았다면 경고 메시지를 출력하고 종료합니다.
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform is not assigned to PlayerFollowCamera!");
            return;
        }

        // 플레이어의 현재 Y 위치를 가져옵니다.
        float playerY = playerTransform.position.y;

        // 카메라의 목표 Y 위치를 계산합니다.
        // fixedXPosition으로 X축은 고정하고, 플레이어의 Y 위치를 사용합니다.
        Vector3 targetPosition = new Vector3(fixedXPosition, playerY, transform.position.z);

        // Y축 제한을 적용합니다.
        targetPosition.y = Mathf.Clamp(targetPosition.y, minYPosition, maxYPosition);

        // Lerp 함수를 사용하여 현재 카메라 위치에서 목표 위치로 부드럽게 이동합니다.
        // FixedUpdate 대신 LateUpdate에서 카메라 이동을 처리하여 프레임 동기화를 개선합니다.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}