using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    private Rigidbody2D rb;
    private AnimationHandler animationHandler; // AnimationHandler ��ũ��Ʈ ����

    private Vector2 movementInput; // ���� �̵� �Է� ��

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>(); // ���� ������Ʈ�� �ִ� AnimationHandler ������Ʈ ��������
    }

    void Update()
    {
        // �Է� ó��
        movementInput.x = Input.GetAxisRaw("Horizontal"); // A, D Ű �Ǵ� Left/Right Arrow
        movementInput.y = Input.GetAxisRaw("Vertical");   // W, S Ű �Ǵ� Up/Down Arrow

        // �Է� ����ȭ (�밢�� �̵� �� �ӵ� �����ϰ� ����)
        if (movementInput.magnitude > 1f)
        {
            movementInput.Normalize();
        }

        // AnimationHandler�� �Է� ���� ����
        animationHandler.SetAnimationParameters(movementInput);
    }

    void FixedUpdate()
    {
        // ���� ������Ʈ (FixedUpdate���� ó���ϴ� ���� ������)
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }
}