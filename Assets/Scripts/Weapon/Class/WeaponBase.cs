using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected int level = 0;

    [SerializeField]
    protected WeaponBase_SO weaponData;

    protected float damage;
    public float cooldown;
    protected float criticalChance;

    protected Sprite weaponSprite;
    protected Animator effectAnimator;

    protected float attackCooltime = 0f;

    protected virtual void Update()
    {
        attackCooltime = Mathf.Clamp(attackCooltime - Time.deltaTime, 0, cooldown);

    }

    public virtual void SetWeaponStat(int level)
    {
        damage = weaponData.damages[level];
        cooldown = weaponData.cooldowns[level];
        criticalChance = weaponData.criticalChances[level];
    }

    public virtual void Attack()
    {
        if (attackCooltime > 0f)
        {
            return;
        }
    }
}
