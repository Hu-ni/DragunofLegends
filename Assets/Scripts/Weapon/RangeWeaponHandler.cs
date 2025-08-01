using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeaponHandler : WeaponHandler
{
    public GameObject ProjectilePrefab;     // 투사체 프리팹
    public Transform firePoint;                   // 투사체 생성 위치

    private float nextFireTime = 0f;              // 다음 공격 시간
    public float rangeShootSpeed = 0.5f;  //투사체속도

    private void Update()
    {

        // 공격 쿨타임이 지났다면 공격 실행
        if (Time.time >= nextFireTime)
        {
            Attack();
            nextFireTime = Time.time + Delay;
        }
    }

    // 공격 실행
    public override void Attack()
    {

    }
}
