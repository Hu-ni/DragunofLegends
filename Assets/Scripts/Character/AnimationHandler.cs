using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public GameObject playerSpriteObject;

    // ������ �밢������ ���������� ���θ� ������ ���� �߰�
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
                Debug.LogError("AnimationHandler: 'Player' child object not found. Please assign 'Player Sprite Object' in the Inspector.", this);
                return;
            }
        }

        animator = playerSpriteObject.GetComponent<Animator>();
        spriteRenderer = playerSpriteObject.GetComponent<SpriteRenderer>();

        if (animator == null)
        {
            Debug.LogError("AnimationHandler: Animator component not found on Player sprite object.", this);
        }
        if (spriteRenderer == null)
        {
            Debug.LogError("AnimationHandler: SpriteRenderer component not found on Player sprite object.", this);
        }
    }

    public void SetAnimationParameters(Vector2 movementInput)
    {
        if (animator == null || spriteRenderer == null) return;

        float moveX = movementInput.x;
        float moveY = movementInput.y;

        bool isMoving = movementInput.magnitude > 0.1f; // �ణ�� ���� ���� ��� (���� ���� �Ǵ�)

        // ���� �밢������ �����̴� ������ �Ǵ�
        bool currentlyMovingDiagonal = (Mathf.Abs(moveX) > 0.1f && Mathf.Abs(moveY) > 0.1f);

        // Animator �Ķ���� ������Ʈ
        animator.SetFloat("MoveX", moveX);
        animator.SetFloat("MoveY", moveY);
        animator.SetBool("IsMoving", isMoving);

        // �̵� ���� ���� _lastMovementWasDiagonal�� ������Ʈ
        if (isMoving)
        {
            _lastMovementWasDiagonal = currentlyMovingDiagonal;
        }

        // Animator�� IsDiagonal �Ķ���ʹ� ���� �밢�� �̵� ���̰ų�,
        // (���� �����ε�) ������ �̵��� �밢���̾��� ��� true�� �ǵ��� ����
        // �� �κ��� �ٽ� ���� �����Դϴ�.
        animator.SetBool("IsDiagonal", isMoving ? currentlyMovingDiagonal : _lastMovementWasDiagonal);

        // SpriteRenderer flipX�� �̿��� �¿� ���� (���� ����)
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