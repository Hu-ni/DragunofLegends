using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileWeapon", menuName = "Weapons/Projectile")]
public class ProjectileWeapon_SO : RangeWeapon_SO
{
    public int[] penetrationCounts;
}
