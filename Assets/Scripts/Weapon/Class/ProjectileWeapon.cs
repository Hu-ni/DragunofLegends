using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeapon : WeaponBase
{
    protected int projectileCounts;
    protected float projectileSpeeds;
    protected int penetrationCounts;

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

        if (Input.GetKeyDown(KeyCode.Space)) //확인하기
        {
            Attack();
        }
    }

    public override void SetWeaponStat(int level)
    {
        base.SetWeaponStat(level);

        ProjectileWeapon_SO projectileWeaponData = weaponData as ProjectileWeapon_SO;

        int projectileCounts = projectileWeaponData.projectileCounts[level];
        float projectileSpeeds = projectileWeaponData.projectileSpeeds[level];
        int penetrationCounts = projectileWeaponData.penetrationCounts[level];
    }

    public override void Attack()
    {
        if (attackCooltime > 0f)
        {
            return;
        }

        LayerMask targetLayer = LayerMask.GetMask("Enemy");

        for (int i = 0; i < projectileCounts; i++)
        {
            CreateProjectile();

            Debug.Log("발사");
            attackCooltime = cooldown;

        }
    }

    public void CreateProjectile()
    {
        GameObject obj = Instantiate(projectilePrefab, startPostiion.position, Quaternion.identity);
        //ProjectileController projectileController = obj.GetComponent<ProjectileController>();
        //projectileController.Init(direction, rangeWeaponHandler);
    }
}

