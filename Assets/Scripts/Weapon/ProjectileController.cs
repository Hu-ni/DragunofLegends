using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] private LayerMask target;

    private RangeWeaponHandler rangeWeaponHandler; // 발사에 사용된 무기 정보 참조

    private float currentDuration; // 현재까지 살아있는 시간
    private Vector2 direction; // 발사 방향
    private bool isReady; // 발사 준비 완료 여부
    private Transform pivot; // 총알의 시각 회전을 위한 피벗

    private Rigidbody2D _rigidbody;
    private SpriteRenderer spriteRenderer;

    public bool fxOnDestory = true; // 충돌 시 이펙트를 생성할지 여부

    // 초기 컴포넌트 참조 설정
    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();
        pivot = transform.GetChild(0); // 피벗 오브젝트 (스프라이트 회전용)
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        // 생존 시간 누적
        currentDuration += Time.deltaTime;

        // 설정된 지속 시간 초과 시 자동 파괴
        if (currentDuration > rangeWeaponHandler.AttackRange)
        {
            DestroyProjectile(transform.position, false);
        }

        // 물리 이동 처리 (방향 * 속도)
        _rigidbody.velocity = direction * rangeWeaponHandler.rangeShootSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 지형 충돌 시 → 약간 앞 위치에 이펙트 생성하고 파괴
        if ((levelCollisionLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
        }
        // 공격 대상 레이어에 충돌했을 경우
        else if ((target.value & (1 << collision.gameObject.layer)) != 0)
        {
            OnAttack();
            DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }


    public void Init(Vector2 direction, RangeWeaponHandler weaponHandler)
    {
        rangeWeaponHandler = weaponHandler;

        this.direction = direction;
        currentDuration = 0;

        // 크기 적용
        transform.localScale = Vector3.one * weaponHandler.BulletSize;

        transform.right = this.direction;

        isReady = true;
    }

    // 투사체 파괴 함수
    private void DestroyProjectile(Vector3 position, bool createFx)
    {
        Destroy(this.gameObject);
    }

    public void OnAttack()
    {

    }
}

