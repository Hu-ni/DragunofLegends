using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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

        if (_stage != null)
            _stage.Initialize(this);
        else
            Debug.LogError("EnemyManager가할당되지 않았습니다");

    }

    private void Start()
    {
        if (UIManager.Instance == null)
        {
            SceneManager.LoadScene(Constants.SceneStageUI, LoadSceneMode.Additive);
        }
    }


    public void NextStage()
    {
        // 스테이지 생성
        // UI 업데이트
        // 몬스터 스폰
        _stage.CreateStage();
        UIManager.Instance.UpdateStageRound(_stage.CurrentStageIdx);
        _stage.SpawnMonster();
    }

    public void GameOver()
    {
        UIManager.Instance.ShowPopupUI<ResultPopupUI>();
    }

    public void UpdateMonsterCount(int count)
    {
        UIManager.Instance.UpdateMonsterCount(count);

        if (count <= 0)
            _stage.CheckClearStage();    //스테이지를 클리어했는지 확인하고 클리어 시 포탈 활성화

    }

}
