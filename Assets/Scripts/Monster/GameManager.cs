using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private EnemyResourceController _playerResourceController;

    //[SerializeField]
    //private EnemyManager enemyManager;
    [SerializeField]
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

        //if (enemyManager != null)
        //{
        //    enemyManager.Init(this);
        //}
        //else
        //{
        //    Debug.LogError("EnemyManager가할당되지 않았습니다");
        //}

        if (_stage != null)
            _stage.Initialize(this);
        else
            Debug.LogError("EnemyManager가할당되지 않았습니다");

    }

    private void Start()
    {
        //Debug.Log("GameManager Start 메서드 호출됨!");
        //SceneManager.LoadScene(Constants.Scene_Stage_UI, LoadSceneMode.Additive);
        NextStage();    // TODO: UI 상호작용으로 이동
        //StartSpawnMonster();
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
        UIManager.Instance.UpdateStageRound(_stage.CurrentStageIdx);
        _stage.SpawnMonster();
    }

    public void GameOver()
    {
        UIManager.Instance.ShowPopupUI<ResultPopupUI>();
        //enemyManager.StopWave();
    }

    public void UpdateMonsterCount(int count)
    {
        if (count <= 0)
            _stage.CheckClearStage();    //스테이지를 클리어했는지 확인하고 클리어 시 포탈 활성화

        UIManager.Instance.UpdateMonsterCount(count);
    }

}
