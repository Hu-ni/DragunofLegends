using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
public class WeaponHandler : MonoBehaviour
{
    private WeaponData weaponData;

    public int weaponLevel;
    [SerializeField] private Transform firePoint;

    // 무기 스탯 프로퍼티 (Awake에서 초기화됨)
    public float Delay { get; private set; }               // 공격 간격 (연사 속도)
    public int Power { get; private set; }                 // 공격력
    public float AttackRange { get; private set; }         // 사거리
    public float BulletSize { get; private set; }          // 범위 (투사체 크기 등)
    public int ProjectileCount { get; private set; }       // 발사체 수
    public int PenetrateCount { get; private set; }        // 관통 횟수
    public float CritRate { get; private set; }            // 치명타 확률
    public float CritDamage { get; private set; }          // 치명타 데미지
    // 애니메이터 관련
    private static readonly int IsAttack = Animator.StringToHash("IsAttack");

    private Animator animator;
    private SpriteRenderer weaponRenderer;
    private int currentLevel;
    [SerializeField] public WeaponHandler projectilePrefab;

    private void Awake()
    {
        WeaponLevelUp();
        // WeaponData는 같은 오브젝트에 붙은 컴포넌트에서 가져옴
        weaponData = GetComponent<WeaponData>();
        animator = GetComponentInChildren<Animator>();
        weaponRenderer = GetComponentInChildren<SpriteRenderer>();

        // 무기 스탯 초기화 (현재 레벨 기준)
        Delay = weaponData.fireRate[currentLevel];
        Power = weaponData.weaponDamage[currentLevel];
        AttackRange = weaponData.range[currentLevel];
        BulletSize = weaponData.area[currentLevel];
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
        transform.localScale = Vector3.one * BulletSize;
    }

    public void ApplyWeaponStats()
    {
        Delay = weaponData.fireRate[currentLevel];
        Power = weaponData.weaponDamage[currentLevel];
        AttackRange = weaponData.range[currentLevel];
        BulletSize = weaponData.area[currentLevel];
        ProjectileCount = weaponData.projectileNum[currentLevel];
        PenetrateCount = weaponData.penetrateNum[currentLevel];
        CritRate = weaponData.critRate[currentLevel];
        CritDamage = weaponData.critdamage[currentLevel];
    }
    public void Fire()
    {
        float randomValue = Random.Range(0f, 1f);

        if (randomValue <= CritRate)
        {
            CreateProjectile(isCritical: true);
        }
        else
        {
            CreateProjectile(isCritical: false);
        }
    }

    private void CreateProjectile(bool isCritical)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.Init(direction, weaponHandler, isCritical);
    }

    // 애니메이션 실행
    private void AttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger(IsAttack);
        }
    }

    public void GetLevelData(int WeaponLevel)
    {
        currentLevel = weaponData.level[WeaponLevel];
    }
    public void WeaponLevelUp()
    {
        weaponLevel++;
    }
    public void Attack()
    {
        switch (weaponData.weaponType)
        {
            case WeaponType.range:
                StraightAttack();
                break;
            case WeaponType.melee:
                AreaAttack();
                break;
            case WeaponType.chase:
                ChaseAttack();
                break;
        }
    }
    public void StraightAttack(/*Vector3 lookdirection*/)
    {
        //방향을 받아서 그 방향에 직선형 투사체 발사
        //공격시 무기 공격 애니메이션 작동
        //맞는 경우는 따로 OnAttack으로 만들면 될듯?
    }

    public void AreaAttack()
    {

    }

    public void ChaseAttack()
    {

    }
}
