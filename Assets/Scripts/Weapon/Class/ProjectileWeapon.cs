using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    float searchRadius = 2f;
    Transform target;

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
        base.Attack();

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, searchRadius, LayerMask.GetMask("Enemy"));
        float minDistance = float.MaxValue;

        foreach (Collider2D hit in hits)
        {
            float dist = Vector2.Distance(transform.position, hit.transform.position);
            if (dist < minDistance)
            {
                minDistance = dist;
                target = hit.transform;
            }
        }

        if (target != null)
        {
            Vector2 dir = target.transform.position - transform.position;
            dir = dir.normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
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

