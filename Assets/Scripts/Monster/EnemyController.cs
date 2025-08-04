using UnityEngine;
using UnityEngine.AI;

public class EnemyController : EnemyBaseController
{
    [SerializeField]
    private Transform target;

    public enum EnemyType { Melee, Ranged }

    [SerializeField] private EnemyType enemyType;
    [SerializeField] private float attackRange = 5f;
    private EnemyStatHandler statHandler;
    private float meleeDamage;
    private float rangedDamage;
    private float projectileSpeed;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireCooldown = 2f;
    private float fireTimer;

    protected override void Start()
    {
        base.Start();

        statHandler = GetComponent<EnemyStatHandler>();

        if (statHandler != null)
        {
            meleeDamage = statHandler.MeleeDamage;
            rangedDamage = statHandler.RangedDamage;
            projectileSpeed = statHandler.ProjectileSpeed;
        }

        if (agent != null)
        {
            agent.speed = 3.5f; // 이동 속도 설정 (원하는 값으로)
            if (enemyType == EnemyType.Melee)
            {
                agent.stoppingDistance = 0.1f;
            }
            else if (enemyType == EnemyType.Ranged)
            {
                agent.stoppingDistance = attackRange;
            }
        }

        if (target == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("Target이 혹은 Player가 없습니다");
            }
        }
    }

    protected override void HandleAction()
    {
        if (target == null || agent == null)
        {
            isAgentMoving = false;
            return;
        }

        // 목적지 설정
        agent.SetDestination(target.position);
        lookDirection = (target.position - transform.position).normalized;

        // 실제 거리를 계산
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // 공격 범위 안에 들어왔는지 여부로 isAgentMoving을 결정
        if (distanceToTarget <= agent.stoppingDistance + 0.1f)
        {
            isAgentMoving = false;
        }
        else
        {
            isAgentMoving = true;
        }

        // 몬스터가 공격 범위 안에 들어오고 원거리 타입일 때만 공격
        if (!isAgentMoving && enemyType == EnemyType.Ranged)
        {
            fireTimer -= Time.deltaTime;
            if (fireTimer <= 0f)
            {
                FireProjectile(lookDirection);
                fireTimer = fireCooldown;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) // 접촉 데미지
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(meleeDamage);
            }
        }
    }

    private void FireProjectile(Vector2 direction) // 원거리 공격
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = direction * projectileSpeed;

                // Projectile 스크립트에 데미지 전달
                EnemyProjectile enemyprojectile = bullet.GetComponent<EnemyProjectile>();
                if (enemyprojectile != null)
                {
                    enemyprojectile.damage = (int)rangedDamage; 
                }
            }
        }
    }
}