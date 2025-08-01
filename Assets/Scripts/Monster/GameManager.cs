using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private ResourceController _playerResourceController;

    [SerializeField]
    private int currentWaveIndex = 0;

    [SerializeField]
    private EnemyManager enemyManager;

    private StageManager _stage;

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
        currentWaveIndex = 0;
        StartGame();
    }

    public void StartGame()
    { 
        StartNextWave();
    }

    void StartNextWave()
    {
        currentWaveIndex += 1;
        enemyManager.StartWave(1 + currentWaveIndex / 5);
    }

    public void ClearStage()
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


}
