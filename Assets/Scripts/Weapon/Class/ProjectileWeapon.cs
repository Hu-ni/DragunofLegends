using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    protected int projectileCount;
    public int ProjectileCount { get { return projectileCount; } }
    protected float projectileSpeed;
    public float ProjectileSpeed { get { return projectileSpeed; } }

    protected int penetrationCount;
    public int PenetrationCount { get { return penetrationCount; } }


    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public Transform startPostiion;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        if (weaponData == null)
        {
            Debug.LogWarning("무기 데이터 연결 해주세요.");
            return;

        }

        if (weaponSprite == null)
        {
            weaponSprite = weaponData.weaponSprites[0];
        }

        if (effectAnimator == null)
        {
            effectAnimator = GetComponentInChildren<Animator>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
    }

    private void Start()
    {
        SetWeaponStat(level);
        spriteRenderer.sprite = weaponSprite;
        attackCooltime = cooldown;
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attack();
        }
    }

    public override void SetWeaponStat(int level)
    {
        base.SetWeaponStat(level);

        ProjectileWeapon_SO projectileWeaponData = weaponData as ProjectileWeapon_SO;

        projectileCount = projectileWeaponData.projectileCounts[level];
        projectileSpeed = projectileWeaponData.projectileSpeeds[level];
        penetrationCount = projectileWeaponData.penetrationCounts[level];
    }

    public override void Attack()
    {
        if (attackCooltime > 0f)
        {
            return;
        }


        if (gameObject == null)
        {
            Debug.Log("발사체 프리팹 로드 실패");
            return;
        }

        CreateProjectile();
        
        
    }

    public void CreateProjectile()
    {
        GameObject obj = Instantiate(projectilePrefab, startPostiion.position, Quaternion.identity);
        Projectile projectile = obj.GetComponent<Projectile>();
        projectile.Initialize(this, transform.right);
        //ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        //projectileController.Init(direction, rangeWeaponHandler);
    }
}

