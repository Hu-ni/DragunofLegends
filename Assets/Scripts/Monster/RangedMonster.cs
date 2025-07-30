using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform firePoint; //미사일이 나갈 위치 / 원하는 오브젝트 하위에 넣어줘서 위치 정하기
    public float fireInterval = 2f;

    private void Start()
    {
        InvokeRepeating("FireMissile", fireInterval, fireInterval);
    } 

    void FireMissile()
    {
        Instantiate(missilePrefab, firePoint.position, firePoint.rotation);
    }
}
