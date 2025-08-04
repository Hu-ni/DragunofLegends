using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatHandler : MonoBehaviour
{
    [SerializeField]
    private float bossMaxHealth = 500;
    [SerializeField]
    private float bossCurrentHealth;
    public float BossCurrentHealth
    {
        get => bossCurrentHealth;
        set => bossCurrentHealth = Mathf.Clamp(value, 0, 500);
    }

    [SerializeField]
    private float bossChargeDamage = 30;
    public float BossChargeDamage => bossChargeDamage;

    [SerializeField]
    private float bossRangedDamage = 15;
    public float BossRangedDamage => bossRangedDamage;

    [SerializeField]
    private float bossContactDamage = 10;
    public float BossContactDamage => bossContactDamage;

    [SerializeField]
    private float bossprojectileSpeed = 10f;
    public float BossProjectileSpeed => bossprojectileSpeed;


    private void Start()
    {
        BossCurrentHealth = bossMaxHealth;
    }
    private void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        BossCurrentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + BossCurrentHealth);

        if (BossCurrentHealth <= 0)
        {
            // 죽음 관리 // 애니메이션
        }
    }
}
