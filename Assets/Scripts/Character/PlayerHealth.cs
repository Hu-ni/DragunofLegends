using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action<float> OnHealthChanged;
    private GaugeBar hpBar;
    [SerializeField]
    private float maxHealth = 100f;
    private float currentHealth;
    public float CurrentHealth 
    {   get { return currentHealth; }
        set 
        { 
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth / maxHealth);
        } 
    }

    [SerializeField]
    private float invincibilityDuration = 0.5f;
    private float invincibilityTimer = 0f;

    private void Awake()
    {
        hpBar = GetComponentInChildren<GaugeBar>();
        OnHealthChanged += hpBar.SetValue;
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
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

        CurrentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + CurrentHealth);

        // 데미지를 입은 후 무적 시간 시작
        invincibilityTimer = invincibilityDuration;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        // 게임오버, 플레이어 사망을 여기에
        GameManager.instance.GameOver();
        gameObject.SetActive(false); // 일단 플레이어 오브젝트 비활성화
    }
}
