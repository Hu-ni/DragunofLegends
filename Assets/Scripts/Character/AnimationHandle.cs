using UnityEngine;

public class AnimationHandle : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public GameObject playerSpriteObject;

    // 이전에 대각선으로 움직였는지 여부를 저장할 변수 추가
    private bool _lastMovementWasDiagonal = false;

    void Awake()
    {
        if (playerSpriteObject == null)
        {
            Transform playerChild = transform.Find("Player");
            if (playerChild != null)
            {
                playerSpriteObject = playerChild.gameObject;
            }
            else
            {
                Debug.LogError("AnimationHandle: 'Player' child object not found. Please assign 'Player Sprite Object' in the Inspector.", this);
                return;
            }
        }

        animator = playerSpriteObject.GetComponent<Animator>();
        spriteRenderer = playerSpriteObject.GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            Debug.LogError("AnimationHandle: Animator component not found on Player sprite object.", this);
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("AnimationHandle: SpriteRenderer component not found on Player sprite object.", this);
        }
    }

    public void SetAnimationParameters(Vector2 movementInput)
    {
        if (animator == null || spriteRenderer == null) return;

        float moveX = movementInput.x;
        float moveY = movementInput.y;

        bool isMoving = movementInput.magnitude > 0.1f; // 약간의 오차 범위 허용 (정지 상태 판단)

        // 현재 대각선으로 움직이는 중인지 판단
        bool currentlyMovingDiagonal = (Mathf.Abs(moveX) > 0.1f && Mathf.Abs(moveY) > 0.1f);

        // Animator 파라미터 업데이트
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
        animator.SetBool("IsMoving", isMoving);

        // 이동 중일 때만 _lastMovementWasDiagonal을 업데이트
        if (isMoving)
        {
            _lastMovementWasDiagonal = currentlyMovingDiagonal;
        }

        // Animator의 IsDiagonal 파라미터는 현재 대각선 이동 중이거나,
        // (정지 상태인데) 마지막 이동이 대각선이었을 경우 true가 되도록 설정
        // 이 부분이 핵심 수정 사항입니다.
        animator.SetBool("IsDiagonal", isMoving ? currentlyMovingDiagonal : _lastMovementWasDiagonal);

        // SpriteRenderer flipX를 이용한 좌우 반전 (선택 사항)
        if (moveX < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (moveX > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
}