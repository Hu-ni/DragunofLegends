using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatHandler : MonoBehaviour
{
    // 몬스터 체력 // 맞은 데미지 관리는 EnemyResourceController에서
    [Range(1, 100)]
    [SerializeField]
    private int health = 10;
    public int Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, 100);
    }
    // 근접 데미지
    [Range(1, 50)]
    [SerializeField]
    private float meleeDamage = 10f;
    public float MeleeDamage => meleeDamage;

    // 원거리 데미지
    [Range(1, 50)]
    [SerializeField]
    private float rangedDamage = 5f;
    public float RangedDamage => rangedDamage;

    // 투사체 속도
    [Range(1, 20)]
    [SerializeField]
    private float projectileSpeed = 10f;
    public float ProjectileSpeed => projectileSpeed;
}
