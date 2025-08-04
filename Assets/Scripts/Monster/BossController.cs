using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour
{
    [Header("몬스터 움직임")]
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float moveSpeed = 1.5f;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("패턴 제어")]
    [SerializeField] private float rangedAttackInterval = 3f;
    [SerializeField] private float patternCooldown = 10f;
    private float lastAttackTime;
    private float lastPatternTime;
    private bool isPatternActive = false;

    [Header("보스 페이즈")]
    private int _currentPhase = 1; // 1페이즈로 시작

    private float bossContactDamage;
    private float bossRangedDamage;
    private float bossprojectileSpeed;
    private BossStatHandler bossStatHandler;

    [Header("원거리 공격")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private BossPatterns bossPatterns;

    protected NavMeshAgent agent;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = Time.time;
        lastPatternTime = Time.time;

        bossStatHandler = GetComponent<BossStatHandler>();
        bossPatterns = GetComponent<BossPatterns>();

        agent = GetComponent<NavMeshAgent>();

        if (agent != null)
        {
            agent.speed = 0.5f; // 이동 속도 설정
        }
            if (bossStatHandler != null)
        {
            bossContactDamage = bossStatHandler.BossContactDamage;
            bossRangedDamage = bossStatHandler.BossRangedDamage;
            bossprojectileSpeed = bossStatHandler.BossProjectileSpeed;
        }

        if (bossPatterns != null)
        {
            bossPatterns.onPatternFinished += OnPatternFinished;
        }
    }

    private void OnDestroy()
    {
        if (bossPatterns != null)
        {
            bossPatterns.onPatternFinished -= OnPatternFinished;
        }
    }

    private void Update()
    {
        // 2페이즈 전환 조건: 체력이 50% 이하가 되고, 아직 2페이즈가 아닐 때
        if (bossStatHandler != null && bossStatHandler.BossCurrentHealth <= bossStatHandler.BossMaxHealth / 2 && _currentPhase == 1)
        {
            _currentPhase = 2;
            Debug.Log("보스가 2페이즈로 전환됩니다!");
            
        }

        if (!isPatternActive)
        {
            MoveToPlayer();

            if (Time.time >= lastAttackTime + rangedAttackInterval)
            {
                StartCoroutine(TripleShotRoutine());
                lastAttackTime = Time.time;
            }

            if (Time.time >= lastPatternTime + patternCooldown)
            {
                if (bossPatterns != null)
                {
                    isPatternActive = true;
                    // 현재 페이즈에 따라 패턴을 실행
                    bossPatterns.ExecutePattern(_currentPhase);
                }
                lastPatternTime = Time.time;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!isPatternActive)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void MoveToPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            float distance = direction.magnitude;

            if (distance <= chaseRange)
            {
                movement = direction.normalized;
            }
            else
            {
                movement = Vector2.zero;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(bossContactDamage);
            }
        }
    }

    IEnumerator TripleShotRoutine()
    {
        Vector2 direction = (player.position - firePoint.position).normalized;
        FireProjectile(direction);
        yield return new WaitForSeconds(0.2f);
        FireProjectile(direction);
        yield return new WaitForSeconds(0.2f);
        FireProjectile(direction);
    }

    private void FireProjectile(Vector2 direction)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = direction * bossprojectileSpeed;
                EnemyProjectile enemyprojectile = bullet.GetComponent<EnemyProjectile>();
                if (enemyprojectile != null)
                {
                    enemyprojectile.damage = (int)bossRangedDamage;
                }
            }
        }
    }

    private void OnPatternFinished()
    {
        isPatternActive = false;
        Debug.Log("패턴 끝");
    }
}