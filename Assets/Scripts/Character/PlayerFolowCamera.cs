using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    // ������ �÷��̾� ������Ʈ�� �ν����Ϳ��� �Ҵ��մϴ�.
    public Transform playerTransform;

    // ī�޶��� Y�� ���� �ӵ��� �����մϴ�. ���� �������� ������ ���󰩴ϴ�.
    public float smoothSpeed = 0.125f;

    // ī�޶��� X�� ���� ��ġ�Դϴ�. �� ���� �������� X���� �����˴ϴ�.
    // ���� �÷��̾��� �ʱ� X ��ġ�� ���� �߾� X ��ġ�� �����մϴ�.
    public float fixedXPosition = 0f;

    // ī�޶��� Y�� �ּ�/�ִ� �����Դϴ�.
    // ī�޶� Ư�� Y ���� ������ ������ �ʵ��� ������ �� �ֽ��ϴ�.
    public float minYPosition = -10f; // �ʿ信 ���� ����
    public float maxYPosition = 10f;   // �ʿ信 ���� ����

    // ��ũ��Ʈ�� Ȱ��ȭ�� �� �Ǵ� ���� ����� ������ ȣ��˴ϴ�.
    void LateUpdate()
    {
        // �÷��̾� Ʈ�������� �Ҵ���� �ʾҴٸ� ��� �޽����� ����ϰ� �����մϴ�.
        if (playerTransform == null)
        {
            Debug.LogWarning("Player Transform is not assigned to PlayerFollowCamera!");
            return;
        }

        // �÷��̾��� ���� Y ��ġ�� �����ɴϴ�.
        float playerY = playerTransform.position.y;

        // ī�޶��� ��ǥ Y ��ġ�� ����մϴ�.
        // fixedXPosition���� X���� �����ϰ�, �÷��̾��� Y ��ġ�� ����մϴ�.
        Vector3 targetPosition = new Vector3(fixedXPosition, playerY, transform.position.z);

        // Y�� ������ �����մϴ�.
        targetPosition.y = Mathf.Clamp(targetPosition.y, minYPosition, maxYPosition);

        // Lerp �Լ��� ����Ͽ� ���� ī�޶� ��ġ���� ��ǥ ��ġ�� �ε巴�� �̵��մϴ�.
        // FixedUpdate ��� LateUpdate���� ī�޶� �̵��� ó���Ͽ� ������ ����ȭ�� �����մϴ�.
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}