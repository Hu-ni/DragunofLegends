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
