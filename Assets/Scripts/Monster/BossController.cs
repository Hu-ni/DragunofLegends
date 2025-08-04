using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // NavMeshAgent 사용을 위해 추가

public class BossController : MonoBehaviour
{
    public enum BossState { Idle, Attack, Dead }
    public BossState currentState;

    [SerializeField] private BossStatHandler bossStatsHandler;
    [SerializeField] private BossPatterns bossPatterns;

    private NavMeshAgent agent;
    private Transform playerTarget;

    private int currentPhase = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent 컴포넌트가 없습니다!");
            return;
        }

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;

        currentState = BossState.Idle;
        bossPatterns.onPatternFinished += OnPatternFinished;
    }

    void Update()
    {
        if (currentState == BossState.Dead) return;

        // 보스 체력에 따라 페이즈 전환
        if (bossStatsHandler.currentHealth <= bossStatsHandler.maxHealth * 0.5f && currentPhase < 2)
        {
            Debug.Log("보스 2페이즈로 전환!");
            currentPhase = 2;
        }
        else if (currentPhase == 0)
        {
            currentPhase = 1;
        }

        // 상태에 따른 행동
        switch (currentState)
        {
            case BossState.Idle:
                // Idle 상태일 때 플레이어를 추적
                MoveToTarget();
                break;
            case BossState.Attack:
                // 공격 상태일 때 패턴 실행
                if (!bossPatterns.IsPatternRunning)
                {
                    // 패턴이 시작되면 NavMeshAgent를 비활성화
                    agent.enabled = false;
                    bossPatterns.ExecutePattern(currentPhase);
                }
                break;
        }
    }
    private void MoveToTarget()
    {
        // NavMeshAgent가 활성화된 상태에서만 이동
        if (agent != null && agent.enabled && playerTarget != null)
        {
            agent.SetDestination(playerTarget.position);
        }
    }

    private void OnPatternFinished()
    {
        currentState = BossState.Idle;

        if (agent != null)
        {
            agent.enabled = true;
        }
    }

    private void SetAttackState()
    {
        currentState = BossState.Attack;
    }

    void OnDestroy()
    {
        bossPatterns.onPatternFinished -= OnPatternFinished;
    }
}