using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyBaseController
{
    private EnemyManager enemyManager;
    private Transform target;
    private NavMeshAgent agent;

    public enum EnemyType { Melee, Ranged }

    [SerializeField]
    private EnemyType enemyType;

    [SerializeField]
    private float attackRange = 5f;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private float fireCooldown = 2f;

    private float fireTimer;

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent != null)
        {
            agent.updateRotation = false;
            agent.updateUpAxis = false;
        }
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (target == null || agent == null)
            return;

        float distance = Vector3.Distance(transform.position, target.position);
        Vector2 direction = (target.position - transform.position).normalized;
        lookDirection = direction;

        switch (enemyType)
        {
            case EnemyType.Melee:
                // NavMeshAgent가 알아서 따라가므로 별도 처리 X
                agent.isStopped = false;
                break;

            case EnemyType.Ranged:
                if (distance > attackRange)
                {
                    agent.isStopped = false; // 계속 접근
                }
                else
                {
                    agent.isStopped = true; // 멈추고 발사

                    fireTimer -= Time.deltaTime;
                    if (fireTimer <= 0f)
                    {
                        FireProjectile(direction);
                        fireTimer = fireCooldown;
                    }
                }
                break;
        }
    }

    private void FireProjectile(Vector2 direction)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f;
        }
    }
}