using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ChasingState
{ 
    Idle,
    Chasing
}

public class ChasingWeapon : WeaponBase
{
    protected int projectileCount;
    public int ProjectileCount { get { return projectileCount; } }

    protected float projectileSpeed;
    public float ProjectileSpeed { get { return projectileSpeed; } }

    ChasingState chasingState = ChasingState.Idle;

    Transform target;
    Transform player;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;

    float searchRadius = 2f;
    float distThreshold = 0.2f;
    private void Awake()
    {
        if (weaponSprite == null)
        {
            weaponSprite = weaponData.weaponSprites[0];
        }

        if (rigid == null)
        {
            rigid = GetComponentInChildren<Rigidbody2D>();
        }

        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (player == null)
        {
            if (transform.parent != null)
            {
                player = transform.parent;

            }
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Attack();
        }

    }


    private void FixedUpdate()
    {
        switch (chasingState)
        {
            case ChasingState.Idle:
                Wander();
                break;
            case ChasingState.Chasing:
                ChaseTarget();
                break;
            default:
                break;
        }
        
    }


    public override void SetWeaponStat(int level)
    {
        base.SetWeaponStat(level);

        ChansingWeapon_SO chasingWeaponData = weaponData as ChansingWeapon_SO;

        projectileCount = chasingWeaponData.projectileCounts[level];
        projectileSpeed = chasingWeaponData.projectileSpeeds[level];
    }

    public override void Attack()
    {
        base.Attack();

        if (target != null)
        {
            return;
        }

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
            chasingState = ChasingState.Chasing;
        }
    }


    private void Wander()
    {
        if (target != null)
        {
            return;
        }

        Vector2 dir = player.transform.position - transform.position;
        dir = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        rigid.AddForce(dir * projectileSpeed);
    }


    private void ChaseTarget()
    {
        if (rigid.velocity.magnitude > 0)
        {
            rigid.velocity = Vector3.zero;
        }

        if (target == null)
        {
            return;
        }

        if (CheckTargetIsClose(target))
        {
            chasingState = ChasingState.Idle;
            target = null;
            return;
        }
        
        Vector2 dir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position += (Vector3)(dir * projectileSpeed * Time.fixedDeltaTime);
    }

    
    bool CheckTargetIsClose(Transform currentTarget)
    {
        float dist = Vector2.Distance(transform.position, currentTarget.position);

        if (dist < distThreshold)
        {
            return true;
        }

        return false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        string layerName = LayerMask.LayerToName(collision.gameObject.layer);
        LayerMask layerMask = LayerMask.GetMask(layerName);

        if (layerMask != LayerMask.GetMask("Enemy"))
        {
            return;
        }

        // enemy TakeDamage 메서드 호출
        Debug.Log("에너미와 충돌");
        collision.GetComponent<EnemyBaseController>().Death();      // 테스트

    }
}
