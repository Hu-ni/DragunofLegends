using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    public WeaponData weaponData;
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
    public void StraightAttack()
    {

    }

    public void AreaAttack()
    {

    }

    public void ChaseAttack()
    {

    }
}
