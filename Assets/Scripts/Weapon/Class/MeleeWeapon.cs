using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : WeaponBase
{
    protected float attackRange;

    SpriteRenderer spriteRenderer;

    public Vector2 AttackBoxRange = new Vector2(0.5f, 1f);

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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    public override void SetWeaponStat(int level)
    {
        base.SetWeaponStat(level);

        MeleeWeapon_SO meleeWeaponData = weaponData as MeleeWeapon_SO;

        attackRange = meleeWeaponData.attackRanges[level];

    }


    public override void Attack()
    {
        base.Attack();
        
        LayerMask targetLayer = LayerMask.GetMask("Enemy");

        Vector2 center = effectAnimator.transform.position + transform.right * AttackBoxRange.x / 2;

        Collider2D[] hits = Physics2D.OverlapBoxAll(center, AttackBoxRange, 0f, targetLayer);
        effectAnimator.SetTrigger("IsAttack");

        Debug.Log("공격");
        attackCooltime = cooldown;

    }

    void OnDrawGizmos()
    {
        Vector2 center = transform.position + transform.right * AttackBoxRange.x / 2 + transform.right * 0.275f;


        Gizmos.color = Color.red;
        Gizmos.matrix = Matrix4x4.TRS(center, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, AttackBoxRange);
    }


    
}
