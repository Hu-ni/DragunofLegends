using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class BossPatterns : MonoBehaviour
{
    public bool IsPatternRunning { get; private set; } = false;

    [Header("미사일")]
    [SerializeField] private GameObject projectilePrefab; // 미사일 프리팹
    [SerializeField] private Transform firePoint; // 미사일이 발사될 위치
    [SerializeField] private float projectileSpeed = 10f; // 미사일 발사 속도
    [SerializeField] private float rangedDamage = 10f; // 미사일 데미지
    [SerializeField] private float missileInterval = 0.3f; // 미사일 발사 간격

    [Header("소환")]
    [SerializeField] private List<GameObject> monsterPrefabs; // 소환할 프리팹 목록
    [SerializeField] private int summonCount = 3; // 소환할 몬스터 수
    [SerializeField] private float summonRadius = 5f; // 보스 주변 소환 반경

    [Header("돌진")]
    [SerializeField] private float chargeSpeed = 10f; // 돌진 속도
    [SerializeField] private float chargeDuration = 1.5f; // 돌진 준비 시간
    [SerializeField] private float chargeCooldown = 3f; // 쿨타임

    private Rigidbody2D rb;
    private Transform player;

    // 코루틴 종료를 알리는 이벤트
    public delegate void PatternEndAction();
    public event PatternEndAction onPatternFinished;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // BossController가 이 메서드를 호출하여 패턴을 실행
    public void ExecutePattern(int phase)
    {
        if (IsPatternRunning) return;

        IsPatternRunning = true;

        switch (phase)
        {
            case 1:
                StartCoroutine(Phase1Patterns());
                break;
            case 2:
                StartCoroutine(Phase2Patterns());
                break;
            default: // 기본 패턴
                StartCoroutine(MissileAttack());
                break;
        }
    }

    // 기본 // 플레이어를 향해 미사일 3발 발사
    private IEnumerator MissileAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            FireProjectile(direction);
            yield return new WaitForSeconds(missileInterval);
        }

        IsPatternRunning = false;
        onPatternFinished?.Invoke();
    }

    private void FireProjectile(Vector2 direction)
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            if (bulletRigidbody != null)
            {
                bulletRigidbody.velocity = direction * projectileSpeed;

                EnemyProjectile enemyprojectile = bullet.GetComponent<EnemyProjectile>();
                if (enemyprojectile != null)
                {
                    enemyprojectile.damage = (int)rangedDamage;
                }
            }
        }
    }

    // 1 페이즈 // 몬스터 3마리 소환
    private IEnumerator SummonMonsters()
    {
        for (int i = 0; i < summonCount; i++)
        {
            // 하나를 랜덤으로 선택
            GameObject monsterToSummon = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];

            // 보스 주변의 랜덤한 위치 계산
            Vector2 randomPos = Random.insideUnitCircle * summonRadius;
            Vector3 summonPosition = transform.position + new Vector3(randomPos.x, randomPos.y, 0);

            // 몬스터 소환
            Instantiate(monsterToSummon, summonPosition, Quaternion.identity);

            yield return new WaitForSeconds(0.5f); // 소환 간격
        }

        IsPatternRunning = false;
        onPatternFinished?.Invoke();
    }

    // 2 페이즈 // 돌진 공격
    private IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(1.0f);
        Vector2 chargeDirection = (player.position - transform.position).normalized;
        rb.velocity = chargeDirection * chargeSpeed;
        yield return new WaitForSeconds(chargeDuration);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(chargeCooldown);

        IsPatternRunning = false;
        onPatternFinished?.Invoke();
    }

    // 몬스터 소환
    private IEnumerator Phase1Patterns()
    {
        yield return StartCoroutine(SummonMonsters());
        yield return new WaitForSeconds(2f);

        IsPatternRunning = false;
        onPatternFinished?.Invoke();
    }

    // 돌진 + 소환
    private IEnumerator Phase2Patterns()
    {
        yield return StartCoroutine(ChargeAttack());
        yield return new WaitForSeconds(1f);
        yield return StartCoroutine(SummonMonsters());

        IsPatternRunning = false;
        onPatternFinished?.Invoke();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsPatternRunning && rb.velocity.magnitude > 0 && collision.gameObject.CompareTag("Player"))
        {
            // 플레이어에게 데미지 처리
        }
    }
}