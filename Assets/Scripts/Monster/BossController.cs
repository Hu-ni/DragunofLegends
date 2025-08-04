using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("몬스터 움직임")]
    [SerializeField] private float chaseRange = 5f;
    [SerializeField] private float moveSpeed = 3f;
    private Transform player;
    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("패턴 제어")]
    [SerializeField] private float rangedAttackInterval = 3f; // 원거리 공격 쿨타임
    [SerializeField] private float patternCooldown = 10f; // 패턴 쿨타임
    private float lastAttackTime;
    private float lastPatternTime;

    private bool isPatternActive = false;

    private float bossContactDamage;
    private float bossRangedDamage;
    private float bossprojectileSpeed;
    private BossStatHandler bossStatHandler;

    [Header("원거리 공격")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private BossPatterns bossPatterns;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastAttackTime = Time.time;
        lastPatternTime = Time.time;

        // BossStatHandler와 BossPatterns 컴포넌트 가져오기
        bossStatHandler = GetComponent<BossStatHandler>();
        bossPatterns = GetComponent<BossPatterns>();

        if (bossStatHandler != null)
        {
            bossContactDamage = bossStatHandler.BossContactDamage;
            bossRangedDamage = bossStatHandler.BossRangedDamage;
            bossprojectileSpeed = bossStatHandler.BossProjectileSpeed;
        }

        // 패턴이 끝났을 때 BossController에 알리도록 이벤트 구독
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
        // 보스의 기본 움직임은 패턴 중이 아닐 때만 실행
        if (!isPatternActive)
        {
            MoveToPlayer();
        }

        // 원거리 공격 및 패턴 발동 로직
        if (!isPatternActive)
        {
            // 원거리 공격 쿨타임 체크
            if (Time.time >= lastAttackTime + rangedAttackInterval)
            {
                StartCoroutine(TripleShotRoutine());
                lastAttackTime = Time.time;
            }

            // 패턴 쿨타임 체크
            if (Time.time >= lastPatternTime + patternCooldown)
            {
                // BossPatterns 스크립트에 패턴 실행 요청
                if (bossPatterns != null)
                {
                    // isPatternActive 플래그를 true로 설정하고, BossPatterns에게 패턴 시작 명령
                    isPatternActive = true;
                    // 예시로 1 페이즈 패턴을 실행
                    bossPatterns.ExecutePattern(1);
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

    // 접촉 데미지
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

    // 원거리 공격
    void RangedAttack()
    {
        {
            if (projectilePrefab != null && firePoint != null)
            {
                StartCoroutine(TripleShotRoutine());
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

    // BossPatterns에서 패턴이 끝났을 때 호출되는 메서드
    private void OnPatternFinished()
    {
        isPatternActive = false;
        Debug.Log("패턴이 끝났음을 감지, 보스 컨트롤러가 다시 행동합니다.");
    }
}