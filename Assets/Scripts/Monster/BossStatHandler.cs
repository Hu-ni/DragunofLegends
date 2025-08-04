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
    [SerializeField]
    private float invincibilityDuration = 0.5f;
    private float invincibilityTimer = 0f;

    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
        if (invincibilityTimer > 0)
        {
            invincibilityTimer -= Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {
        // 무적 시간이 아닐 때만 데미지를 입도록
        if (invincibilityTimer > 0)
        {
            Debug.Log("Player is invincible!");
            return;
        }

        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);

        // 데미지를 입은 후 무적 시간 시작
        invincibilityTimer = invincibilityDuration;

        if (currentHealth <= 0)
        {
            // 죽음 애니메이션
        }
    }
}
