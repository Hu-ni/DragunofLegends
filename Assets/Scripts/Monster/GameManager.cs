using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private EnemyResourceController _playerResourceController;

    [SerializeField]
    private EnemyManager enemyManager;
    [SerializeField]
    private StageManager _stage;
    [SerializeField]
    private UIManager _uiManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (enemyManager != null)
        {
            enemyManager.Init(this);
        }
        else
        {
            Debug.LogError("EnemyManager가할당되지 않았습니다");
        }

        if (_stage != null)
            _stage.Initialize(this);
        else
            Debug.LogError("EnemyManager가할당되지 않았습니다");

    }

    private void Start()
    {
        //Debug.Log("GameManager Start 메서드 호출됨!");
        NextStage();    // TODO: UI 상호작용으로 이동
        StartSpawnMonster();
    }

    // 몬스터 생성 시작
    void StartSpawnMonster()
    {
        _stage.SpawnMonster();
    }

    public void NextStage()
    {
        // 스테이지 생성
        // 몬스터 스폰
        // UI 업데이트
        _stage.CreateStage();
    }

    public void GameOver()
    {
        enemyManager.StopWave();
    }

    public void UpdateMonsterCount(int count)
    {
        if (count <= 0)
            _stage.ClearStage();
        
        // TODO: UI 업데이트
    }
}
