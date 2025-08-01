using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponHandler : MonoBehaviour
{
    private WeaponData weaponData;

    // 현재 무기 레벨
    private int currentLevel = 0;

    // 무기 스탯 프로퍼티 (Awake에서 초기화됨)
    public float Delay { get; private set; }               // 공격 간격 (연사 속도)
    public int Power { get; private set; }                 // 공격력
    public float AttackRange { get; private set; }         // 사거리
    public float WeaponSize { get; private set; }          // 범위 (투사체 크기 등)
    public int ProjectileCount { get; private set; }       // 발사체 수
    public int PenetrateCount { get; private set; }        // 관통 횟수
    public float CritRate { get; private set; }            // 치명타 확률
    public float CritDamage { get; private set; }          // 치명타 데미지

    private int currentLevel = WeaponData.level;
    // 애니메이터 관련
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    private Animator animator;
    private SpriteRenderer weaponRenderer;

    private void Awake()
    {
        // WeaponData는 같은 오브젝트에 붙은 컴포넌트에서 가져옴
        weaponData = GetComponent<WeaponData>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        currentLevel = weaponLevel;

        // 무기 스탯 초기화 (현재 레벨 기준)
        Delay = weaponData.fireRate[currentLevel];
        Power = weaponData.weaponDamage[currentLevel];
        AttackRange = weaponData.range[currentLevel];
        WeaponSize = weaponData.area[currentLevel];
        ProjectileCount = weaponData.projectileNum[currentLevel];
        PenetrateCount = weaponData.penetrateNum[currentLevel];
        CritRate = weaponData.critRate[currentLevel];
        CritDamage = weaponData.critdamage[currentLevel];

        // 애니메이션 속도 = 연사 속도에 비례
        if (animator != null)
        {
            animator.speed = 1.0f / Delay;
        }

        // 무기 자체 크기 조절 (범위 기반)
        transform.localScale = Vector3.one * WeaponSize;
    }

    private void Update()
    {
        AimAtMouse();

        // 테스트용: 마우스 클릭 시 공격 실행
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    // 마우스를 바라보게 회전
    private void AimAtMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        direction.z = 0; // z축 제거

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // 무기 스프라이트 반전 (왼쪽/오른쪽)
        if (angle > 90 || angle < -90)
        {
            if (weaponRenderer != null) weaponRenderer.flipY = true;
        }
        else
        {
            if (weaponRenderer != null) weaponRenderer.flipY = false;
        }
    }

    public virtual void Attack()
    {
        AttackAnimation();
    }

    // 애니메이션 실행
    private void AttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(IsAttack);
        }
    }
}
