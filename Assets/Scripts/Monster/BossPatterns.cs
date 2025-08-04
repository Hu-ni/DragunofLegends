using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatterns : MonoBehaviour
{
    // 보스 패턴 실행 상태를 외부에서 읽기 전용으로 접근
    public bool IsPatternRunning { get; private set; } = false;

    [Header("소환")]
    [SerializeField] private List<GameObject> monsterPrefabs; // 소환할 프리팹 목록
    [SerializeField] private int summonCount = 3; // 소환할 몬스터 수
    [SerializeField] private float summonRadius = 3f; // 보스 주변 소환 반경

    [Header("돌진")]
    [SerializeField] private float chargeSpeed = 10f; // 돌진 속도
    [SerializeField] private float chargeDuration = 1.5f; // 돌진 지속 시간
    [SerializeField] private float chargeCooldown = 1f; // 돌진 후 쿨타임

    private Rigidbody2D rb;
    private Transform player;

    // 패턴 종료를 알리는 이벤트 (BossController에서 구독)
    public delegate void PatternEndAction();
    public event PatternEndAction onPatternFinished;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void ExecutePattern(int phase)
    {
        if (IsPatternRunning) return;

        IsPatternRunning = true;

        switch (phase)
        {
            case 1:
                StartCoroutine(Phase1Routine());
                break;
            case 2:
                StartCoroutine(Phase2Routine());
                break;
            default: // 기본 패턴
                FinishPattern();
                break;
        }
    }

    // 몬스터 소환 로직
    private IEnumerator SummonMonstersRoutine()
    {
        Debug.Log("소환 패턴 시작!");
        for (int i = 0; i < summonCount; i++)
        {
            GameObject monsterToSummon = monsterPrefabs[Random.Range(0, monsterPrefabs.Count)];
            Vector2 randomPos = Random.insideUnitCircle * summonRadius;
            Vector3 summonPosition = transform.position + new Vector3(randomPos.x, randomPos.y, 0);

            Instantiate(monsterToSummon, summonPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
        Debug.Log("소환 패턴 종료.");
    }

    // 돌진 공격 로직
    private IEnumerator ChargeAttackRoutine()
    {
        Debug.Log("돌진 패턴 시작!");
        yield return new WaitForSeconds(1.0f); // 돌진 준비 시간

        if (player != null)
        {
            Vector2 chargeDirection = (player.position - transform.position).normalized;
            rb.velocity = chargeDirection * chargeSpeed;
        }

        yield return new WaitForSeconds(chargeDuration); // 돌진 지속 시간
        rb.velocity = Vector2.zero;

        Debug.Log("돌진 패턴 종료.");
        yield return new WaitForSeconds(chargeCooldown); // 돌진 후 쿨타임
    }

    // 1 페이즈: 소환
    private IEnumerator Phase1Routine()
    {
        yield return StartCoroutine(SummonMonstersRoutine());
        FinishPattern();
    }

    // 2 페이즈: 돌진 + 소환
    private IEnumerator Phase2Routine()
    {
        yield return StartCoroutine(ChargeAttackRoutine());
        yield return new WaitForSeconds(1f); // 패턴 간의 대기 시간
        yield return StartCoroutine(SummonMonstersRoutine());
        FinishPattern();
    }

    // 패턴 종료
    private void FinishPattern()
    {
        IsPatternRunning = false;
        onPatternFinished?.Invoke();
    }
}