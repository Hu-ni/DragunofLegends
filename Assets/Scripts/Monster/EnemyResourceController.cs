using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private EnemyBaseController baseController;
    private MonsterStat monsterStat;
    private AnimationHandler animationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => monsterStat.Health;

    private void Awake()
    {
        monsterStat = GetComponent<MonsterStat>();
        animationHandler = GetComponent<AnimationHandler>();
        baseController = GetComponent<EnemyBaseController>();
    }

    private void Start()
    {
        CurrentHealth = monsterStat.Health;
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                animationHandler.InvincibilityEnd();
            }
        }
    }

    public bool ChangeHealth(float change)
    {
        if (change == 0 || timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change < 0)
        {
            animationHandler.Damage();

        }

        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        baseController.Death();
    }

}