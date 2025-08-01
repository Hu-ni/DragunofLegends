using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResourceController : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private EnemyBaseController enemybasecontroller;
    private EnemyStatHandler enemystatHandler;
    private EnemyAnimationHandler enemyanimationHandler;

    private float timeSinceLastChange = float.MaxValue;

    public float CurrentHealth { get; private set; }
    public float MaxHealth => enemystatHandler.Health;

    private void Awake()
    {
        enemystatHandler = GetComponent<EnemyStatHandler>();
        enemyanimationHandler = GetComponent<EnemyAnimationHandler>();
        enemybasecontroller = GetComponent<EnemyBaseController>();
    }

    private void Start()
    {
        CurrentHealth = enemystatHandler.Health;
    }

    private void Update()
    {
        if (timeSinceLastChange < healthChangeDelay)
        {
            timeSinceLastChange += Time.deltaTime;
            if (timeSinceLastChange >= healthChangeDelay)
            {
                enemyanimationHandler.InvincibilityEnd();
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
            enemyanimationHandler.Damage();

        }

        if (CurrentHealth <= 0f)
        {
            Death();
        }

        return true;
    }

    private void Death()
    {
        enemybasecontroller.Death();
    }

}