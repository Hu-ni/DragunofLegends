using UnityEngine;

public class EnemyController : BaseController
{
    private EnemyManager enemyManager;
    private Transform target;

    public enum EnemyType { Melee, Ranged }

    [SerializeField]
    private EnemyType enemyType;

    [SerializeField]
    private float attackRange = 5f; // 원거리 몬스터는 이 사거리까지만 접근

    [SerializeField]
    private float fireCooldown = 2f;

    [SerializeField]
    private GameObject projectilePrefab; // 원거리 몬스터만 사용

    [SerializeField]
    private Transform firePoint; // 원거리 몬스터만 사용

    private float fireTimer;

    private void Update()
    {
        HandleAction();
    }

    public void Init(EnemyManager enemyManager, Transform target)
    {
        this.enemyManager = enemyManager;
        this.target = target;
        Debug.Log("Init called with target: " + target.name);
    }

    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, target.position);
    }
        protected Vector2 DirectionToTarget()
    {
        return (target.position - transform.position).normalized;
    }

    protected override void HandleAction()
    {
        base.HandleAction();

        if (target == null) // 타겟 없으면 멈추기
        {
            movementDirection = Vector2.zero;
            return;
        }

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();
        lookDirection = direction;

        switch (enemyType)
        {
            case EnemyType.Melee:
                movementDirection = direction;
                break;

            case EnemyType.Ranged:
                if (distance > attackRange)
                {
                    // 사거리 밖이면 접근
                    movementDirection = direction;
                }
                else
                {
                    // 사거리 안이면 발사
                    movementDirection = Vector2.zero;

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
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f; // 속도
        }
    }

}
