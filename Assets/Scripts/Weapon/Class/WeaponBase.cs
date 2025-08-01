using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBase : MonoBehaviour
{
    protected int level = 0;

    [SerializeField]
    protected WeaponBase_SO weaponData;

    protected float damage;
    protected float cooldown;
    protected float criticalChance;

    protected Sprite weaponSprite;
    protected Animator weaponAnimator;

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
}
