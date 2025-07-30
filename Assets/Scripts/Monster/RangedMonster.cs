using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMonster : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform firePoint; //�̻����� ���� ��ġ / ���ϴ� ������Ʈ ������ �־��༭ ��ġ ���ϱ�
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
