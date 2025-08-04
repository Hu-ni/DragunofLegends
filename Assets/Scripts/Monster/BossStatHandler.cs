using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStatHandler : MonoBehaviour
{
    public float maxHealth = 500;
    public float currentHealth;
    public float chargeDamage = 30;
    public float rangedDamage = 15;
    public float contecDamage = 10;


    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // 보스 사망 처리 로직
            Debug.Log("보스가 죽었습니다.");
        }
    }
}
