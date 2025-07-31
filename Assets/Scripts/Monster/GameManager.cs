using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private ResourceController _playerResourceController;

    [SerializeField]
    private int currentWaveIndex = 0;

    private EnemyManager enemyManager;

    private void Awake()
    {
        instance = this;

        enemyManager = GetComponentInChildren<EnemyManager>();
        enemyManager.Init(this);
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

    private void Update()
    {
        StartGame();
    }
}
