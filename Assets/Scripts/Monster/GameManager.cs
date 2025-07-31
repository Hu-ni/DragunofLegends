using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private EnemyResourceController _playerResourceController;

    [SerializeField]
    private int currentWaveIndex = 0;

    [SerializeField]
    private EnemyManager enemyManager;

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
            Debug.LogError("EnemyManager가 할당되지 않았습니다");
        }
    }

    private void Start()
    {
        Debug.Log("GameManager Start 메서드 호출됨!");
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

    public void GameOver()
    {
        enemyManager.StopWave();
    }


}
