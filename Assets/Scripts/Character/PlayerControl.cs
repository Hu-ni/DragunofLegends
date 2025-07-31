using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    private Rigidbody2D rb;
    private AnimationHandle AnimationHandler; // animationHandler 스크립트 참조

    private Vector2 movementInput; // 현재 이동 입력 값

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        AnimationHandler = GetComponent<AnimationHandle>(); // 같은 오브젝트에 있는 AnimationHandler 컴포넌트 가져오기
    }

    void Update()
    {
        // 입력 처리
        movementInput.x = Input.GetAxisRaw("Horizontal"); // A, D 키 또는 Left/Right Arrow
        movementInput.y = Input.GetAxisRaw("Vertical");   // W, S 키 또는 Up/Down Arrow

        // 입력 정규화 (대각선 이동 시 속도 일정하게 유지)
        if (movementInput.magnitude > 1f)
        {
            movementInput.Normalize();
        }

        // AnimationHandler에 입력 정보 전달
        AnimationHandler.SetAnimationParameters(movementInput);
    }

    void FixedUpdate()
    {
        // 물리 업데이트 (FixedUpdate에서 처리하는 것이 안정적)
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }
}